using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovementOnSphere : MonoBehaviour 
{
    [SerializeField]
    protected float speed;

    [SerializeField]
    protected float radius;

    [SerializeField]
    protected float playerSizeOffset;

    [SerializeField]
    private Transform target;

    protected float azimuth;
    protected float elevation;
    protected float movementVector;

    protected virtual void Awake()
    {
        radius += playerSizeOffset;
        azimuth = 0f;
        elevation = 0f;
    }

    protected virtual void Update()
    {
        
    }

    protected virtual void MoveOnSphere(float horizontalAngle, float verticalAngle)
    {
        azimuth += horizontalAngle * speed * Time.deltaTime;
        elevation += verticalAngle * speed * Time.deltaTime;

        azimuth = ClampAngle(azimuth);
        elevation = ClampAngle(elevation);

        var x = radius * Mathf.Cos(elevation) * Mathf.Cos(azimuth);
        var y = radius * Mathf.Sin(elevation);
        var z = radius * Mathf.Cos(elevation) * Mathf.Sin(azimuth);

        transform.position = new Vector3(x, y, z);
        transform.LookAt(target.transform.position, transform.up);

    }

    protected virtual float ClampAngle(float angle)
    {
        while(angle >= 2f * Mathf.PI)
        {
            angle -= 2f * Mathf.PI;
        }

        return angle;
    }  
}
