using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{

    public FlockManager flockManager;
    public GameObject birdPrefab;

    // Start is called before the first frame update
    void Start()
    {
        flockManager = gameObject.AddComponent<FlockManager>();
        _init(1000, 0, 0, 0, 100f);

    }



    // Update is called once per frame
    void Update()
    {
        
    }

    private void _init(int flockSize, int x, int y, int z, float radius)
    {
        Bird[] flock = new Bird[flockSize];
        for(int i = 0; i < flockSize; i++)
        {
            flock[i] = Instantiate(birdPrefab).GetComponent<Bird>();
            flock[i].SetLocationRandom(x, y, z, radius);
            flock[i].SetSeperationDistance(1.5f);
        }

        flockManager.SetFlock(flock);

    }
}
