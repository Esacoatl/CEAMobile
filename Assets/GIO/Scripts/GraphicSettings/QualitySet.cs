using UnityEngine;
using System.Collections;

public class QualitySet : MonoBehaviour {

	public int InitialQualityIndex = 2;
	public bool CambioDinamico_Expensivo = true;

	void Start () {
		QualitySettings.SetQualityLevel (LoadQuality(), CambioDinamico_Expensivo);}

	int LoadQuality(){
		return PlayerPrefs.GetInt ("QualityLevelIndex", InitialQualityIndex);}
	
	public static void SaveQuality(int Index){
		PlayerPrefs.SetInt ("QualityLevelIndex", Index);}

}