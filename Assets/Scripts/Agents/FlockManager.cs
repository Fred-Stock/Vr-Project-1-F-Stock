using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
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

    }

    public void HandleBirdDeath(Bird hitBird)
    {
        //happens when bird gets hit with projectile
        //1. disable the game object
        //2. Swap the index of the gameobject with the last active bird in the flock
        //3. decrement flockSize by one
    }
}
