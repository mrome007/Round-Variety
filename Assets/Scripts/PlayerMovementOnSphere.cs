using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementOnSphere : MovementOnSphere 
{
    protected override void Update()
    {
        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");

        MoveOnSphere(h, v);
    }
}
