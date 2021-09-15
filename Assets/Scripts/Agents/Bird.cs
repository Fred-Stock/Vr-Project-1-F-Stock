using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : Agent
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody>().AddForce(Flee(new Vector3(5, 5, 5)));
    }
}
