using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody rb;

    public int movementSpeed = 1000;
    public int angularVelocity = 1000;
    public Controls.KeysetName controls;
    private Keyset ks;

    // Start is called before the first frame update
    void Start()
    {
        ks = Controls.GetKeyset(controls);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Quaternion deltaRotation = Quaternion.Euler(Vector3.up * angularVelocity * Time.deltaTime);
        rb.MoveRotation(rb.rotation * deltaRotation);

        if (Input.GetKey(ks.LEFT))
        {
            rb.AddForce(Vector3.back * movementSpeed * Time.deltaTime);

        }
        if (Input.GetKey(ks.RIGHT))
        {
            rb.AddForce(Vector3.forward * movementSpeed * Time.deltaTime);

        }
        if (Input.GetKey(ks.UP))
        {
            rb.AddForce(Vector3.left * movementSpeed * Time.deltaTime);

        }
        if (Input.GetKey(ks.DOWN))
        {
            rb.AddForce(Vector3.right * movementSpeed * Time.deltaTime);
        }

    }
}
