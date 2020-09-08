using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class shooterGameplay : MonoBehaviour
{
    //public int enemysCount = 3;
    public float timeEnemySpawn;
    public int waterLife;
    public int enemySpawnCount;
    float tempEnemyTime;
    public bool gameover = false;
    public bool spawnEnd = false;

    public GameObject gameoverScreen;
    public GameObject[] objectsLife = new GameObject[4];
    public GameObject[] objectsEnemys = new GameObject[3];

    // Start is called before the first frame update
    void Start()
    {
        waterLife = 3;
        timeEnemySpawn = 10f;
        tempEnemyTime = timeEnemySpawn;
        enemySpawnCount = objectsEnemys.Length;
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

    public void BichoAlAgua()
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

    public void GameOverLoad()
    {
        StartCoroutine(WaitCoroutine());
    }

    public void GameOverLoadSceneMain()
    {
        SceneManager.LoadScene("MainMenu");
    }

    IEnumerator WaitCoroutine()
    {
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(3);
        GameOverLoadSceneMain();
    }
}
