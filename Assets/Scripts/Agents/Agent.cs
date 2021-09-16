using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{

    [SerializeField] protected Vector3 acceleration;
    [SerializeField] protected Vector3 velocity;
    [SerializeField] protected float maxAcceleration;
    protected Vector3 heading;
    [SerializeField] protected Collider hitBox;

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

    //public void ApplyForce(Vector3 force)
    //{
    //    acceleration.x += force.x / mass;
    //    acceleration.y += force.y / mass;
    //    acceleration.z += force.z / mass;

    //}

    //public void Move()
    //{
    //    velocity += acceleration;

    //    acceleration = Vector3.zero;
    //    Vector3.ClampMagnitude(velocity, maxSpeed);
    //    gameObject.transform.position += velocity * Time.deltaTime;
    //    heading = transform.forward;

    //}

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

}
