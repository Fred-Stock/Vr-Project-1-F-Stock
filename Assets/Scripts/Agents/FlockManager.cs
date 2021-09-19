using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// For multiple flocks the flock memeber storage will need to be different
/// Want easy removal and addition of singular elements 
/// Maybe every flock has an array of the size equal to the total birds?
///     - Little memory inefficient
/// Linked list doesnt handle removal well enough
///     - Inserting is easy though
///     - Could just have a var track which flock the bird is a part of 
/// </summary>
public class FlockManager : MonoBehaviour
{

    [SerializeField] private Bird[] flock;
    private int flockSize;
    private Vector3 flockCenter;
    private float fleeWeight;
    private float centerWeight;

    private bool followPath; //might be changed to enum
    public GameObject[] flockPath;
    private int pathIndex;
    public GameObject currentNode;
    private float sqrMoveNodeRadius;
    private float pathWeight;
    private float wanderWeight;
    private float sqrSeperationRadius;

    void OnEnable()
    {
        fleeWeight = 6f;
        pathWeight = 20f;
        centerWeight = 2f;
        wanderWeight = 5f;

        flockCenter = FindFlockCenter();

        pathIndex = 0;
        sqrMoveNodeRadius = 4000;

        sqrSeperationRadius = 1f * 1f; // wrote as a multiplication as it is easier to interpret the seperation distance

        //TEMP SOLUTION
        flockPath = new GameObject[4];
        flockPath[0] = GameObject.Find("Node0");
        flockPath[1] = GameObject.Find("Node1");
        flockPath[2] = GameObject.Find("Node2");
        flockPath[3] = GameObject.Find("Node3");

        currentNode = flockPath[0];
        followPath = true;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        flockCenter = FindFlockCenter();
        SeekCenter(); //this may be moved in future
        KeepDistance(); //this may be moved in future
        MoveAlongPath();
    }

    public Vector3 FindFlockCenter()
    {
        Vector3 positionSum = Vector3.zero;
        for(int i = 0; i < flockSize; i++)
        {
            positionSum += flock[i].transform.position;
        }

        return positionSum / flockSize;
    }

    /// <summary>
    /// Moves all birds in the flock towards the center of the flock
    /// </summary>
    public void SeekCenter()
    {
        for(int i = 0; i < flockSize; i++)
        {
            flock[i].agentRigidBody.AddForce(flock[i].Seek(flockCenter) * centerWeight, ForceMode.Acceleration);
        }
    }

    /// <summary>
    /// Ensures there is a force keeping the birds apart from each other in the flock so they do not bundle together
    /// </summary>
    public void KeepDistance()
    {

    }

    public void MoveAlongPath()
    {
        if (followPath)
        {
            if((currentNode.transform.position - flockCenter).sqrMagnitude <= sqrMoveNodeRadius)
            {
                if(pathIndex >= flockPath.Length-1) pathIndex = 0; 
                else pathIndex++;

                currentNode = flockPath[pathIndex];
            }
            for (int i = 0; i < flockSize; i++)
            {
                flock[i].agentRigidBody.AddForce(flock[i].Seek(currentNode.transform.position)*pathWeight, ForceMode.Acceleration);
                flock[i].agentRigidBody.AddForce(flock[i].Wander() * wanderWeight, ForceMode.Acceleration);
            }
        }

    }

    public void SetFlock(Bird[] flock)
    {
        this.flock = flock;
        flockSize = flock.Length;
    }

    public void HandleBirdDeath(Bird hitBird)
    {
        //happens when bird gets hit with projectile
        //1. disable the game object
        //2. Swap the index of the gameobject with the last active bird in the flock
        //3. decrement flockSize by one
    }


}
