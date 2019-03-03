using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boxText : MonoBehaviour
{
    public Rigidbody rb;
    public int velocity = 20;
    Vector3 m_EulerAngleVelocity;

    public float moveSpeed = 10f;
    public float turnSpeed = 50f;
    // Start is called before the first frame update
    void Start()
    {
        m_EulerAngleVelocity = new Vector3(0, 30, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Quaternion deltaRotation = Quaternion.Euler(m_EulerAngleVelocity * Time.deltaTime);
            rb.MoveRotation(rb.rotation * deltaRotation);
            //rb.AddForce(0, 0, velocity);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            //Quaternion deltaRotation = Quaternion.Euler(m_EulerAngleVelocity * Time.deltaTime);
            //rb.MoveRotation(rb.rotation * deltaRotation);
            rb.AddForce(0, 0, -velocity);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            Quaternion deltaRotation = Quaternion.Euler(m_EulerAngleVelocity * Time.deltaTime);
            rb.MoveRotation(rb.rotation * deltaRotation);
            rb.AddForce(velocity, 0, 0);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            //Quaternion deltaRotation = Quaternion.Euler(m_EulerAngleVelocity * Time.deltaTime);
            //rb.MoveRotation(rb.rotation * deltaRotation);
            rb.AddForce(-velocity, 0, 0);
        }

        //if (Input.GetKey(KeyCode.UpArrow))
        //{
        //    transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        //}

        //if (Input.GetKey(KeyCode.DownArrow))
        //    transform.Translate(-Vector3.forward * moveSpeed * Time.deltaTime);

        //if (Input.GetKey(KeyCode.LeftArrow))
        //    transform.Rotate(Vector3.up, -turnSpeed * Time.deltaTime);

        //if (Input.GetKey(KeyCode.RightArrow))
            //transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime);

    }
}
