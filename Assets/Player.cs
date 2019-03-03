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
    public int jumpSpeed = 500;
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
        Physics.gravity = Vector3.down * jumpGravity;
        initRotation = transform.rotation;
    }


    public void OnCollisionEnter(Collision collision)
    {
        //Jump is allowed only when a beyblade touches the battle arena
        if (collision.gameObject.name.Equals(Constants.BATTLE_ARENA))
        {
            Physics.gravity = Vector3.down * normalGravity;
            inMiddleOfJump = false;
        }
    }

    void FixedUpdate()
    {
        Quaternion deltaRotation = Quaternion.Euler(rotationVector * angularVelocity * Time.deltaTime);
        rb.MoveRotation(rb.rotation * deltaRotation);

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
        if (Input.GetKey(ks.JUMP))
        {
            if(!inMiddleOfJump)
            {
                inMiddleOfJump = true;
                Physics.gravity = Vector3.down * jumpGravity;
                rb.AddForce(Vector3.up * jumpSpeed);
            }
        }
    }
}
