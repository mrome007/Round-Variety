using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrillEnemyMovement : EnemyMovement
{
    [SerializeField]
    private float towardsRotation;

    [SerializeField]
    private float inRotation;

    private float currentRotation;
    private float towardsTimer = 3f;
    private Vector3 originalPosition;
    private float inTimer = 6f;
    private Vector3 drillRotation;
    private float outTimer = 3f;

    protected override void Start()
    {
        base.Start();
        transform.LookAt(Vector3.zero);
        currentRotation = towardsRotation;
        originalPosition = transform.position;
        drillRotation = Vector3.forward;
    }

    protected override void MoveTowards()
    {
        if(towardsTimer > 0f)
        {
            towardsTimer -= Time.deltaTime;
            var newPos = originalPosition + new Vector3(Random.Range(-0.2f,0.2f), Random.Range(-0.2f, 0.2f), 0f);
            transform.position = newPos;
            if(towardsTimer <= 0f)
            {
                transform.position = originalPosition;
                transform.LookAt(Vector3.zero);
            }
        }
        else
        {
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
        }
    }

    protected override void MoveIn()
    {
        inTimer -= Time.deltaTime;
        transform.position += transform.forward * 0.075f * moveSpeed * Time.deltaTime;

        if(inTimer <= 0f)
        {
            moveType = MoveType.Damage;
        }
    }

    protected override void MoveOut()
    {
        outTimer -= Time.deltaTime;
        transform.position += Vector3.up * 0.1f * moveSpeed * Time.deltaTime;

        if(outTimer <= 0f)
        {
            moveType = MoveType.Towards;
            outTimer = 3f;
            enemyCollider.enabled = true;
            transform.LookAt(Vector3.zero);
            drillRotation = Vector3.forward;
        }
    }

    protected override void Damage()
    {
        World.Instance.DamageWorld(EnemyType.Drill);
        moveType = MoveType.Done;
    }

    protected override void Update()
    {
        base.Update();

        if(moveType != MoveType.Done)
        {
            transform.Rotate(drillRotation * currentRotation * Time.deltaTime);
        }
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if(other.tag == "World")
        {
            moveType = MoveType.In;
            currentRotation = inRotation;
        }
        
        if(other.tag == "Player")
        {
            if(moveType != MoveType.In)
            {
                enemyCollider.enabled = false;
                moveType = MoveType.Out;
                drillRotation = Vector3.up;
            }
        }

        if(other.tag == "Projectile")
        {
            Destroy(other.gameObject);
            if(moveType != MoveType.In)
            {
                moveType = MoveType.Out;
                enemyCollider.enabled = false;
                drillRotation = Vector3.up;
                outTimer = 2f;
            }
        }
    }
}
