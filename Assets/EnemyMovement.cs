using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour 
{
    [SerializeField]
    protected float moveSpeed;

    public enum MoveType
    {
        Towards,
        In,
        Out
    }

    protected MoveType moveType;
    protected Collider enemyCollider;

    protected virtual void Awake()
    {
        enemyCollider = GetComponent<Collider>();
    }

    protected virtual void Start()
    {
        moveType = MoveType.Towards;
    }

    protected virtual void Update()
    {
        MoveEnemy();
    }

    protected virtual void MoveEnemy()
    {
        switch(moveType)
        {
            case MoveType.In:
                MoveIn();
                break;

            case MoveType.Out:
                MoveOut();
                break;

            case MoveType.Towards:
                MoveTowards();
                break;

            default:
                break;
        }
    }

    protected virtual void MoveIn()
    {

    }

    protected virtual void MoveOut()
    {

    }

    protected virtual void MoveTowards()
    {
        
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if(other.tag == "World")
        {
            moveType = MoveType.In;
        }

        if(other.tag == "Player")
        {
            moveType = MoveType.Out;
        }
    }
}
