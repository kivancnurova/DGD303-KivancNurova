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
    private List<GameObject> previousWaveEnemies = new List<GameObject>();  

    public TimerManager timerScript;

    void Awake() 
    {
        if(timerScript == null)
        {
            timerScript = FindObjectOfType<TimerManager>();    
        } 
    }

    void Update()
    {
        float elapsedSeconds = timerScript.GetElapsedTime();

        if(currentWaveIndex < waves.Count)
        {
            Wave currentWave = waves[currentWaveIndex];

            if(elapsedSeconds >= currentWave.startTime && elapsedSeconds <= currentWave.endTime)
            {
                if(!previousWaveEnemies.Contains(currentWave.enemyPrefab))
                {
                    previousWaveEnemies.Add(currentWave.enemyPrefab);
                }

                StartCoroutine(SpawnWave(currentWave));
                currentWaveIndex++;
            }
        }

        if(elapsedSeconds >= 150f && elapsedSeconds <= 180f && currentWaveIndex == waves.Count)
        {
            StartCoroutine(SpawnMixedWave());
            currentWaveIndex++;
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
            GameObject randomEnemy = previousWaveEnemies[Random.Range(0, previousWaveEnemies.Count)];

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
