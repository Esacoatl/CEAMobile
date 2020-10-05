using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GraphicSettingsManager : MonoBehaviour {

	public GameObject PanelGraphicSettings;
	public GameObject[] ObjetosAOcultar;

	[Header("FPS Public Variables")]
	public Toggle ToggleFPS;

	DontDestroyFPS scriptDontDestroyFPS;
	bool Open;
	bool FPShowed;

	[Header("QualitySettings Public Variables")]
	public Dropdown DropdownQualitySelector;
	public Text TextQualitySettingsInfo;
	public bool CambioDinamico_Expensivo;
	public bool QualitySettingsInfo;

	string[] Qualitys;
	string CurrentQuality;


	void Awake(){
		scriptDontDestroyFPS = FindObjectOfType<DontDestroyFPS> ();
		if (scriptDontDestroyFPS)
			Debug.Log ("FPSObject Encontrado");
		else
			Debug.Log ("No se Encontro FPSObject");
	}
		

	void Start () {
		PanelGraphicSettings.SetActive (false);
		if(scriptDontDestroyFPS){
			ToggleFPS.isOn = scriptDontDestroyFPS.GameObjectFPSText.activeInHierarchy;}

		GetQualityInfo ();
		/*ActualizaDropdown_Lenguage ();*/
		DropdownQualitySelector.value = QualitySettings.GetQualityLevel ();

		if (QualitySettingsInfo) {
			TextQualitySettingsInfo.gameObject.SetActive (true);
			Actualiza_TextQualityInfo ();} 
		else {
			TextQualitySettingsInfo.gameObject.SetActive (false);}
	}


	void OcultarObjetos(bool Ocultar){
		for(int i=0; i<ObjetosAOcultar.Length; i++){
			if(ObjetosAOcultar[i])
				ObjetosAOcultar [i].SetActive (!Ocultar);}
	}


	void GetQualityInfo(){
		Qualitys = QualitySettings.names;
		CurrentQuality = QualitySettings.GetQualityLevel ().ToString ();
	}


	void Actualiza_TextQualityInfo(){
		TextQualitySettingsInfo.text = string.Empty;
		GetQualityInfo ();

		for(int i=0; i<Qualitys.Length; i++){
			TextQualitySettingsInfo.text += "Index: " + i.ToString() + "  Quality: " + Qualitys [i] + "\n";}

		TextQualitySettingsInfo.text += "Current Quality Index: " + CurrentQuality;
	}


	public void OpenPanel(){
		Open = !Open;
		OcultarObjetos (Open);
		PanelGraphicSettings.SetActive (Open);
	}


	public void ClosePanel(){
		Open = false;
		OcultarObjetos (false);
		PanelGraphicSettings.SetActive (false);
	}


	public void ShowHideFPS(){
		FPShowed = !FPShowed;
		if (scriptDontDestroyFPS) {
			scriptDontDestroyFPS.MostrarFPSText (FPShowed);}
	}
	

	public void ChangeCurrentQuality(int QualityIndex){

		if (QualityIndex < Qualitys.Length && QualityIndex >= 0) {
			if (Qualitys [QualityIndex] != string.Empty) {
				QualitySettings.SetQualityLevel (QualityIndex, CambioDinamico_Expensivo);			
				QualitySet.SaveQuality (QualityIndex);}}

		if (QualitySettingsInfo) {
			Actualiza_TextQualityInfo ();}
	}


	void ActualizaDropdown_Lenguage(){
		//DropdownQualitySelector.options[0].text = TextManager.AsignaTexto("GraphicSettingsManager", "Fastest"); 
		//DropdownQualitySelector.options[1].text = TextManager.AsignaTexto("GraphicSettingsManager", "Fast"); 
		//DropdownQualitySelector.options[2].text = TextManager.AsignaTexto("GraphicSettingsManager", "Simple"); 
		//DropdownQualitySelector.options[3].text = TextManager.AsignaTexto("GraphicSettingsManager", "Good"); 
		//DropdownQualitySelector.options[4].text = TextManager.AsignaTexto("GraphicSettingsManager", "Beautiful"); 
		//DropdownQualitySelector.options[5].text = TextManager.AsignaTexto("GraphicSettingsManager", "Fantastic");
	}

}