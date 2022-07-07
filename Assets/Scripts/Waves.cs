using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Waves : MonoBehaviour
{
    public int currentWave = 0;
    public static Waves instance;

    [SerializeField]
    private Spawner[] spawners;
    [SerializeField]
    private int baseSpawn = 3;
    [SerializeField]
    private float spawnInterval = 5;
    [SerializeField]
    private int extraSpawn = 2;
    [SerializeField]
    private float timeBetweenWaves = 5f;
    [SerializeField]
    private Text text;
    [SerializeField]
    private GameObject GameOverPanel;
    [SerializeField]
    private Text scoreText;
    [SerializeField]
    private Text waveText;

    List<GameObject> enemies = new List<GameObject>();

    private bool waitForNextWave = false;
    private bool gameIsRunning = true;
    private bool loadingScene = false;
    private int enemiesKilled = 0;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Time.timeScale = 1;
        StartWave();
    }

    private void StartWave()
    {
        currentWave = 1;
        for (int i = 0; i < spawners.Length; i++)
        {
            spawners[i].Listen(enemies);
            spawners[i].SpawnInterval = spawnInterval;
            spawners[i].isSpawning = true;
            spawners[i].currentWave = currentWave;
            if (i == 0)
            {
                spawners[i].spawnCount = baseSpawn;
            }
            else
            {
                spawners[i].spawnCount = 0;
            }
        }
        StartCoroutine(FirstWave());
    }
    IEnumerator FirstWave()
    {
        text.text = "Wave " + currentWave;
        yield return new WaitForSeconds(1f);
        text.gameObject.SetActive(true);
        yield return new WaitForSeconds(timeBetweenWaves);
        text.gameObject.SetActive(false);
    }

    private float gameFinished = float.PositiveInfinity;
    public void GameOver()
    {
        Time.timeScale = 0;
        gameIsRunning = false;
        for (int i = 0; i < spawners.Length; i++)
        {
            spawners[i].gameObject.SetActive(false);
        }
        foreach (var enemy in enemies)
        {
            if (enemy != null)
            {
                Destroy(enemy);
            }
        }
        FindObjectOfType<Shooter>().canControl = false;
        scoreText.text = enemiesKilled.ToString();
        waveText.text = currentWave.ToString();
        //GameOverPanel.SetActive(true);
        GameOverPanel.GetComponent<Animator>().SetTrigger("End");
        gameFinished = Time.unscaledTime;
    }

    private void Update()
    {
        if (!gameIsRunning)
        {
            if (Time.unscaledTime < gameFinished + 2) return;
            if (Input.GetMouseButtonDown(0) && !loadingScene)
            {
                loadingScene = true;
                SceneManager.LoadScene(1);
            }
            if (Input.GetMouseButtonDown(1))
            {
                Application.Quit();
            }
            return;
        }
        foreach (var enemy in enemies)
        {
            if (enemy == null)
            {
                enemies.Remove(enemy);
                enemiesKilled++;
                if (enemies.Count == 0 && !waitForNextWave)
                {
                    StartCoroutine(RestartWave());
                }
                return;
            }
        }
    }
    IEnumerator RestartWave()
    {
        waitForNextWave = true;
        currentWave++;
        text.text = "Wave " + currentWave;
        yield return new WaitForSeconds(1);
        text.gameObject.SetActive(true);
        yield return new WaitForSeconds(timeBetweenWaves);
        text.gameObject.SetActive(false);
        waitForNextWave = false;
        for (int i = 0; i < spawners.Length; i++)
        {
            if (currentWave == 2 && i > 1) break;
            spawners[i].spawnCount = baseSpawn + extraSpawn * (currentWave - 1);
            spawners[i].currentWave = currentWave;
        }

    }

}
