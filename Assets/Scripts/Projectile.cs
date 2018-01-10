using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour 
{
    [SerializeField]
    private float speed;

    private float lifeSpan = 2.5f;

    private void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
        lifeSpan -= Time.deltaTime;
        if(lifeSpan <= 0f)
        {
            Destroy(gameObject);
        }
    }
}
