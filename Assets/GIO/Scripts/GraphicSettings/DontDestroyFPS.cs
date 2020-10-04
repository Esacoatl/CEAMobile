using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class DontDestroyFPS : MonoBehaviour {

	public static DontDestroyFPS dontDestroyFPS;
	public GameObject GameObjectFPSText;  
	string InitialSceneName;
	bool Inactivo = false;

	void Awake(){
		if(dontDestroyFPS == null){
			dontDestroyFPS = this;
			DontDestroyOnLoad (gameObject);
			Debug.Log ("Soy El Primer DontDestroyFPS");}

		else if(dontDestroyFPS != this){
			Destroy (gameObject);
			Debug.Log ("Ya Existe un Objeto DontDestroyFPS. Me Destruyo");}

		GameObjectFPSText.SetActive (false);
	}

	void Start(){
		InitialSceneName = SceneManager.GetActiveScene ().name;
	}

	void Update(){
		if (!GameObjectFPSText.activeInHierarchy) {
			if(SceneManager.GetActiveScene ().name != InitialSceneName){
				Inactivo = true;
				Destroy (gameObject);}}
	}

	public void MostrarFPSText(bool Mostrar){
		GameObjectFPSText.SetActive (Mostrar);
	}

	void OnDestroy(){
		if (Inactivo) {
			Debug.Log ("Se Destruye Objeto DontDestroyFPS Por Inactividad en la Escena");}
	}

}