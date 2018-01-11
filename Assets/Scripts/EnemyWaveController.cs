using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyWaveController : MonoBehaviour 
{
    [SerializeField]
    private List<WaveRound> WaveRounds;

    private void Start()
    {
        StartCoroutine(StartWave());
    }

    private IEnumerator StartWave()
    {
        var wave = WaveRounds.FirstOrDefault();
        var enemies = EnemySpawner.Instance.SpawnEnemy(wave.EnemyWaves[0].NumberOfEnemies, EnemyType.Kamikaze, new Vector3(0f, -10f, 0f), Quaternion.identity);
        foreach(var enemy in enemies)
        {
            yield return new WaitForSeconds(wave.EnemyWaves[0].TimeBetweenSpawn);
        }
    }
}

public enum EnemyType
{
    Kamikaze = 0,
    UFO = 1
}

[Serializable]
public class WaveRound
{
    public List<EnemyWave> EnemyWaves;
}

[Serializable]
public class EnemyWave
{
    public EnemyType EnemyType;
    public int NumberOfEnemies;
    public float WaveDelay;
    public float TimeBetweenSpawn;
}
