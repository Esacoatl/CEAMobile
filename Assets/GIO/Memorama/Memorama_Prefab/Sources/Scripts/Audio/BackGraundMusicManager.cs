using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class BackGraundMusicManager : MonoBehaviour {

	GameObject BackGraundMusic;

	bool Inicio = true;
	bool Inicio2 = true;


	void Start () {
		BackGraundMusic = GameObject.Find ("BackGraundMusic");
		/*TurnOnBackGraundMusic (false);*/}


	void Update(){
		if (SplashLoadingScreen.UsandoASYNC) {
			if (SplashLoadingScreen.END && Inicio) {
				TurnOnBackGraundMusic (true);
				Inicio = false;}} 

		else {
			if (Inicio2 && !SplashLoadingScreen.UsandoASYNC) {
				TurnOnBackGraundMusic (true);
				Inicio2 = false;}}}


	void TurnOnBackGraundMusic(bool Action){
		if (BackGraundMusic) {
			BackGraundMusic.SetActive (Action);}}

}