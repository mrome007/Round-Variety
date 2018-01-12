using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyDeathEventArgs : EventArgs
{
    public int EnemyId { get; private set; }

    public EnemyDeathEventArgs(int id)
    {
        EnemyId = id;
    }
}
