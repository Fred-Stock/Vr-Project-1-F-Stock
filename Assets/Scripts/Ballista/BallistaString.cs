using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


// Might be better to do all rotation of the ballista with the sticks

public class BallistaString : MonoBehaviour
{
    public GameObject boltPrefab;

    private Vector3 initialGrabPos;
    private Vector3 currentGrabVec;
    private XRBaseInteractor currentHand;
    private float ballistaRotationSpeed;

    public GameObject ballistaBase;
    private GameObject curBolt;
    [SerializeField] private float stringForce;
    [SerializeField] private float maxDrawDist;
    private float sqrMaxDrawDist;
    private bool holdingString = false;

    public void OnEnable()
    {
        gameObject.GetComponent<XRSimpleInteractable>().selectEntered.AddListener(OnPlayerGrabEvent);
        gameObject.GetComponent<XRSimpleInteractable>().selectExited.AddListener(OnPlayerLetGoEvent);
        sqrMaxDrawDist = maxDrawDist * maxDrawDist;
        ballistaRotationSpeed = 45f;
    }

    public void OnDisable()
    {
        gameObject.GetComponent<XRSimpleInteractable>().selectEntered.RemoveListener(OnPlayerGrabEvent);
        gameObject.GetComponent<XRSimpleInteractable>().selectExited.RemoveListener(OnPlayerLetGoEvent);
    }

    public void FixedUpdate()
    {
        if (holdingString)
        {
            currentGrabVec = (initialGrabPos - currentHand.transform.position);


            ballistaBase.transform.localEulerAngles += currentGrabVec.y * transform.up * Time.deltaTime * ballistaRotationSpeed;
            
            currentGrabVec = new Vector3(currentGrabVec.x, 0, currentGrabVec.z); //remove the y component of the controller movement as that was used for rotation of ballista
      
            currentGrabVec = Vector3.ClampMagnitude(currentGrabVec, maxDrawDist);
            curBolt.transform.position = transform.position + (.5f - (currentGrabVec.sqrMagnitude / sqrMaxDrawDist)) * transform.forward;
            curBolt.transform.rotation = transform.rotation;
        }
    }

    private void OnPlayerGrabEvent(SelectEnterEventArgs arg0)
    {
        OnPlayerGrab();
        currentHand = arg0.interactor;
        
        initialGrabPos = arg0.interactor.transform.position;
    }

    private void OnPlayerGrab()
    {
        GetComponent<MeshRenderer>().enabled = false;
        //GetComponent<CapsuleCollider>().enabled = false;
        curBolt = Instantiate(boltPrefab, transform.position + .5f*transform.forward, transform.rotation);
        curBolt.GetComponent<Rigidbody>().useGravity = false;
        holdingString = true;
    }

    private void OnPlayerLetGoEvent(SelectExitEventArgs arg0)
    {
        OnPlayerLetGo();
        curBolt.GetComponent<Rigidbody>().AddForce(Shoot(arg0.interactor.transform.position), ForceMode.Force);
    }

    private void OnPlayerLetGo()
    {
        GetComponent<MeshRenderer>().enabled = true;
        //GetComponent<CapsuleCollider>().enabled = true;
        curBolt.GetComponent<Rigidbody>().useGravity = true;
        holdingString = false;
    }

    private Vector3 Shoot(Vector3 interactorCurPos)
    {

        Vector3 shootForce = transform.forward * (currentGrabVec.sqrMagnitude/sqrMaxDrawDist) * stringForce; 
   
        return shootForce;
    }
}
