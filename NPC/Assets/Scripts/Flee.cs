using UnityEngine;

public class Flee : Agent
{
    [SerializeField] GameObject target;

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        if (CircleCollision(physicsObject, target.GetComponent<PhysicsObject>()))
        {
            Vector3 randomPosition = new Vector3(
                Random.Range(-physicsObject.CamSize.x, physicsObject.CamSize.x),
                Random.Range(-physicsObject.CamSize.y, physicsObject.CamSize.y),
                0f);
            physicsObject.Position = randomPosition;
        }
    }

    protected override void CalcSteeringForces()
    {
        Flee(target);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;

        Gizmos.DrawLine(transform.position, physicsObject.Velocity + transform.position);
    }

    bool CircleCollision(PhysicsObject spriteA, PhysicsObject spriteB)
    {
        Vector2 aCenter = new(spriteA.transform.position.x, spriteA.transform.position.y);
        Vector2 bCenter = new(spriteB.transform.position.x, spriteB.transform.position.y);

        if (Mathf.Pow(bCenter.x - aCenter.x, 2) + Mathf.Pow(bCenter.y - aCenter.y, 2) <
            Mathf.Pow(spriteA.Radius + spriteB.Radius, 2))
        {
            return true;
        }
        return false;
    }
}