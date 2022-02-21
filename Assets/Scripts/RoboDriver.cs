using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoboDriver : MonoBehaviour
{
    //Components
    Rigidbody2D rb;

    //Movement variables
    public float thrust;
    public float friction;
    public float rotation_coefficient;
    Vector3 actual_direction = new Vector3(0, 0, 0); 

    // Start is called before the first frame update
    void Start()
    {
        //Components
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        #region Rotation (inputs)
        
            if (Input.GetKey(KeyCode.D))
            {
                transform.Rotate(-Vector3.forward * rotation_coefficient);
            }
            else if (Input.GetKey(KeyCode.A))
            {
                transform.Rotate(Vector3.forward * rotation_coefficient);
            }

        #endregion

        #region Raycasts


        #endregion

        #region Movement (inputs)

            //Acceleration
            if (Input.GetKey(KeyCode.W))
            {
                actual_direction = transform.up;
                rb.AddForce(actual_direction * thrust);
            }

            //Friction
            rb.velocity *= friction;

        #endregion
    }

    //change void to float
    float NewRoboRay(Vector2 robovector)
    {
        //Draw the vector of the ray
        Debug.DrawRay(transform.position, robovector, Color.green);

        //Create the ray array
        RaycastHit2D[] roboraycast = Physics2D.RaycastAll(transform.position, robovector); //, left_raycast;

        if (roboraycast.Length >= 2)
        {
            Debug.Log("Found an object in the forward side - distance: " + roboraycast[1].distance);

            return roboraycast.Length;
        }
        else
        {
            return 0;
        }
    }
}