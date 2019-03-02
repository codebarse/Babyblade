using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 : MonoBehaviour
{
    public Rigidbody rb;
    public int velocity = 50;
    private string LEFT = "a";
    private string RIGHT = "d";
    private string UP = "w";
    private string DOWN = "s";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(LEFT))
        {
            rb.AddForce(0, 0, velocity);
        }
        if (Input.GetKey(RIGHT))
        {
            rb.AddForce(0, 0, -velocity);
        }
        if (Input.GetKey(UP))
        {
            rb.AddForce(velocity, 0, 0);
        }
        if (Input.GetKey(DOWN))
        {
            rb.AddForce(-velocity, 0, 0);
        }

    }
}
