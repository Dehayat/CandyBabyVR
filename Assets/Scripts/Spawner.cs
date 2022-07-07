using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] spawn;


    public float spawnerDelay = 3f;
    public float SpawnInterval = 5f;
    public int spawnCount = 2;
    public bool isSpawning = false;
    public int waveToStartMulti = 3;
    public Vector3[] spawnPositions;

    private float lastSpawn = 0;

    private bool nextAdd = true;
    private int currentSpawn = 0;

    private void Awake()
    {
        lastSpawn = -SpawnInterval;
    }
    private void Update()
    {
        if (spawnCount == 0 || !isSpawning) return;
        if (lastSpawn + SpawnInterval < Time.time)
        {
            currentSpawn = UnityEngine.Random.Range(0, spawn.Length);
            if (nextAdd)
            {
                lastSpawn = Time.time + spawnerDelay;
                nextAdd = false;
                return;
            }
            if (currentWave < waveToStartMulti || spawnPositions.Length < 1)
            {
                SpawnEnemy(transform.position);
            }
            else
            {
                for (int i = 0; i < spawnPositions.Length && spawnCount > 0; i++)
                {
                    SpawnEnemy(spawnPositions[i]);
                }
            }

            if (spawnCount == 0)
            {
                nextAdd = true;
            }
            lastSpawn = Time.time;
        }
    }
    private void SpawnEnemy(Vector3 position)
    {
        parentSpawner.Add(Instantiate(spawn[currentSpawn], position, Quaternion.identity));
        spawnCount--;
    }

    private List<GameObject> parentSpawner;
    [HideInInspector]
    public int currentWave;

    internal void Listen(List<GameObject> parent)
    {
        parentSpawner = parent;
    }

    private void OnDrawGizmosSelected()
    {
        for (int i = 0; i < spawnPositions.Length; i++)
        {
            Gizmos.DrawWireCube(spawnPositions[i], Vector3.one);
        }
    }
}
