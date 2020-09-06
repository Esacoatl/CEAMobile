using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider slider;
    public Text progressText;
    public float transitionTime = 1f;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        LoadLevel(1);
    }

    IEnumerator WaitCoroutine(int sceneIndex)
    {
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(2);
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }

    //------------------------------------------------------------------by Index
    public void LoadLevel(int sceneIndex)
    {
        StartCoroutine(WaitCoroutine(sceneIndex));
    }

    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            slider.value = progress;
            progressText.text = progress * 100f + "%";
            yield return null;
        }
    }

    //------------------------------------------------------------------by name
    public void LoadLevelByName(string sceneName)
    {
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
            progressText.text = progress * 100f + "%";
            yield return null;
        }
    }
}
