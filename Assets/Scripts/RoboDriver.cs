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

            Vector2 right_vector = this.transform.up;
            Vector3 left_vector = this.transform.up - this.transform.right;

            Debug.DrawRay(transform.position, right_vector * 50, Color.green);
            Debug.DrawRay(transform.position, left_vector, Color.green);

            RaycastHit2D right_raycast = Physics2D.Raycast(transform.position, right_vector); //, left_raycast;
            Debug.Log("Found an object in the forward side - distance: " + right_raycast.distance);
            
            //if (Physics.Raycast(transform.position, left_vector, out left_raycast))
            //{
                //Debug.Log("Found an object in the left side - distance: " + left_raycast.distance);
            //}

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
}