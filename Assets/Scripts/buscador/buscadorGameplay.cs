using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class buscadorGameplay : MonoBehaviour
{
    //public int enemysCount = 3;
    public float timeEnemySpawn;
    public int waterLife;
    public int enemySpawnCount;
    public int enemyDeadCorpseCount;
    float tempEnemyTime;
    public bool gameover = false;
    public bool winLevel = false;
    public bool spawnEnd = false;

    public GameObject gameoverScreen;
    public GameObject winScreen;
    public GameObject[] objectsLife = new GameObject[4];
    public GameObject[] objectsEnemys = new GameObject[3];

    // Start is called before the first frame update
    void Start()
    {
        waterLife = 3;
        timeEnemySpawn = 7f;
        tempEnemyTime = timeEnemySpawn;
        enemySpawnCount = objectsEnemys.Length;
        enemyDeadCorpseCount = objectsEnemys.Length;
        enemySpawnCount = enemySpawnCount - 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (!spawnEnd)
        {
            timeEnemySpawn -= Time.deltaTime;
            if (timeEnemySpawn < 0)
            {
                objectsEnemys[enemySpawnCount - 1].SetActive(true);
                timeEnemySpawn = tempEnemyTime;
                enemySpawnCount--;
                if (enemySpawnCount <= 0)
                {
                    spawnEnd = true;
                }
            }
        }

    }

    public void BichoToWater()
    {
        objectsLife[waterLife].SetActive(false);
        waterLife--;
        objectsLife[waterLife].SetActive(true);
        if (waterLife <= 0)
        {
            gameover = true;
            gameoverScreen.SetActive(true);
        }
    }

    public void BichoDead()
    {
        enemyDeadCorpseCount--;
        if (enemyDeadCorpseCount <= 0)
        {
            winLevel = true;
            winScreen.SetActive(true);
        }
    }

    // win

    public void WinLoad()
    {
        StartCoroutine(WaitCoroutine());
    }

    public void WinLoadSceneMain()
    {
        SceneManager.LoadScene("MainMenu");
    }

    IEnumerator WaitCoroutine()
    {
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(3);
        WinLoadSceneMain();
    }

    // gameover

    public void GameOverLoad()
    {
        StartCoroutine(WaitCoroutineGameover());
    }

    public void GameOverLoadSceneMain()
    {
        SceneManager.LoadScene("MainMenu");
    }

    IEnumerator WaitCoroutineGameover()
    {
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(3);
        GameOverLoadSceneMain();
    }
}
