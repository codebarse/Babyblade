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
    public bool inMiddleOfJump;
    public int jumpSpeed = 2;
    public float jumpGravity = 35.0f;
    public float normalGravity = 9.8f;
    public bool inMiddleOfAttack;
    private bool rotating = false;
    Quaternion initRotation;

    // Start is called before the first frame update
    void Start()
    {
        inMiddleOfAttack = false;
        inMiddleOfJump = false;
        rb = GetComponent<Rigidbody>();
        ks = Controls.GetKeyset(controls);
        if (rotationAxis.Equals(axis.x)) rotationVector = Vector3.right;
        else if (rotationAxis.Equals(axis.y)) rotationVector = Vector3.up;
        else if (rotationAxis.Equals(axis.z)) rotationVector = Vector3.forward;
        rb.mass = mass;
        rb.freezeRotation = true;
        Physics.gravity = new Vector3(0, -normalGravity, 0);
        initRotation = transform.rotation;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (rb.position.y < 1.5 && rb.position.y > 1.1)
        {
            Physics.gravity = new Vector3(0, -normalGravity, 0);
            inMiddleOfJump = false;
        }

        Quaternion deltaRotation = Quaternion.Euler(rotationVector * angularVelocity * Time.deltaTime);
        rb.MoveRotation(rb.rotation * deltaRotation);

        /* No amount of angular velocity could generate enough force to keep the blade stable while spinning. 
         * So used this to keep the blade stable.
        */

        //if (Input.GetKey(ks.ATTACK))
        //{
        //    if (Input.GetKey(ks.LEFT))
        //    {
        //        if (!inMiddleOfAttack)
        //        {
        //            inMiddleOfAttack = true;
        //            rb.MoveRotation(transform.rotation * Quaternion.AngleAxis(20, Vector3.left));
        //        }
        //        rb.AddForce(Vector3.back * 3 * movementSpeed * Time.deltaTime, ForceMode.Acceleration);
        //    }
        //    else if (Input.GetKey(ks.RIGHT))
        //    {
        //        if (!inMiddleOfAttack)
        //        {
        //            inMiddleOfAttack = true;
        //            rb.MoveRotation(transform.rotation * Quaternion.AngleAxis(20, Vector3.right));
        //        }
        //        rb.AddForce(Vector3.forward * 3 * movementSpeed * Time.deltaTime, ForceMode.Acceleration);
        //    }
        //    else if (Input.GetKey(ks.UP))
        //    {
        //        if (!inMiddleOfAttack)
        //        {
        //            inMiddleOfAttack = true;
        //            rb.MoveRotation(transform.rotation * Quaternion.AngleAxis(20, Vector3.up));
        //        }
        //        rb.AddForce(Vector3.left * 3 * movementSpeed * Time.deltaTime, ForceMode.Acceleration);
        //    }
        //    else if (Input.GetKey(ks.DOWN))
        //    {
        //        if (!inMiddleOfAttack)
        //        {
        //            inMiddleOfAttack = true;
        //            rb.MoveRotation(transform.rotation * Quaternion.AngleAxis(20, Vector3.down));
        //        }
        //        rb.AddForce(Vector3.right * 3 * movementSpeed * Time.deltaTime, ForceMode.Acceleration);
        //    }

        //}
        if (Input.GetKey(ks.LEFT))
        {
            inMiddleOfAttack = false;
            //transform.rotation = initRotation;
            rb.AddForce(Vector3.back * movementSpeed * Time.deltaTime, ForceMode.Acceleration);
        }
        else if (Input.GetKey(ks.RIGHT))
        {
            inMiddleOfAttack = false;
            //transform.rotation = initRotation;
            rb.AddForce(Vector3.forward * movementSpeed * Time.deltaTime, ForceMode.Acceleration);

        }
        else if (Input.GetKey(ks.UP))
        {
            inMiddleOfAttack = false;
            //transform.rotation = initRotation;
            rb.AddForce(Vector3.left * movementSpeed * Time.deltaTime, ForceMode.Acceleration);

        }
        if (Input.GetKey(ks.DOWN))
        {
            inMiddleOfAttack = false;
            //transform.rotation = initRotation;
            rb.AddForce(Vector3.right * movementSpeed * Time.deltaTime, ForceMode.Acceleration);
        }
        else if (Input.GetKey(ks.JUMP))
        {
            if(!inMiddleOfJump)
            {
                inMiddleOfJump = true;
                Physics.gravity = new Vector3(0, -jumpGravity, 0);
                rb.AddForce(Vector3.up * jumpSpeed * movementSpeed * Time.deltaTime, ForceMode.Acceleration);
            }
        }

    }
}
