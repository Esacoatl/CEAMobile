using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Text))]
public class FPSCount : MonoBehaviour {

	[Header("Actualizacion del Texto En Segundos")]
	public float tiempoMostrarTexto = 0.5f;

	public enum AlgoritmoCalculoFPS{DeltaDifuso,  IntervaloContado, IntervaloPonderado, Todos};
	public AlgoritmoCalculoFPS AlgoritmoFPS;

	[Header("TimeZero: Solo En El Editor")]
	public bool TimeZero = false;

	Text TextFPS;
	bool MostrarTexto = true;

	/*Variables Algoritmo DeltaDifuso*/
	[Header("Algoritmo DeltaDifuso (Integracion Smooth Del DeltaTime)")]
	public float DifuseConstant = 0.1f;
	float FPS_Difuso;
	float msec_Difuso;
	float deltaTime_Difuso;

	/*Variables Algortimo IntervaloContado*/
	[Header("Algoritmo IntervaloContado (Cuentas Por Segundo)")]
	public float UpdatesPerSecond = 4.0f; 
	float FPS_Contado;
	float msec_Contado;
	float Frames_Contado;
	float tiempo_Contado;	 

	/*Variables Algoritmo IntervaloPonderado*/
	[Header("Algoritmo IntervaloPonderado (Intervalo De Ponderacion En Segundos)")]
	public float UpdateTimeInterval = 0.25f;
	float FPS_Ponderado ;
	float msec_Ponderado;
	float tiempo_Ponderado;
	float FramesContados_Ponderado;
	float FramesAcomulados_Ponderado;


	void Awake(){
		TextFPS = GetComponent<Text> ();
	}
	

	void Start () {
		StopAllCoroutines ();
		StartCoroutine (ActualizaFPSText ());
		tiempo_Ponderado = UpdateTimeInterval;
	}
	

	void OnEnable(){
		StopAllCoroutines ();
		StartCoroutine (ActualizaFPSText ());
	}
	

	void Update () {
		
		switch(AlgoritmoFPS){

			case AlgoritmoCalculoFPS.DeltaDifuso:
				DeltaDifuso ();
				break;
		
			case AlgoritmoCalculoFPS.IntervaloContado:
				IntervaloContado ();
				break;

			case AlgoritmoCalculoFPS.IntervaloPonderado:
				IntervaloPonderado ();
				break;

			case AlgoritmoCalculoFPS.Todos:
				DeltaDifuso ();
				IntervaloContado ();
				IntervaloPonderado ();
				break;}

		#if UNITY_EDITOR

			if (TimeZero && (Time.timeScale == 1)) {
				Time.timeScale = 0;} 
			
			else if (!TimeZero && (Time.timeScale == 0)) {
				Time.timeScale = 1;}

		#endif
	}


	IEnumerator ActualizaFPSText(){
		while (MostrarTexto) {
		  	switch(AlgoritmoFPS){

			case AlgoritmoCalculoFPS.DeltaDifuso:
				TextFPS.text = string.Format ("fps:{0:0}"/* ms:{1:0.0}*/, FPS_Difuso, msec_Difuso);
				FPSColorText (FPS_Difuso);
				break;

			case AlgoritmoCalculoFPS.IntervaloContado:
				TextFPS.text = string.Format("fps:{0:0.0} ms:{1:0.0}",FPS_Contado, msec_Contado);
				FPSColorText (FPS_Contado);
				break;

			case AlgoritmoCalculoFPS.IntervaloPonderado:
				TextFPS.text = string.Format("fps:{0:0.0} ms:{1:0.0}",FPS_Ponderado, msec_Ponderado);
				FPSColorText (FPS_Ponderado);
				break;

			case AlgoritmoCalculoFPS.Todos:
				TextFPS.text = string.Format
					("fps: {0:0.0} ({1:0.0} ms) \nfps: {2:0.0} ({3:0.0} ms) \nfps: {4:0.0} ({5:0.0} ms)",
					  FPS_Difuso,msec_Difuso,     FPS_Contado,msec_Contado,   FPS_Ponderado, msec_Ponderado);
				break;}

			float TiempoEspera = tiempoMostrarTexto + Time.realtimeSinceStartup;
			yield return new WaitUntil (() => Time.realtimeSinceStartup >= TiempoEspera);}
	}


	void FPSColorText(float FPS){
		if (FPS > 30f && TextFPS.color != Color.green) {
			TextFPS.color = Color.green;} 

		else if (FPS > 10f && FPS <= 30f && TextFPS.color != Color.yellow) {
			TextFPS.color = Color.yellow;} 

		else if(FPS <= 10f && TextFPS.color != Color.red){
			TextFPS.color = Color.red;}
	}


	void DeltaDifuso(){
		deltaTime_Difuso += (Time.unscaledDeltaTime - deltaTime_Difuso) * DifuseConstant;
		msec_Difuso = deltaTime_Difuso * 1000.0f;
		FPS_Difuso = 1.0f / deltaTime_Difuso;
	}


	void IntervaloContado(){ 
		Frames_Contado++;
		tiempo_Contado += Time.unscaledDeltaTime;

		if (tiempo_Contado > 1.0f/UpdatesPerSecond){
			FPS_Contado = Frames_Contado / tiempo_Contado ;
			msec_Contado = 1000.0f / FPS_Contado;
			Frames_Contado = 0.0f;
			tiempo_Contado -= 1.0f/UpdatesPerSecond;}
	}


	void IntervaloPonderado(){
		tiempo_Ponderado -= Time.unscaledDeltaTime;
		FramesAcomulados_Ponderado += /*Time.timeScale*/ 1.0f / Time.unscaledDeltaTime;
		++FramesContados_Ponderado;

		if(tiempo_Ponderado <= 0.0){
			FPS_Ponderado = FramesAcomulados_Ponderado/FramesContados_Ponderado;
			msec_Ponderado = 1000.0f / FPS_Ponderado;
			tiempo_Ponderado = UpdateTimeInterval;
			FramesAcomulados_Ponderado = 0.0f;
			FramesContados_Ponderado = 0.0f;}
	}

}