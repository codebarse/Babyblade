using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2 : MonoBehaviour
{
    public Rigidbody rb;
    public int velocity = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb.AddForce(0, 0, velocity, ForceMode.VelocityChange);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            rb.AddForce(0, 0, -velocity, ForceMode.VelocityChange);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            rb.AddForce(velocity, 0, 0, ForceMode.VelocityChange);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            rb.AddForce(-velocity, 0, 0, ForceMode.VelocityChange);
        }

    }
}
