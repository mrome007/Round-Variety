using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementOnSphere : MonoBehaviour 
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private float radius;

    [SerializeField]
    private float playerSizeOffset;

    private float azimuth;
    private float elevation;
    private float movementVector;

    private void Awake()
    {
        radius += playerSizeOffset;
        azimuth = 0f;
        elevation = 0f;
    }

    private void Start()
    {
        transform.position = new Vector3(radius, 0f, 0f);
    }

    private void Update()
    {
        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");

        MoveOnSphere(h, v);
        transform.LookAt(Vector3.zero);
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
