using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public enum SpawnState
    {
        SPAWNING,
        WAITING,
        COUNTING
    };
    
    [System.Serializable]
    public class Wave
    {
        public string name;
        public GameObject enemy;
        public GameObject Enemy2;
        public GameObject Boss;
        public GameObject powerUp;
        public int BadieCount;
        public int BigBadieCount;
        public int BossCount;
        public float rate;
    }
    
    public Wave[] Waves;
    public SpawnState State = SpawnState.COUNTING;
    public Transform[] EnemySpawnPoints;
    public Transform[] PowerupSpawnPoints;
    public float TimeBetweenWaves = 3f;
    private int _nextWave = 0;
    private float _waveCountdown;
    private float _searchCountdown =1f;

     void Start()
     {
         _waveCountdown = TimeBetweenWaves;
         
         if (EnemySpawnPoints.Length == 0)
         {
             Debug.LogError("No enemy spawn points referenced");
         }
         if (PowerupSpawnPoints.Length == 0)
         {
             Debug.LogError("No powerup spawn points referenced");
         }
     }

    private void Update()
    {

        if (State == SpawnState.WAITING)
        {
            //check if enemy are still alive
            if (!EnemyIsAlive())
            {
                WaveCompleted();
            }
            else
            {
                return;
            }
        }
        
        if (_waveCountdown <= 0)
        {
            if (State != SpawnState.SPAWNING)
            {
                StartSpawning(Waves[_nextWave]);
                //StartCoroutine(SpawnWave(Waves[_nextWave]));
            }
        }
        else
        {
            _waveCountdown -= Time.deltaTime;
        }
    }

    void WaveCompleted()
    {
        State = SpawnState.COUNTING;
        _waveCountdown = TimeBetweenWaves;

        if (_nextWave + 1 > Waves.Length - 1)
        {
            _nextWave = 0;
        }
        else
        {
            _nextWave++;
        }
    }
    
    bool EnemyIsAlive()
    {
        _searchCountdown -= Time.deltaTime;
        if (_searchCountdown <= 0f)
        {
            _searchCountdown = 1f;

            int enemiesRemaining = GameObject.FindGameObjectsWithTag("ENEMY").Length;
            Debug.Log("Enemies Remaining: " + enemiesRemaining);
            if (GameObject.FindGameObjectWithTag("ENEMY") == null)
            {
                return false;
            }
        }
        return true;
    }

    private bool _badiesDone = false;
    private bool _bigBadiesDone = false;
    private bool _bossDone = false;

    void StartSpawning(Wave wave)
    {
        StartCoroutine(AttemptWaitTransistion(wave));
        StartCoroutine(SpawnBadiesWave(wave));
        StartCoroutine(SpawnBigBadiesWave(wave));
        StartCoroutine(SpawnBossWave(wave));
    }

    IEnumerator AttemptWaitTransistion(Wave wave)
    {
        State = SpawnState.SPAWNING;
        while (!_badiesDone || !_bigBadiesDone || !_bossDone)
        {
            yield return null;
        }

        _badiesDone = false;
        _bigBadiesDone = false;
        _bossDone = false;
        
        State = SpawnState.WAITING;
        SpawnPowerup(wave.powerUp.transform);
    }

    IEnumerator SpawnBadiesWave(Wave wave)
    {
        for (int i = 0; i < wave.BadieCount; i++)
        {
            SpawnEnemy(wave.enemy.transform);
         

            yield return new WaitForSeconds(1f/wave.rate);
        }
        _badiesDone = true;
    }

    IEnumerator SpawnBigBadiesWave(Wave wave)
    {
        for (int i = 0; i < wave.BigBadieCount; i++)
        {
            SpawnEnemy(wave.Enemy2.transform);

            yield return new WaitForSeconds(1f/wave.rate);
        }
        _bigBadiesDone = true;
    }

    IEnumerator SpawnBossWave(Wave wave)
    {
        for (int i = 0; i < wave.BossCount; i++)
        {
            SpawnEnemy(wave.Boss.transform);

            yield return new WaitForSeconds(1f/wave.rate);
        }
        _bossDone = true;

    }

    void SpawnEnemy(Transform enemy)
    {
        Transform _sp = EnemySpawnPoints[Random.Range(0, EnemySpawnPoints.Length)];
        Instantiate(enemy, _sp.position, _sp.rotation);
    }

    void SpawnPowerup(Transform powerUp)
    {
        Transform _sp = PowerupSpawnPoints[Random.Range(0, PowerupSpawnPoints.Length)];
        Instantiate(powerUp, _sp.position, _sp.rotation);
    }
}