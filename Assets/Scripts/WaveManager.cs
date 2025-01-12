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
                StartCoroutine(SpawnWave(currentWave));
                currentWaveIndex++;
            }
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
