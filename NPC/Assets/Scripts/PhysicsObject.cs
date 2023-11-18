using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObject : MonoBehaviour
{
    [SerializeField] Vector3 direction;
    [SerializeField] Vector3 velocity;
    [SerializeField] Vector3 acceleration;

    [SerializeField] float mass = 1;
    [SerializeField] float radius;

    private Vector3 camSize;

    public float Radius { get { return radius; } }
    public Vector3 Velocity { get { return velocity; } }
    public Vector3 Direction { get { return direction; } }
    public Vector3 Position { get { return transform.position; } set { transform.position = value; } }
    public Vector3 CamSize { get { return camSize; } }

    // Start is called before the first frame update
    void Start()
    {
        camSize.y = Camera.main.orthographicSize;
        camSize.x = camSize.y * Camera.main.aspect;

        direction = Random.insideUnitCircle.normalized;
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate the velocity for this frame
        velocity += acceleration * Time.deltaTime;

        CheckBounds();

        transform.position += velocity * Time.deltaTime;

        // Grab current direction from velocity
        if (velocity.sqrMagnitude > Mathf.Epsilon)
        {
            direction = velocity.normalized;
        }

        // Zero out acceleration
        acceleration = Vector3.zero;

        transform.rotation = Quaternion.LookRotation(Vector3.back, direction);
    }

    public void ApplyForce(Vector3 force)
    {
        acceleration += force / mass;
    }


    public void ApplyFriction(float coefficient)
    {
        Vector3 friction = velocity * -1;
        friction.Normalize();
        friction *= coefficient;

        ApplyForce(friction);
    }

    private void CheckBounds()
    {
        if (transform.position.x > camSize.x)
        {
            velocity.x *= -1;
        }
        if (transform.position.x < -camSize.x)
        {
            velocity.x *= -1;
        }

        if (transform.position.y > camSize.y)
        {
            velocity.y *= -1;
        }
        if (transform.position.y < -camSize.y)
        {
            velocity.y *= -1;
        }
    }
}
