using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayerOnOrbit : MonoBehaviour 
{
    [SerializeField]
    private float radius;

    [SerializeField]
    private float sizeOffset;

    [SerializeField]
    private float speed;

    [SerializeField]
    private float idleSpeed;

    [SerializeField]
    private float idleIncrment = 0.15f;


    private float horizontalRadius;
    private float theta;
    private float direction;
    private Vector3 position;

    private void Start()
    {
        horizontalRadius = radius;
        direction = 1f;
        position = Vector3.zero;
    }

    private void Update()
    {
        var h = Input.GetAxis("Horizontal");

        ChangeHorizontalRadius();
        MoveOnOrbit(h);
    }

    private void MoveOnOrbit(float movement)
    {
        if(movement == 0)
        {
            theta += (idleIncrment * direction * idleSpeed * Time.deltaTime);
        }
        else
        {
            direction = movement > 0 ? 1f : -1f;
            theta += (movement * speed * Time.deltaTime);
        }

        var y = (radius + sizeOffset) * Mathf.Cos(theta);
        var x = (horizontalRadius + sizeOffset) * Mathf.Sin(theta);
        
        position.x = x;
        position.y = y;
        
        transform.position = position;
    }

    private void ChangeHorizontalRadius()
    {
        if(Input.GetKey(KeyCode.J))
        {
            if(horizontalRadius < radius * 3f)
            {
                horizontalRadius += 0.1f;
            }
        }

        if(Input.GetKey(KeyCode.K))
        {
            if(horizontalRadius > radius)
            {
                horizontalRadius -= 0.1f;
            }
        }
    }
}
