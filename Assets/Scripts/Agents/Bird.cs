using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : Agent
{
    public Rigidbody rigidBody;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
    }
}
