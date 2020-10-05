using UnityEngine;
using System.Collections;

public class DontDestroy2 : MonoBehaviour {

	public static DontDestroy2 dontDestroy2;


	void Awake(){
		if(dontDestroy2 == null){
			dontDestroy2 = this;
			DontDestroyOnLoad (gameObject);
			Debug.Log ("SYS: Soy El Primero");}

		else if(dontDestroy2 != this){
			Destroy (gameObject);
			Debug.Log ("SYS: Ya Existe un Objeto DontDestroy. Me Destruyo");}
	}
		
}