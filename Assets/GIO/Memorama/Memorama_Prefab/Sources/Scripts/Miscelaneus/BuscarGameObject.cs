using UnityEngine;
using System.Collections;

public class BuscarGameObject : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	/*
	public static EventosImageTarger ObtenerImageTarget()
	{
		GameObject gameManager = GameObject.Find("GameManager"); //Los Busca en la Herarquía del juego

		if (gameManager != null) {
			return gameManager.GetComponent<EventosImageTarger> ();} 
		else {
			Debug.LogError("NO SE HA ENCONTRADO EL GAMEOBJECT GameManager CON EL COMPONENTE EventosImageTarger");
			return null;}
	}
	*/




	// Esta ES LA FORMA GENERICA DE C#
	//Obtiene cualquier componente de cualquier GameObject

	public static Tipo ObtenerComponente <Tipo>(string nombreGameObject) where Tipo : UnityEngine.Component
	{
		GameObject ObjetoJuego = GameObject.Find(nombreGameObject); //Los Busca en la Herarquía del juego

		if (ObjetoJuego != null) {
			return ObjetoJuego.GetComponent<Tipo> ();} 
		else {
			Debug.LogError("NO SE HA ENCONTRADO EL GAMEOBJECT CON EL COMPONENTE");
			return null;}
	}


}
