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

    public int movementSpeed = 30;
    public long angularVelocity = int.MaxValue;
    public axis rotationAxis;
    public Controls.KeysetName controls;
    private Keyset ks;
    private Vector3 rotationVector;
    public int mass;
    private ForceMode forceMode = ForceMode.VelocityChange;
    private bool inMiddleOfJump;
    public int jumpSpeed = 500;
    public float jumpGravity = 35.0f;

    public float normalGravity = 9.8f;
    public bool inMiddleOfAttack;
    private bool rotating = false;
    Quaternion initRotation;
    Quaternion deltaRotation;

    private float maxTilt = 15f;
    private bool holdingDown = false;

    private DashAbility dashAbility;
    private Tilt tilt;

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
        //deltaRotation = Quaternion.Euler(rotationVector * angularVelocity * Time.deltaTime);

        /* No amount of angular velocity could generate enough force to keep the blade stable while spinning. 
         * So used this to keep the blade stable.
        */
        rb.freezeRotation = true;
        Physics.gravity = Vector3.down * jumpGravity;
        initRotation = transform.rotation;
        dashAbility = new DashAbility();
        dashAbility.dashState = DashState.Ready;
        tilt = new Tilt(initRotation.eulerAngles.x, maxTilt);
    }


    public void OnCollisionEnter(Collision collision)
    {
        //Jump is allowed only when a beyblade touches the battle arena
        if (collision.gameObject.name.Equals(Constants.BATTLE_ARENA))
        {
            Physics.gravity = Vector3.down * normalGravity;
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

        //Rotating the blade every frame to keep it spinning.
        Quaternion bladeSpin = Quaternion.Euler(Vector3.forward * angularVelocity * Time.deltaTime);

        //dashAbility.checkDash(rb, ks);

        // Default force
        Vector3 forceToAdd = Vector3.zero;
        if (true)
        {
            if (moveLeft)
            {
                forceToAdd += Vector3.back * movementSpeed * Time.deltaTime;         
                Quaternion newAngle = tilt.getTilt(rb);
                //Debug.Log(newAngle.eulerAngles.x + "," + newAngle.eulerAngles.y + "," + newAngle.eulerAngles.z);
                rb.MoveRotation(newAngle);
            }
            if (moveRight)
            {              
                forceToAdd += Vector3.forward * movementSpeed * Time.deltaTime;
                Quaternion newAngle = tilt.getTilt(rb);
                //Debug.Log(newAngle.eulerAngles.x + "," + newAngle.eulerAngles.y + "," + newAngle.eulerAngles.z);
                rb.MoveRotation(newAngle);
            }
            if (moveUp)
            {
                forceToAdd += Vector3.left * movementSpeed * Time.deltaTime;
                Quaternion newAngle = tilt.getTilt(rb);
                //Debug.Log(newAngle.eulerAngles.x + "," + newAngle.eulerAngles.y + "," + newAngle.eulerAngles.z);
                rb.MoveRotation(newAngle);
            }
            if (moveDown)
            {
                forceToAdd += Vector3.right * movementSpeed * Time.deltaTime;
                Quaternion newAngle = tilt.getTilt(rb);
                //Debug.Log(newAngle.eulerAngles.x + "," + newAngle.eulerAngles.y + "," + newAngle.eulerAngles.z);
                rb.MoveRotation(newAngle);
            }
        }

        if (jump && !inMiddleOfJump)
        {
            inMiddleOfJump = true;
            Physics.gravity = Vector3.down * jumpGravity;
            rb.AddForce(Vector3.up * jumpSpeed);        
        }

        if (Input.anyKey)
        {
            holdingDown = true;
        }

        if (!Input.anyKey && holdingDown)
        {
            holdingDown = false;
            Quaternion newAngle = Quaternion.Euler(Vector3.right * initRotation.eulerAngles.x);
            rb.MoveRotation(newAngle);
        }


        rb.AddForce(forceToAdd, forceMode);
        rb.MoveRotation(rb.rotation * bladeSpin);
    }
}
