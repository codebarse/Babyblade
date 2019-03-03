﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public enum axis
    {
        x,y,z
    }

    public Rigidbody rb;

    public int movementSpeed = 1000;
    public long angularVelocity = int.MaxValue;
    public axis rotationAxis;
    public Controls.KeysetName controls;
    private Keyset ks;
    private Vector3 rotationVector;
    public int mass;
    private ForceMode forceMode = ForceMode.Acceleration;

    // Start is called before the first frame update
    void Start()
    {
        ks = Controls.GetKeyset(controls);
        if (rotationAxis.Equals(axis.x)) rotationVector = Vector3.right;
        else if (rotationAxis.Equals(axis.y)) rotationVector = Vector3.up;
        else if (rotationAxis.Equals(axis.z)) rotationVector = Vector3.forward;
        rb.mass = mass;
        Physics.gravity = new Vector3(1,-9.8F, 1);
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        Quaternion deltaRotation = Quaternion.Euler(rotationVector * angularVelocity * Time.deltaTime);
        rb.MoveRotation(rb.rotation * deltaRotation);

        /* No amount of angular velocity could generate enough force to keep the blade stable while spinning. 
         * So used this to keep the blade stable.
        */      
        rb.freezeRotation = true;

        if (Input.GetKey(ks.LEFT))
        {
            rb.AddForce(Vector3.back * movementSpeed * Time.deltaTime, forceMode);

        }
        if (Input.GetKey(ks.RIGHT))
        {
            rb.AddForce(Vector3.forward * movementSpeed * Time.deltaTime, forceMode);
        }
        if (Input.GetKey(ks.UP))
        {
            rb.AddForce(Vector3.left * movementSpeed * Time.deltaTime, forceMode);

        }
        if (Input.GetKey(ks.DOWN))
        {
            rb.AddForce(Vector3.right * movementSpeed * Time.deltaTime, forceMode);
        }

    }
}
