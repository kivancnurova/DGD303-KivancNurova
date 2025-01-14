using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public string waveName;
        public float startTime;
        public float endTime;
        public GameObject enemyPrefab;
        public int enemyCount;
    }

    public List<Wave> waves;
    public Transform player;
    public float spawnDistance;
    public float maxSpawnDistance;

    private int currentWaveIndex;
    public GameObject[] enemyPrefab;

    public TimerManager timerScript;

    void Awake() 
    {
        if(timerScript == null)
        {
            timerScript = FindObjectOfType<TimerManager>();    
        } 

        InitializeWaveIndex();
    }

    void Update()
    {
        float elapsedSeconds = timerScript.GetElapsedTime();

        if(currentWaveIndex < waves.Count)
        {
            Wave currentWave = waves[currentWaveIndex];

            if(elapsedSeconds >= currentWave.startTime && elapsedSeconds <= currentWave.endTime)
            {
                StartCoroutine(SpawnWave(currentWave));
                currentWaveIndex++;
            }
        }

        if(elapsedSeconds >= 150f && elapsedSeconds <= 180f && currentWaveIndex == waves.Count)
        {
            Debug.Log("Spawning mixed wave");
            StartCoroutine(SpawnMixedWave());
            currentWaveIndex++;
        }

    }

    void InitializeWaveIndex()
    {
        float elapsedSeconds = timerScript.GetElapsedTime();

        for (int i =0; i < waves.Count; i++)
        {

            if(elapsedSeconds >= waves[i].startTime && elapsedSeconds <= waves[i].endTime)
            {
                currentWaveIndex = i;
                break;
            }
        }

        if(elapsedSeconds >= 150f && elapsedSeconds <= 180f)
        {
            currentWaveIndex = waves.Count;
        }
    }

    IEnumerator SpawnWave(Wave wave)
    {
        Debug.Log("Spawning wave: " + wave.waveName);
        for(int i = 0; i < wave.enemyCount; i++)
        {
            Vector3 spawnPosition = GetRandomSpawnPosition();
            Instantiate(wave.enemyPrefab, spawnPosition, Quaternion.identity);

            yield return new WaitForSeconds(0.5f);
        }
    }

    IEnumerator SpawnMixedWave()
    {
        Debug.Log("Spawning mixed wave");
        int mixedEnemyCount = 30;

        for(int i = 0; i < mixedEnemyCount; i++)
        {
            GameObject randomEnemy = enemyPrefab[Random.Range(0, enemyPrefab.Length)];

            Vector3 spawnPosition = GetRandomSpawnPosition();
            Instantiate(randomEnemy, spawnPosition, Quaternion.identity);

            yield return new WaitForSeconds(0.5f);
        }
    }


    Vector3 GetRandomSpawnPosition()
    {
        Vector3 playerPosition = player.position;

        float angle = Random.Range(0, 360);
        float distance = Random.Range(spawnDistance / 2, maxSpawnDistance / 2);

        Vector3 spawnOffset = new Vector3(
            Mathf.Cos(angle * Mathf.Deg2Rad) * distance,
            Mathf.Sin(angle * Mathf.Deg2Rad) * distance,
            0f
        );

        spawnOffset.z = 0f;

        return playerPosition + spawnOffset;
        
    }










}
