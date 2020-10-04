using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class SplashLoadingScreen : MonoBehaviour {

	/*
	[Header("Loading Screen Scene")]
	public string LoadingScreenName;
	*/

	[Header("Loading Visuals")]
	public Image loadingIcon;
	public Image loadingDoneIcon;
	public Text loadingText;
	public Image progressBar;
	public Image fadeOverlay;

	[Header("Timing Settings")]
	public float waitOnLoadEnd = 0.25f;
	public float fadeDuration = 0.25f;

	[Header("Loading Settings")]
	public LoadSceneMode loadSceneMode = LoadSceneMode.Single;
	public ThreadPriority loadThreadPriority;

	[Header("Other")]
	// If loading additive, link to the cameras audio listener, to avoid multiple active audio listeners
	public AudioListener audioListener;

	AsyncOperation operation;
	Scene currentScene;

	bool HaTerminadoDeCargar;
	bool RotandoLoadingIcon = true;

	public static bool START = true;
	public static bool END = false;
	public static bool UsandoASYNC = false;

	public static string sceneToLoad;
	static string LoadScreenNameStatic = "03_Loadig";

	public static void LoadScene(string SceneName) {				
		Application.backgroundLoadingPriority = ThreadPriority.High;
		sceneToLoad = SceneName;
		SceneManager.LoadScene (LoadScreenNameStatic);
	}

	void Start() {
		if (sceneToLoad == null)
			return;

		UsandoASYNC = true;

		HaTerminadoDeCargar = false;
		fadeOverlay.gameObject.SetActive(true); // Making sure it's on so that we can crossfade Alpha
		currentScene = SceneManager.GetActiveScene();
		StartCoroutine(LoadAsync(sceneToLoad));
	}

	private IEnumerator LoadAsync(string SceneName) {
		ShowLoadingVisuals();

		yield return null; 

		FadeIn();
		StartOperation(SceneName);

		float lastProgress = 0f;

		// operation does not auto-activate scene, so it's stuck at 0.9
		while (DoneLoading() == false) {
			yield return null;

			if (Mathf.Approximately(operation.progress, lastProgress) == false) {
				progressBar.fillAmount = operation.progress;
				lastProgress = operation.progress;
			}
		}


		if (loadSceneMode == LoadSceneMode.Additive) {
			audioListener.enabled = false;
		}


		ShowCompletionVisuals();

		yield return new WaitUntil (() => HaTerminadoDeCargar == true);  // WaitForSeconds(waitOnLoadEnd);

		HaTerminadoDeCargar = false;
		UsandoASYNC = false;
		END = false;
		FadeOut();

		//yield return new WaitForSeconds(fadeDuration);

		if (loadSceneMode == LoadSceneMode.Additive)
			SceneManager.UnloadSceneAsync(currentScene.name);
		else
			operation.allowSceneActivation = true;
	}


	private void StartOperation(string SceneName) {
		Application.backgroundLoadingPriority = loadThreadPriority;
		operation = SceneManager.LoadSceneAsync(SceneName, loadSceneMode);

		if (loadSceneMode == LoadSceneMode.Single)
			operation.allowSceneActivation = false;
	}

	private bool DoneLoading() {
		return (loadSceneMode == LoadSceneMode.Additive && operation.isDone) || (loadSceneMode == LoadSceneMode.Single && operation.progress >= 0.9f); 
	}


	public void ConfirmarLoadingEnd(){
		HaTerminadoDeCargar = true;
		END = true;
		START = false;
	}


	void FadeIn() {
		fadeOverlay.CrossFadeAlpha(0, fadeDuration, true);
	}

	void FadeOut() {
		fadeOverlay.CrossFadeAlpha(1, fadeDuration, true);
	}

	void ShowLoadingVisuals() {
		loadingIcon.gameObject.SetActive(true);
		loadingDoneIcon.gameObject.SetActive(false);

		progressBar.fillAmount = 0f;
		loadingText.text = "Cargando...";

		//StartCoroutine (RotarLoadingIcon ());
	}

	void ShowCompletionVisuals() {
		RotandoLoadingIcon = false;

		loadingIcon.gameObject.SetActive(false);
		loadingDoneIcon.gameObject.SetActive(true);

		progressBar.fillAmount = 1f;
		loadingText.text = "";
	}

	IEnumerator RotarLoadingIcon(){
		while(RotandoLoadingIcon){
			loadingIcon.rectTransform.Rotate(Vector3.forward, -90.0f * Time.deltaTime);
			yield return new WaitForEndOfFrame ();}
	}
}