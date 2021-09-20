using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BallistaControlLeft : MonoBehaviour
{

    public GameObject ballista;

    public float rotationSpeed = 10f;

    private XRIDefaultInputActions controls;

    private void Awake()
    {
        controls = new XRIDefaultInputActions();
    }

    private void OnEnable()
    {
        controls.XRILeftHand.Enable();
    }

    private void FixedUpdate()
    {
        if(controls.XRILeftHand.RotateBallista.ReadValue<Vector2>().x > 0)
        {
            ballista.transform.Rotate(rotationSpeed * ballista.transform.up * Time.deltaTime);
        }
        if(controls.XRILeftHand.RotateBallista.ReadValue<Vector2>().x < 0)
        {
            ballista.transform.Rotate(-rotationSpeed * ballista.transform.up * Time.deltaTime);
        }

    }


}
