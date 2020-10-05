using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScrollAndPinch : MonoBehaviour
{
    public Camera Camera;
    public bool Rotate;
    public float Smoth = 0.1f;
    protected Plane Plane;

    [Header("Puntos Limite")]
    public Transform L1;    
    public Transform L3;

    [Header("WebGL")]
    public bool WebGL = false;

    [Header("Android High")]
    public bool Android_High = false;

    [Header("Test Zoom")]
    public Text TextZoom;


#if UNITY_IOS || UNITY_ANDROID

    private void Awake()
    {
        if (Camera == null)
            Camera = Camera.main;
    }

    private void Update()
    {

        //Update Plane
        if (Input.touchCount >= 1)
            Plane.SetNormalAndPosition(transform.up, transform.position);

        var Delta1 = Vector3.zero;
        var Delta2 = Vector3.zero;

        //Scroll
        if (Input.touchCount >= 1 && (!EventSystem.current.IsPointerOverGameObject() || WebGL))
        {            
            if (InRange())
            {
                Delta1 = PlanePositionDelta(Input.GetTouch(0));
                if (Input.GetTouch(0).phase == TouchPhase.Moved)        
                    Camera.transform.Translate(Delta1, Space.World);    
            }
            else
            {
                if(XLimit() == -1)
                    Camera.transform.position = new Vector3(L1.position.x, Camera.transform.position.y, Camera.transform.position.z);
                else if (XLimit() == 1)
                    Camera.transform.position = new Vector3(L3.position.x, Camera.transform.position.y, Camera.transform.position.z);

                if (ZLimit() == 1)
                    Camera.transform.position = new Vector3(Camera.transform.position.x, Camera.transform.position.y, L1.position.z);
                else if (ZLimit() == -1)
                    Camera.transform.position = new Vector3(Camera.transform.position.x, Camera.transform.position.y, L3.position.z);
            }  
        }

        //Pinch
        if (Input.touchCount >= 2)
        {
            var pos1  = PlanePosition(Input.GetTouch(0).position);
            var pos2  = PlanePosition(Input.GetTouch(1).position);
            var pos1b = PlanePosition(Input.GetTouch(0).position - Input.GetTouch(0).deltaPosition);
            var pos2b = PlanePosition(Input.GetTouch(1).position - Input.GetTouch(1).deltaPosition);

            //calc zoom
            var zoom = Vector3.Distance(pos1, pos2) /
                       Vector3.Distance(pos1b, pos2b);

            Vector3 PosicionCamera = Vector3.LerpUnclamped(pos1, Camera.transform.position, 1 / zoom);

            //edge case
            if (Android_High)
            {
                if (PosicionCamera.y > 0f && PosicionCamera.y <= 850f)
                {
                    //return;

                    if (TextZoom)
                    {
                        TextZoom.text = Camera.transform.position.y.ToString();
                    }

                    //Move cam amount the mid ray
                    Camera.transform.position = Vector3.LerpUnclamped(pos1, Camera.transform.position, 1 / zoom);
                }

                if (Rotate && pos2b != pos2)
                    Camera.transform.RotateAround(pos1, Plane.normal, Vector3.SignedAngle(pos2 - pos1, pos2b - pos1b, Plane.normal));
            }
        }

    }

    protected Vector3 PlanePositionDelta(Touch touch)
    {
        //not moved
        if (touch.phase != TouchPhase.Moved)
            return Vector3.zero;

        //delta
        var rayBefore = Camera.ScreenPointToRay(touch.position - touch.deltaPosition);
        var rayNow = Camera.ScreenPointToRay(touch.position);
        if (Plane.Raycast(rayBefore, out var enterBefore) && Plane.Raycast(rayNow, out var enterNow))
            return rayBefore.GetPoint(enterBefore) - rayNow.GetPoint(enterNow);

        //not on plane
        return Vector3.zero;
    }

    protected Vector3 PlanePosition(Vector2 screenPos)
    {
        //position
        var rayNow = Camera.ScreenPointToRay(screenPos);
        if (Plane.Raycast(rayNow, out var enterNow))
            return rayNow.GetPoint(enterNow);

        return Vector3.zero;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + transform.up);
    }

#else

    private void Awake()
    {
        if (Camera == null)
            Camera = Camera.main;
    }



    [Header("Delta (Consulta)")]
    public Vector3 delta = Vector3.zero;
 private Vector3 lastPos = Vector3.zero;
 
 private void Update()
    {        
        if (Input.GetMouseButtonDown(0) && (!EventSystem.current.IsPointerOverGameObject() || WebGL))
        {
            lastPos = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0) && (!EventSystem.current.IsPointerOverGameObject() || WebGL))
        {
            delta = Input.mousePosition - lastPos;

            // Do Stuff here

            Debug.Log("delta X : " + delta.x);
            Debug.Log("delta Y : " + delta.y);

            // End do stuff

            lastPos = Input.mousePosition;

            Vector3 NormDelta = new Vector3(-(delta.x * Smoth), 0, -(delta.y * Smoth));

            if (InRange())
            {
                Camera.transform.Translate(NormDelta, Space.World);
            }
            else
            {
                if(XLimit() == -1)
                    Camera.transform.position = new Vector3(L1.position.x, Camera.transform.position.y, Camera.transform.position.z);
                else if (XLimit() == 1)
                    Camera.transform.position = new Vector3(L3.position.x, Camera.transform.position.y, Camera.transform.position.z);

                if (ZLimit() == 1)
                    Camera.transform.position = new Vector3(Camera.transform.position.x, Camera.transform.position.y, L1.position.z);
                else if (ZLimit() == -1)
                    Camera.transform.position = new Vector3(Camera.transform.position.x, Camera.transform.position.y, L3.position.z);
            }            
        }        
    }


    //private void Update()
    //{

    //    //Update Plane
    //    if (Input.GetButtonDown("Fire1"))
    //        Plane.SetNormalAndPosition(transform.up, transform.position);

    //    var Delta1 = Vector3.zero;
    //    var Delta2 = Vector3.zero;

    //    //Scroll
    //    if (Input.GetButtonDown("Fire1"))
    //    {
    //        Delta1 = PlanePositionDelta(Input.GetTouch(0));
    //        if (Input.GetTouch(0).phase == TouchPhase.Moved)
    //            Camera.transform.Translate(Delta1, Space.World);
    //    }

    //    //Pinch
    //    //if (Input.touchCount >= 2)
    //    //{
    //    //    var pos1 = PlanePosition(Input.GetTouch(0).position);
    //    //    var pos2 = PlanePosition(Input.GetTouch(1).position);
    //    //    var pos1b = PlanePosition(Input.GetTouch(0).position - Input.GetTouch(0).deltaPosition);
    //    //    var pos2b = PlanePosition(Input.GetTouch(1).position - Input.GetTouch(1).deltaPosition);

    //    //    //calc zoom
    //    //    var zoom = Vector3.Distance(pos1, pos2) /
    //    //               Vector3.Distance(pos1b, pos2b);

    //    //    //edge case
    //    //    if (zoom == 0 || zoom > 10)
    //    //        return;

    //    //    //Move cam amount the mid ray
    //    //    Camera.transform.position = Vector3.LerpUnclamped(pos1, Camera.transform.position, 1 / zoom);

    //    //    if (Rotate && pos2b != pos2)
    //    //        Camera.transform.RotateAround(pos1, Plane.normal, Vector3.SignedAngle(pos2 - pos1, pos2b - pos1b, Plane.normal));
    //    //}

    //    }

        protected Vector3 PlanePositionDelta(Touch touch)
    {
        //not moved
        if (touch.phase != TouchPhase.Moved)
            return Vector3.zero;

        //delta
        var rayBefore = Camera.ScreenPointToRay(touch.position - touch.deltaPosition);
        var rayNow = Camera.ScreenPointToRay(touch.position);
        if (Plane.Raycast(rayBefore, out var enterBefore) && Plane.Raycast(rayNow, out var enterNow))
            return rayBefore.GetPoint(enterBefore) - rayNow.GetPoint(enterNow);

        //not on plane
        return Vector3.zero;
    }

    protected Vector3 PlanePosition(Vector2 screenPos)
    {
        //position
        var rayNow = Camera.ScreenPointToRay(screenPos);
        if (Plane.Raycast(rayNow, out var enterNow))
            return rayNow.GetPoint(enterNow);

        return Vector3.zero;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + transform.up);
    }


#endif

    bool InRange()
    {
        if(L1.position.x <= Camera.transform.position.x && Camera.transform.position.x <= L3.position.x)
        {
            if (L3.position.z <= Camera.transform.position.z && Camera.transform.position.z <= L1.position.z)
            {                
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    int XLimit()
    {
        if (Camera.transform.position.x <= L1.position.x)
        {
            return -1;
        }
        else if (Camera.transform.position.x >= L3.position.x)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }

    int ZLimit()
    {
        if (Camera.transform.position.z >= L1.position.z)
        {
            return 1;
        }
        else if (Camera.transform.position.z <= L3.position.z)
        {
            return -1;
        }
        else
        {
            return 0;
        }
    }
}
