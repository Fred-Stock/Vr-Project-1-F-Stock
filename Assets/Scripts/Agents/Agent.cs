using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{

    [SerializeField] protected Vector3 acceleration;
    [SerializeField] protected Vector3 velocity;
    [SerializeField] protected float maxAcceleration;
    [SerializeField] protected Collider hitBox;

    [SerializeField] protected float wanderTimer;
    [SerializeField] protected float wanderInterval;
    [SerializeField] protected float wanderRadius;
    [SerializeField] protected float wanderDist;
    [SerializeField] protected Vector3 wanderVector;

    public Rigidbody agentRigidBody;

    public float gravity = 5.0f;

    [SerializeField] protected float maxSpeed;
    [SerializeField] protected float mass;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public Vector3 Seek(Vector3 target)
    {

        Vector3 desiredVel = target - transform.position;
        desiredVel = desiredVel.normalized;

        return desiredVel;
    }

    public Vector3 Flee(Vector3 target)
    {
        return -Seek(target);
    }

    public Vector3 Wander()
    {
        wanderTimer += Time.deltaTime;

        if (wanderTimer >= wanderInterval)
        {

            double curAngle = Mathf.Atan2(agentRigidBody.velocity.x - transform.position.x, agentRigidBody.velocity.z - transform.position.z);
            curAngle += Random.Range(60f * Mathf.Deg2Rad, 120 * Mathf.Deg2Rad);

            wanderVector = new Vector3(Mathf.Cos((float)curAngle), agentRigidBody.velocity.y + Random.Range(-1, 1), agentRigidBody.velocity.z + Mathf.Sin((float)curAngle)).normalized;

            wanderVector *= wanderRadius;

            wanderTimer -= wanderInterval;

        }

        return Seek(wanderVector + transform.position + transform.forward*wanderDist);
    }
}
