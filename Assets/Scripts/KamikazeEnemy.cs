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

    public override void SpawnEnemy()
    {
        base.SpawnEnemy();

        transform.LookAt(Vector3.zero);
    }

    private void Update()
    {
        if(delayBeforeAttack > 0f)
        {
            delayBeforeAttack -= Time.deltaTime;
            return;
        }

        transform.position += transform.forward * speed * Time.deltaTime;
    }

    private void OnDestroy()
    {
        KillEnemy();
    }
}
