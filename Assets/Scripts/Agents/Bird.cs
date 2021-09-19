using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : Agent
{
    public SphereCollider seperatingSphere;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        agentRigidBody = GetComponent<Rigidbody>();
        seperatingSphere = GetComponent<SphereCollider>();
    }

    /// <summary>
    /// sets location of the object in a random radius around a point (x,y,z)
    /// </summary>
    public void SetLocationRandom(int x, int y, int z, float radius)
    {
        Vector3 newLoc = new Vector3(x + Random.Range(-radius, radius), y + Random.Range(-radius, radius), z + Random.Range(-radius, radius));
        transform.position = newLoc;
    }

    public void SetSeperationDistance(float radius)
    {
        seperatingSphere.radius = radius;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.GetComponent<Bird>() != null)
        {
            agentRigidBody.AddForce(Flee(collision.collider.transform.position)*6f, ForceMode.Acceleration);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
