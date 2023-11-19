using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PhysicsObject))]
public abstract class Agent : MonoBehaviour
{
    [SerializeField] protected PhysicsObject physicsObject;
    [SerializeField] protected float maxForce = 10;
    [SerializeField] protected float maxSpeed = 10;
    private Vector3 totalForce = Vector3.zero;

    private float wanderAngle = 0;
    private float maxWanderAngle = 45;
    private float maxWanderChangePS = 10;

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        if (physicsObject == null)
        {
            physicsObject = GetComponent<PhysicsObject>();
        }
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        CalcSteeringForces();

        totalForce = Vector3.ClampMagnitude(totalForce, maxForce);
        physicsObject.ApplyForce(totalForce);

        totalForce = Vector3.zero;
    }

    protected abstract void CalcSteeringForces();

    protected void Seek(GameObject target)
    {
        Seek(target.transform.position);
    }

    protected void Seek(Vector3 targetPos, float weight = 1f)
    {
        Vector3 desiredVelocity = targetPos - physicsObject.Position;
        desiredVelocity = desiredVelocity.normalized * maxSpeed;

        Vector3 seekingForce = desiredVelocity - physicsObject.Velocity;

        totalForce += seekingForce * weight;
    }

    protected void Flee(GameObject target)
    {
        Flee(target.transform.position);
    }

    protected void Flee(Vector3 targetPos, float weight = 1f)
    {
        Vector3 desiredVelocity = physicsObject.Position - targetPos;
        desiredVelocity = desiredVelocity.normalized * maxSpeed;

        Vector3 fleeingForce = desiredVelocity - physicsObject.Velocity;

        totalForce += fleeingForce * weight;
    }

    //protected void Wander(float weight = 1f)
    //{
    //    float wanderChange = maxWanderChangePS * Time.deltaTime;
    //    wanderAngle += Random.Range(-wanderChange, wanderChange);

    //    wanderAngle = Mathf.Clamp(wanderAngle, -maxWanderAngle, maxWanderAngle);

    //    Vector3 wanderTarget = Quaternion.Euler(0, 0, wanderAngle) * physicsObject.Direction.normalized + physicsObject.Position;

    //    Seek(wanderTarget, weight);
    //}

    protected void Wander(float weight = 1f)
    {
        float wanderRadius = 2f; // Adjust the radius based on your needs
        float wanderDistance = 5f; // Adjust the distance based on your needs

        // Calculate the new wander angle
        wanderAngle += Random.Range(-maxWanderChangePS, maxWanderChangePS) * Time.deltaTime;

        // Calculate the position on the circle
        Vector3 circleOffset = new Vector3(Mathf.Cos(wanderAngle), Mathf.Sin(wanderAngle), 0f) * wanderRadius;

        // Calculate the final wander target
        Vector3 wanderTarget = physicsObject.Position + physicsObject.Direction.normalized * wanderDistance + circleOffset;

        Seek(wanderTarget, weight);
    }

    protected void StayInBounds(float weight = 1f)
    {
        Vector3 futurePosition = CalcFuturePosition(1);

        if (futurePosition.x <= -physicsObject.CamSize.x ||
            futurePosition.x >= physicsObject.CamSize.x ||
            futurePosition.y <= -physicsObject.CamSize.y ||
            futurePosition.y >= physicsObject.CamSize.y)
        {
            Seek(Vector3.zero, weight);
        }
    }

    //protected void StayInBounds(float weight = 1f)
    //{
    //    Vector3 randomTarget = new Vector3(
    //        Random.Range(-physicsObject.CamSize.x, physicsObject.CamSize.x),
    //        Random.Range(-physicsObject.CamSize.y, physicsObject.CamSize.y),
    //        0f);

    //    Seek(randomTarget, weight);
    //}


    public Vector3 CalcFuturePosition(float time)
    {
        return physicsObject.Position + physicsObject.Velocity * time;
    }
}