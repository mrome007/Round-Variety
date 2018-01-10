using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFireProjectile : MonoBehaviour 
{
    [SerializeField]
    private Transform projectileFireTransform;

    [SerializeField]
    private Projectile projectileObject;

    [SerializeField]
    private float fireRate = 0.5f;

    private float fireTimer;

    private void Start()
    {
        fireTimer = 0f;
    }

    private void Update()
    {
        if(fireTimer > 0f)
        {
            fireTimer -= Time.deltaTime;
            return;
        }

        fireTimer = 0f;

        if(Input.GetMouseButtonDown(0))
        {
            fireTimer = fireRate;
            Instantiate(projectileObject, projectileFireTransform.position, projectileFireTransform.rotation);
        }
    }
}
