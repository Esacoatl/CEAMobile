using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour
{
    public static bool GameIsPause = false;

    public GameObject pauseMenuUI;
    public GameObject playerUI;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(GameIsPause)
            {
                Resume();
            } else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        //playerUI.SetActive(true);
        Time.timeScale = 1f;
        GameIsPause = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        //playerUI.SetActive(false);
        Time.timeScale = 0f;
        GameIsPause = true;
    }

    public void ExitAction()
    {
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync("Loader_1");
    }

}
