using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seek : Agent
{
    [SerializeField] GameObject target;

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    protected override void CalcSteeringForces()
    {
        Seek(target);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;

        Gizmos.DrawLine(transform.position, physicsObject.Velocity + transform.position);
    }
}