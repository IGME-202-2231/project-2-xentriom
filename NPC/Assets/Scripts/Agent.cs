using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PhysicsObject))]
public abstract class Agent : MonoBehaviour
{
    [SerializeField] List<SpriteRenderer> obstacleList;
    [SerializeField] protected AgentSpawner agentSpawner;
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

    protected void Wander(float weight = 5f)
    {
        float wanderChange = maxWanderChangePS * Time.deltaTime;
        wanderAngle += Random.Range(-wanderChange, wanderChange);

        wanderAngle = Mathf.Clamp(wanderAngle, -maxWanderAngle, maxWanderAngle);

        Vector3 wanderTarget = Quaternion.Euler(0, 0, wanderAngle) * physicsObject.Direction.normalized + physicsObject.Position;

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

    public Vector3 CalcFuturePosition(float time)
    {
        return physicsObject.Position + physicsObject.Velocity * time;
    }

    protected void AvoidObstacle(float time, float weight)
    {
        Vector3 totalAvoidForce = Vector3.zero;

        foreach (var obstacle in agentSpawner.ObstacleList)
        {
            Vector3 agentToObstacle = obstacle.transform.position - transform.position;
            float rightDot = 0, forwardDot = 0;

            //find whether if the obstacle is in front or behind agent.
            //positive if in front, negative if behind
            forwardDot = Vector3.Dot(physicsObject.Direction, agentToObstacle);

            //because it's wandering, future position is needed
            Vector3 futurePos = CalcFuturePosition(time);

            float dist = Vector3.Distance(transform.position, futurePos) + physicsObject.Radius;

            //if in front of me
            if (forwardDot >= 0)
            {
                //within the box in front of us (give obstacle a radius)
                //if (forwardDot <= dist + obstacle.radius)
                if (forwardDot <= dist + 5)
                {
                    // how far left/right?
                    rightDot = Vector3.Dot(transform.right, agentToObstacle);

                    //Vector3 steeringForceR = transform.right / Mathf.Abs(forwardDot/dist) * physicsObject.MaxForce;
                    Vector3 steeringForce = transform.right * (1 - forwardDot / dist) * maxForce;

                    // is the Obstacle withint the safe box width?
                    // if (Mathf.Abs(rightDot) <= (physicsObject.Radius + obstacle.Radius) || Mathf.Abs(rightDot) >= (physicsObject.Radius + obstacle.Radius))
                    if (Mathf.Abs(rightDot) <= (physicsObject.Radius + 5) || Mathf.Abs(rightDot) >= (physicsObject.Radius + 5))
                    {
                        if (rightDot < 0)
                        {
                            totalAvoidForce += steeringForce;
                        }
                        else if (rightDot > 0)
                        {
                            totalAvoidForce += -steeringForce;
                        }
                    }
                }
            }
        }
        totalForce += totalAvoidForce * maxForce * weight;
    }
}