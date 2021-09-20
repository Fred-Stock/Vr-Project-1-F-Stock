using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.XR.Interaction.Toolkit;

public class Trigger : MonoBehaviour
{

    public Material otherColor;

    public void OnEnable()
    {
        XRGrabInteractable triggerInteractable = GetComponent<XRGrabInteractable>();
        triggerInteractable.activated.AddListener(Shoot);
    }

    private void Shoot(ActivateEventArgs arg0)
    {
        gameObject.GetComponent<MeshRenderer>().material = otherColor;
    }
}
