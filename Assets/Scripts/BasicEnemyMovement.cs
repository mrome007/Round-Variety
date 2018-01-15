using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyMovement : EnemyMovement
{
    [SerializeField]
    private float radius;

    [SerializeField]
    private float offset;

    [SerializeField]
    private float burrowOffset;

    [SerializeField]
    private float inIncrement = 0.15f;

    [SerializeField]
    private int burrowToOutHits;

    private float theta;
    private float direction;
    private Vector3 position;
    private float inTimer;
    private float outTimer;
    private float burrowRadius;
    private float burrowedRadius;

    protected override void Start()
    {
        base.Start();
        transform.LookAt(Vector3.zero);
        position = Vector3.zero;
        burrowRadius = radius;
        inTimer = 2f;
        outTimer = 2f;
        burrowedRadius = burrowRadius - burrowOffset;
    }

    protected override void MoveIn()
    {
        if(inTimer > 0f)
        {
            inTimer -= Time.deltaTime;
            BasicMoveIn();
        }
        else
        {
            BasicMoveInBurrow();
        }
    }

    protected override void MoveOut()
    {
        outTimer -= Time.deltaTime;
        if(outTimer <= 0f)
        {
            outTimer = 2f;
            moveType = MoveType.Towards;
            transform.LookAt(Vector3.zero);
            enemyCollider.enabled = true;
        }
        transform.position += transform.up * moveSpeed * Time.deltaTime;
        transform.Rotate(Vector3.up * 20f * Time.deltaTime);
    }

    protected override void MoveTowards()
    {
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }

    protected override void Damage()
    {
        enemyCollider.enabled = false;
        World.Instance.DamageWorld(EnemyType.Basic);
        var enemy = GetComponent<Enemy>();
        enemy.KillEnemy();
        moveType = MoveType.Done;
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if(other.tag == "World")
        {
            moveType = MoveType.In;
            theta = GetAngleBetweenPos();
            inTimer = Random.Range(1f, 3f);
            direction = Random.Range(0,2) == 0 ? 1f : -1f;
        }
        
        if(other.tag == "Player")
        {
            moveType = MoveType.Out;
            enemyCollider.enabled = false;
        }

        if(other.tag == "Projectile")
        {
            Destroy(other.gameObject);

            if(moveType == MoveType.In)
            {
                burrowToOutHits--;
                if(burrowToOutHits <= 0)
                {
                    moveType = MoveType.Out;
                    outTimer = 4f;
                    enemyCollider.enabled = false;
                }
            }
            else
            {
                moveType = MoveType.Out;
                enemyCollider.enabled = false;
            }
        }
    }

    private void BasicMoveIn()
    {
        theta += inIncrement * direction * moveSpeed * Time.deltaTime;

        var x = (radius + offset) * Mathf.Cos(theta);
        var y = (radius + offset) * Mathf.Sin(theta);

        position.x = x;
        position.y = y;

        transform.LookAt(position, y < 0f ? Vector3.down : Vector3.up);
        transform.position = position;
    }

    private void BasicMoveInBurrow()
    {
        burrowRadius -= Time.deltaTime * 0.1f;  
        if(burrowRadius <= burrowedRadius)
        {
            //Damage the world here.
            enemyCollider.enabled = false;
            moveType = MoveType.Damage;
            return;
        }

        var randomOffset = Random.Range(-0.01f, 0.01f);
        var x = (burrowRadius + offset) * Mathf.Cos(theta + randomOffset);
        var y = (burrowRadius + offset) * Mathf.Sin(theta + randomOffset);
        
        position.x = x;
        position.y = y;

        transform.position = position;
    }

    private float GetAngleBetweenPos()
    {
        var uVector = transform.position;
        var vVector = Vector3.right;

        var angle = Mathf.Atan2(uVector.y - vVector.y, uVector.x - vVector.x);

        return angle;
    }
}
