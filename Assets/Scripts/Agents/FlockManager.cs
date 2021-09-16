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
    public GameObject[] centerPath;
    private int pathIndex;
    public GameObject currentNode;
    private float sqrMoveNodeRadius;
    private float pathWeight;

    // Start is called before the first frame update
    void Start()
    {
        flockSize = 9;
        fleeWeight = 6f;
        centerWeight = 2f;
        flockCenter = FindFlockCenter();
        pathIndex = 0;
        sqrMoveNodeRadius = 1;

        currentNode = centerPath[0];
        followPath = true;
        pathWeight = 4f;
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

    public void SeekCenter()
    {
        for(int i = 0; i < flockSize; i++)
        {
            flock[i].rigidBody.AddForce(flock[i].Seek(flockCenter) * centerWeight, ForceMode.Acceleration);
        }
    }

    public void KeepDistance()
    {
        for(int i = 0; i < flockSize; i++)
        {
            for(int j = 0; j < flockSize; j++)
            {
                if(i != j)
                {
                    if((flock[i].transform.position - flock[j].transform.position).sqrMagnitude < 2.5*2.5)
                    {
                        flock[i].rigidBody.AddForce(flock[i].Flee(flock[j].transform.position) * fleeWeight, ForceMode.Acceleration);
                    }
                }
            }
        }
    }

    public void MoveAlongPath()
    {
        if (followPath)
        {
            if((currentNode.transform.position - flockCenter).sqrMagnitude <= sqrMoveNodeRadius)
            {
                if(pathIndex >= centerPath.Length-1) pathIndex = 0; 
                else pathIndex++;

                currentNode = centerPath[pathIndex];
            }
            for (int i = 0; i < flockSize; i++)
            {
                flock[i].rigidBody.AddForce(flock[i].Seek(currentNode.transform.position)*pathWeight, ForceMode.Acceleration);
            }
        }

    }

    public void HandleBirdDeath(Bird hitBird)
    {
        //happens when bird gets hit with projectile
        //1. disable the game object
        //2. Swap the index of the gameobject with the last active bird in the flock
        //3. decrement flockSize by one
    }
}
