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
    private Vector2 spriteMin;
    private Vector2 spriteMax;

    public float Radius { get { return radius; } }
    public Vector3 Velocity { get { return velocity; } }
    public Vector3 Direction { get { return direction; } }
    public Vector3 Position { get { return transform.position; } set { { transform.position = value; } } }
    public Vector3 CamSize { get { return camSize; } }

    // Start is called before the first frame update
    void Start()
    {
        camSize.y = Camera.main.orthographicSize;
        camSize.x = camSize.y * Camera.main.aspect;

        direction = Random.insideUnitCircle.normalized;

        spriteMin = new Vector2(0, 0);
        spriteMax = new Vector2(0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        velocity += acceleration * Time.deltaTime;

        transform.position += velocity * Time.deltaTime;

        if (velocity.sqrMagnitude > Mathf.Epsilon)
        {
            direction = velocity.normalized;
        }

        acceleration = Vector3.zero;

        transform.rotation = Quaternion.LookRotation(Vector3.back, direction);
    }

    public void ApplyForce(Vector3 force)
    {
        acceleration += force / mass;
    }
}
