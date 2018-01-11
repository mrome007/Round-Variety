using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KamikazeEnemy : Enemy
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private float delayBeforeAttack;

    private void Start()
    {
        SpawnEnemy();
    }

    private void Update()
    {
        if(delayBeforeAttack > 0f)
        {
            delayBeforeAttack -= Time.deltaTime;
            return;
        }

        transform.Translate(transform.forward * speed * Time.deltaTime);
    }

    private void OnDestroy()
    {
        KillEnemy();
    }
}
