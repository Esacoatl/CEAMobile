using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class BotonCargarEscena : MonoBehaviour {

	public string NombreEscenaCargar = "Game";
	public bool CargarAsync = false;
	public float TimeToWait = 1.2f;


	public void CargarEscena(){
		StartCoroutine("TimeToStartLoad");		
	}

	IEnumerator TimeToStartLoad()
	{
		float TiempoLoad = Time.realtimeSinceStartup + TimeToWait;
		yield return new WaitUntil(() => Time.realtimeSinceStartup >  TiempoLoad);
		if (CargarAsync) { CargarNivelAsync(); }
		else { CargarNivelJuego(); }
	}
	 
	void CargarNivelJuego(){
		SceneManager.LoadScene (NombreEscenaCargar);}

	void CargarNivelAsync(){
		SplashLoadingScreen.LoadScene (NombreEscenaCargar);
	}
}


