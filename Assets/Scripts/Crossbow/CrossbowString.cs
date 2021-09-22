using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


public class CrossbowString : MonoBehaviour
{
    public GameObject boltPrefab;

    public GameObject crossbowBase;
    public GameObject crossbowTrigger;
    private GameObject curBolt;

    [SerializeField] private float stringForce;
    public bool loaded = false;

    public void OnEnable()
    {
        gameObject.GetComponent<XRSimpleInteractable>().selectEntered.AddListener(OnPlayerGrabEvent);
        gameObject.GetComponent<XRSimpleInteractable>().selectExited.AddListener(OnPlayerLetGoEvent);
    }

    public void OnDisable()
    {
        gameObject.GetComponent<XRSimpleInteractable>().selectEntered.RemoveListener(OnPlayerGrabEvent);
        gameObject.GetComponent<XRSimpleInteractable>().selectExited.RemoveListener(OnPlayerLetGoEvent);
    }

    /// <summary>
    /// Loads crossbow if the player grabs the string
    /// </summary>
    /// <param name="arg0"></param>
    private void OnPlayerGrabEvent(SelectEnterEventArgs arg0)
    {
        //Makes sure that it is not possible to load multiple bolts 
        if (!loaded)
        {
            OnPlayerGrab();
        }
    }

    /// <summary>
    /// Creates a bolt that is attached to the base of the crossbow
    /// When the trigger is pressed the bolt gets lauched has gravity turned back on
    /// </summary>
    private void OnPlayerGrab()
    {
        //Turns the crossbow string invisible
        GetComponent<MeshRenderer>().enabled = false;

        //Creates a bolt and initializes it so it sticks to the base of the crossbow
        curBolt = Instantiate(boltPrefab, transform.position + -.25f * transform.forward, transform.rotation);
        crossbowTrigger.GetComponent<Trigger>().curBolt = curBolt;
        curBolt.GetComponent<Rigidbody>().useGravity = false;
        curBolt.transform.SetParent(crossbowBase.transform);

        loaded = true;
    }

    //Assigns the magnititude of the force that will launch the bolt
    private void OnPlayerLetGoEvent(SelectExitEventArgs arg0)
    {
        curBolt.GetComponent<Bolt>().shootForce = stringForce;
    }



}
