using UnityEngine;
using System.Collections;

public class SonidoBoton : MonoBehaviour {

	AudioSource audioSourceBoton;

	void Awake(){
		audioSourceBoton = GetComponent<AudioSource> ();}

	public void SuenaBoton(){
		audioSourceBoton.Play ();}

}