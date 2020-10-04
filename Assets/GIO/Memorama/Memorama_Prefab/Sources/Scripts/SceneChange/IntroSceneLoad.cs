using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class IntroSceneLoad : MonoBehaviour {

	public string NombreEscenaCargar = "00_Portada";
	public float TiempoEspera = 5f;

	void Start(){
		StartCoroutine (CargarEscena());}

	IEnumerator CargarEscena(){
		TiempoEspera = Time.realtimeSinceStartup + TiempoEspera;
		yield return new WaitUntil (() => Time.realtimeSinceStartup >= TiempoEspera);
		SceneManager.LoadScene (NombreEscenaCargar);}

}