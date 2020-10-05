using UnityEngine;
using System.Collections;

public class DontDestroy_BGMusic : MonoBehaviour {

	public static DontDestroy_BGMusic dontDestroy;


	void Awake(){
		if(dontDestroy == null){
			dontDestroy = this;
			DontDestroyOnLoad (gameObject);
			Debug.Log ("SYS: Soy El Primero");}

		else if(dontDestroy != this){
			Destroy (gameObject);
			Debug.Log ("SYS: Ya Existe un Objeto DontDestroy. Me Destruyo");}
	}
		
}