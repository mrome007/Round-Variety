using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour 
{
    public event EventHandler EnemySpawned;
    public event EventHandler EnemyDeath;

    public virtual void SpawnEnemy()
    {
        var handler = EnemySpawned;
        if(handler != null)
        {
            handler(this, null);
        }
    }

    public virtual void KillEnemy()
    {
        var handler = EnemyDeath;
        if(handler != null)
        {
            handler(this, null);
        }
    }
}
