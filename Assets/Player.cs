using System.Collections;
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
    public bool inMiddleOfJump;
    public float jumpSpeed = 1/100;
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

        /* No amount of angular velocity could generate enough force to keep the blade stable while spinning. 
         * So used this to keep the blade stable.
        */
        rb.freezeRotation = true;
        Physics.gravity = new Vector3(0, -normalGravity, 0);
        initRotation = transform.rotation;
    }


    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Equals("Pottery_Bowl_Mesh"))
        {
            Debug.Log("Jump woho");
            Physics.gravity = new Vector3(0, -normalGravity, 0);
            inMiddleOfJump = false;
        }
    }

    void FixedUpdate()
    {
        Quaternion deltaRotation = Quaternion.Euler(rotationVector * angularVelocity * Time.deltaTime);
        rb.MoveRotation(rb.rotation * deltaRotation);

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
            rb.AddForce(Vector3.back * movementSpeed * Time.deltaTime, forceMode);
            //inMiddleOfAttack = false;
            //transform.rotation = initRotation;
        }
        if (Input.GetKey(ks.RIGHT))
        {
            rb.AddForce(Vector3.forward * movementSpeed * Time.deltaTime, forceMode);
            //inMiddleOfAttack = false;
            //transform.rotation = initRotation;
        }
        if (Input.GetKey(ks.UP))
        {
            rb.AddForce(Vector3.left * movementSpeed * Time.deltaTime, forceMode);
            //inMiddleOfAttack = false;
            //transform.rotation = initRotation;
        }
        if (Input.GetKey(ks.DOWN))
        {
            rb.AddForce(Vector3.right * movementSpeed * Time.deltaTime, forceMode);
            //inMiddleOfAttack = false;
            //transform.rotation = initRotation;
        }
        if (Input.GetKey(ks.JUMP))
        {
            if(!inMiddleOfJump)
            {
                Debug.Log("Pressed Jump");
                inMiddleOfJump = true;
                Physics.gravity = new Vector3(0, -jumpGravity, 0);
                rb.AddForce(new Vector3(0,500,0));
            }
        }
    }
}
