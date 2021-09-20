using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallistaControlRight : MonoBehaviour
{
    public GameObject ballistaTop;
    public GameObject ballista;

    public float rotationSpeed = 10f;


    private XRIDefaultInputActions controls;

    private void Awake()
    {
        controls = new XRIDefaultInputActions();
    }

    private void OnEnable()
    {
        controls.XRIRightHand.Enable();
    }

    private void FixedUpdate()
    {

        ballistaTop.transform.Rotate(controls.XRIRightHand.RotateBallista.ReadValue<Vector2>().y * new Vector3(1, 0, 0) * rotationSpeed * Time.deltaTime);

        //if (controls.XRIRightHand.RotateBallista.ReadValue<Vector2>().y > 0)
        //{
        //    ballistaTop.transform.Rotate(new Vector3(1, 0, 0) * rotationSpeed * Time.deltaTime);
        //}
        //if (controls.XRIRightHand.RotateBallista.ReadValue<Vector2>().y < 0)
        //{
        //    ballistaTop.transform.Rotate(new Vector3(-1, 0, 0) * rotationSpeed * Time.deltaTime);
        //}

    }
}
