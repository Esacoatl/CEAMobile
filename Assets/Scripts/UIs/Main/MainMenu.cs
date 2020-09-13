using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    /*public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }*/

    public void PlayArroyoShoot_1Loader()
    {
        SceneManager.LoadScene("Loader_Shooter");
    }
    public void PlayArroyoShoot_1()
    {
        SceneManager.LoadScene("1_ArroyoShoot");
    }


    public void PlayBuscador_1()
    {
        SceneManager.LoadScene("1_Buscador");
    }

    public void PlayBuscador_1Loader()
    {
        SceneManager.LoadScene("Loader_Buscador");
    }
}
