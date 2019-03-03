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
    Quaternion deltaRotation;
    public float deltaTilt = 25F;
    float leftTilt;
    float rightTilt;
    float upTilt;
    float downTilt;
    float maxTilt = 25;

    bool moveLeft = false;
    bool moveRight = false;
    bool moveUp = false;
    bool moveDown = false;
    bool jump = false;

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
        deltaRotation = Quaternion.Euler(rotationVector * angularVelocity * Time.deltaTime);

        /* No amount of angular velocity could generate enough force to keep the blade stable while spinning. 
         * So used this to keep the blade stable.
        */
        rb.freezeRotation = true;
        Physics.gravity = new Vector3(0, -normalGravity, 0);
        initRotation = transform.rotation;
        Debug.Log(rotationAxis);
    }


    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Equals("Pottery_Bowl_Mesh"))
        {
            Debug.Log("Jump woho");
            Physics.gravity = new Vector3(0, -normalGravity, 0);
            inMiddleOfJump = false;
            jump = false;
        }
    }

    private void Update()
    {
        if (Input.GetKey(ks.LEFT))
        {
            moveLeft = true;
        }
        else
        {
            moveLeft = false;
        }
        if (Input.GetKey(ks.RIGHT))
        {
            moveRight = true;
        }
        else
        {
            moveRight = false;
        }

        if (Input.GetKey(ks.UP))
        {
            moveUp = true;
        }
        else
        {
            moveUp = false;
        }

        if (Input.GetKey(ks.DOWN))
        {
            moveDown = true;
        }
        else
        {
            moveDown = false;
        }
        if (Input.GetKey(ks.JUMP))
        {
            jump = true;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // gives the spin
        //Quaternion quaternion = new Quaternion(rb.rotation.w,,  0, angularVelocity * Time.deltaTime);
        Quaternion quaternion = rb.rotation * deltaRotation;
        //Quaternion tempQuater = new Quaternion();

        // default force
        Vector3 forceToAdd = new Vector3(0, 0, 0);
        if (true)
        {
            if (moveLeft)
            {
                forceToAdd += Vector3.back * movementSpeed * Time.deltaTime;
                //tempQuater *= Quaternion.AngleAxis(deltaTilt, Vector3.left);
                //transform.rotation = Quaternion.Euler(10, 0, 0);
                            }
            if (moveRight)
            {
                //transform.rotation = Quaternion.Euler(p, 0, 0);
                forceToAdd += Vector3.forward * movementSpeed * Time.deltaTime;
            }
            if (moveUp)
            {
                forceToAdd += Vector3.left * movementSpeed * Time.deltaTime;
            }
            if (moveDown)
            {
                forceToAdd += Vector3.right * movementSpeed * Time.deltaTime;
            }
        }
        if (jump && !inMiddleOfJump)
        {
                Debug.Log("Pressed Jump");
                inMiddleOfJump = true;
                Physics.gravity = new Vector3(0, -jumpGravity, 0);
                rb.AddForce(new Vector3(0,500,0));
        }
        rb.AddForce(forceToAdd, forceMode);
        //transform.Rotate(10, 0, 0, Space.World);
        //transform.rotation = Quaternion.identity;
        //transform.eulerAngles = new Vector3(30,0,0);
        rb.MoveRotation(quaternion);
    }
}
