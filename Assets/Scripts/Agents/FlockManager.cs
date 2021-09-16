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

    private Bird[] flock;
    private int flockSize;
    private Vector3 flockCenter;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        flockCenter = FindFlockCenter();
        SeekCenter();
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
            flock[i].Seek(flockCenter);
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
