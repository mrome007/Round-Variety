using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour 
{
    public int Health = 1000;

    #region Instance
    
    public static World Instance
    {
        get
        {
            if(instance == null)
            {
                instance = (World)FindObjectOfType(typeof(World));
            }
            return instance;
        }
    }
    
    private static World instance = null;
    
    #endregion

    public void DamageWorld(EnemyType type)
    {
        var damage = 0;
        switch(type)
        {
            case EnemyType.Basic:
                damage = 10;
                break;

            case EnemyType.Drill:
                damage = 50;
                break;

            default:
                break;
        }

        Health -= damage;
        if(Health <= 0)
        {
            //End Game;
            StartGame.StartTheGame();
        }
    }
}
