using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerMovementOnSphere : MovementOnSphere
{   
    [SerializeField]
    private Transform player;

    protected override void Awake()
    {
        base.Awake();

        azimuth = Random.Range(0f, 2f * Mathf.PI);
        elevation = Random.Range(0f, 2f * Mathf.PI);
    }

    protected override void Update()
    {
        var target = player.transform.position - transform.position;
        MoveOnSphere(target.normalized.x, target.normalized.y);
    }
}
