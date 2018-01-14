using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : Enemy
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Projectile")
        {
            var projectile = other.GetComponent<Projectile>();
            if(projectile != null)
            {
                Health -= projectile.Damage;
                if(Health <= 0f)
                {
                    GetComponent<Collider>().enabled = false;
                    Destroy(gameObject, 1.5f);
                }
            }
        }
    }

    private void OnDestroy()
    {
        KillEnemy();
    }
}
