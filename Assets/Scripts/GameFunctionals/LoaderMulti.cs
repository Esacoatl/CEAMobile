using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LoaderMulti : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider slider;
    private static string sceneNameWorld;
    //public Text progressText;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        sceneNameWorld = PlayerPrefs.GetString("nextSceneName");
        //sceneNameWorld = GameWorldValues.sceneNameStr;
        LoadLevelByName(sceneNameWorld);
    }

    public void LoadLevelByName(string sceneName)
    {
        StartCoroutine(WaitCoroutine(sceneName));
    }

    IEnumerator WaitCoroutine(string sceneName)
    {
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(2);
        StartCoroutine(LoadAsynchronouslyByName(sceneName));
    }

    IEnumerator LoadAsynchronouslyByName(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            slider.value = progress;
            int realProg = (int)progress;
            //progressText.text = realProg * 100 + "%";
            yield return null;
        }
    }
}
