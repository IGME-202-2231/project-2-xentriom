using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wander : Agent
{
    [Min(1f)] public float wanderWeight = 1f;
    [Min(1f)] public float stayInBoundWeight = 3f;

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    protected override void CalcSteeringForces()
    {
        Wander(wanderWeight);
        StayInBounds(stayInBoundWeight);
    }
}
