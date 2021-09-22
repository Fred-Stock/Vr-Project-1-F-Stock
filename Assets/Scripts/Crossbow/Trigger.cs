using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Trigger : MonoBehaviour
{

    public GameObject curBolt;
    public GameObject crossbowString;
    private XRGrabInteractable triggerInteractable;

    public void OnEnable()
    {
        triggerInteractable = GetComponent<XRGrabInteractable>();
        triggerInteractable.activated.AddListener(OnShoot);
    }

    public void OnDisable()
    {
        triggerInteractable.activated.RemoveListener(OnShoot);
    }

    /// <summary>
    /// Method which is called when the player presses the trigger while holding crossbow
    /// Launches the loaded bolt and makes the crossbow string visible again
    /// </summary>
    /// <param name="arg0"></param>
    private void OnShoot(ActivateEventArgs arg0)
    {
        if (!crossbowString.GetComponent<CrossbowString>().loaded) { return; } //makes sure crossbow is actually loaded before trying to fire

        // launches bolt and assigns parameters needed for flight 
        curBolt.GetComponent<Rigidbody>().AddForce(curBolt.GetComponent<Bolt>().shootForce * curBolt.transform.forward, ForceMode.Force);
        curBolt.GetComponent<Rigidbody>().useGravity = true;
        curBolt.GetComponent<Bolt>().inAir = true;
        curBolt.transform.SetParent(curBolt.transform);

        // Makes the crossbow string visible and grabable again
        crossbowString.GetComponent<MeshRenderer>().enabled = true;
        crossbowString.GetComponent<CapsuleCollider>().enabled = true;
        crossbowString.GetComponent<CrossbowString>().loaded = false;

    }
}
