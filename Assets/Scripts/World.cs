using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour 
{
    public int Health = 1000;

    [SerializeField]
    private AudioSource hit;

    [SerializeField]
    private Renderer renderer;

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
                damage = 25;
                break;

            default:
                break;
        }

        Health -= damage;
        var color = renderer.material.color;
        color.r = 1f -  (Health / 1000f);
        renderer.material.color = color;
        if(Health <= 0)
        {
            //End Game;
            StartGame.StartTheGame();
        }
    }

    public void PlayHitMarkter()
    {
        hit.Play();
    }
}
