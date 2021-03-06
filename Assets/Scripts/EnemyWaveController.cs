﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyWaveController : MonoBehaviour 
{
    [SerializeField]
    private List<WaveRound> WaveRounds;

    [SerializeField]
    private Transform worldTransform;

    private Dictionary<int, Enemy> enemiesInWave;
    private static int enemyIdCounter;
    private int enemyInRoundCount;
    private int currentRound;

    private void Awake()
    {
        enemyIdCounter = 0;
        enemyInRoundCount = 0;
        currentRound = 0;
        enemiesInWave = new Dictionary<int, Enemy>();
    }

    private void Start()
    {
    }

    public void StartRound()
    {
        StartEnemies(WaveRounds[currentRound]);
    }

    private void StartEnemies(WaveRound round)
    {
        StartCoroutine(StartRound(round));
    }

    private IEnumerator StartRound(WaveRound round)
    {
        yield return new WaitForSeconds(round.RoundBeginDelay);

        for(int index = 0; index < round.EnemyWaves.Count; index++)
        {
            var wave = round.EnemyWaves[index];
            enemyInRoundCount += wave.NumberOfEnemies;
        }
        
        for(int index = 0; index < round.EnemyWaves.Count; index++)
        {
            var wave = round.EnemyWaves[index];
            StartCoroutine(StartWave(wave));
        }
    }

    private IEnumerator StartWave(EnemyWave wave)
    {
        yield return new WaitForSeconds(wave.WaveDelay);
        var enemies = EnemySpawner.Instance.SpawnEnemy(wave.NumberOfEnemies, wave.EnemyType, wave.wavePosition.position, Quaternion.identity);
        foreach(var enemy in enemies)
        {
            enemy.transform.parent = worldTransform;
            enemy.EnemyId = enemyIdCounter;
            enemiesInWave.Add(enemyIdCounter++, enemy);
            enemy.EnemyDeath += HandleEnemyDeath;
            yield return new WaitForSeconds(wave.TimeBetweenSpawn);
        }
    }


    private void HandleEnemyDeath(object sender, EnemyDeathEventArgs e)
    {
        var enemy = enemiesInWave[e.EnemyId];
        enemy.EnemyDeath -= HandleEnemyDeath;

        enemiesInWave.Remove(e.EnemyId);
        enemyInRoundCount--;

        if(enemyInRoundCount == 0)
        {
            Debug.Log("Round End");
            currentRound++;
            if(currentRound < WaveRounds.Count)
            {
                StartEnemies(WaveRounds[currentRound]);
                enemyIdCounter = 0;
                enemiesInWave.Clear();
            }
            else
            {
                StartGame.StartTheGame();
            }
        }
    }

}

public enum EnemyType
{
    Basic = 0,
    Drill = 1
}

[Serializable]
public class WaveRound
{
    public string Name;
    public List<EnemyWave> EnemyWaves;
    public float RoundBeginDelay;
}

[Serializable]
public class EnemyWave
{
    public EnemyType EnemyType;
    public int NumberOfEnemies;
    public float WaveDelay;
    public float TimeBetweenSpawn;
    public Transform wavePosition;
}
