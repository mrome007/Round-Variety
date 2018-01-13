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

    private float theta;
    private float direction;
    private Vector3 position;
    private float inTimer;
    private float burrowRadius;
    private float burrowedRadius;

    protected override void Start()
    {
        base.Start();
        transform.LookAt(Vector3.zero);
        position = Vector3.zero;
        burrowRadius = radius;
        inTimer = Random.Range(1f, 3f);
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

    protected override void MoveTowards()
    {
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if(other.tag == "World")
        {
            moveType = MoveType.In;
            theta = GetAngleBetweenPos();
            direction = Random.Range(0,2) == 0 ? 1f : -1f;
        }
        
        if(other.tag == "Player")
        {
            moveType = MoveType.Out;
        }
    }

    private void BasicMoveIn()
    {
        theta += inIncrement * direction * moveSpeed * Time.deltaTime;

        var x = (radius + offset) * Mathf.Cos(theta);
        var y = (radius + offset) * Mathf.Sin(theta);

        position.x = x;
        position.y = y;

        transform.LookAt(position);
        transform.position = position;
    }

    private void BasicMoveInBurrow()
    {
        burrowRadius -= Time.deltaTime * 0.1f;  
        if(burrowRadius <= burrowedRadius)
        {
            return;
        }

        var x = (burrowRadius + offset) * Mathf.Cos(theta);
        var y = (burrowRadius + offset) * Mathf.Sin(theta);
        
        position.x = x;
        position.y = y;

        transform.position = position;
    }

    private float GetAngleBetweenPos()
    {
        var uVector = transform.position;
        var vVector = Vector3.right;
        
        var cosTheta = (Vector3.Dot(uVector, vVector) / (uVector.magnitude * vVector.magnitude));
        var angle = Mathf.Acos(cosTheta);
        return angle;
    }
}
