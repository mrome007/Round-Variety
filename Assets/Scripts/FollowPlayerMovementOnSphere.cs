using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerMovementOnSphere : MonoBehaviour 
{   
    [SerializeField]
    private float speed;

    [SerializeField]
    private float radius;

    [SerializeField]
    private float playerSizeOffset;

    [SerializeField]
    private Transform player;

    private float azimuth;
    private float elevation;
    private float movementVector;

    private void Awake()
    {
        radius += playerSizeOffset;
        azimuth = Random.Range(0f, 2f * Mathf.PI);
        elevation = Random.Range(0f, 2f * Mathf.PI);
    }

    private void Start()
    {

    }

    private void Update()
    {
        var target = player.transform.position - transform.position;

        MoveOnSphere(target.normalized.x, target.normalized.y);
    }

    private void MoveOnSphere(float horizontalAngle, float verticalAngle)
    {
        azimuth += horizontalAngle * speed * Time.deltaTime;
        elevation += verticalAngle * speed * Time.deltaTime;

        azimuth = ClampAngle(azimuth);
        elevation = ClampAngle(elevation);

        var x = radius * Mathf.Cos(elevation) * Mathf.Cos(azimuth);
        var y = radius * Mathf.Sin(elevation);
        var z = radius * Mathf.Cos(elevation) * Mathf.Sin(azimuth);

        transform.position = new Vector3(x, y, z);
        transform.LookAt(Vector3.zero);
    }

    private float ClampAngle(float angle)
    {
        while(angle >= 2f * Mathf.PI)
        {
            angle -= 2f * Mathf.PI;
        }

        return angle;
    }    
}
