using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoboDriver : MonoBehaviour
{
    //Components
    Rigidbody rb;

    //Movement variables
    public float thrust = 500f;
    public float friction = 0.993f;
    Vector3 actual_direction = new Vector3(0, 0, 0);

    // Start is called before the first frame update
    void Start()
    {
        //Components
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        #region Rotation (inputs)
        
            if (Input.GetKey(KeyCode.D))
            {
                transform.Rotate(-Vector3.forward, Space.World);
            }
            else if (Input.GetKey(KeyCode.A))
            {
                transform.Rotate(Vector3.forward, Space.World);
            }

        #endregion

        #region Raycasts

            Vector3 right_vector = this.transform.up + this.transform.right;
            Vector3 left_vector = this.transform.up - this.transform.right;

            Debug.DrawRay(transform.position, right_vector, Color.green);
            Debug.DrawRay(transform.position, left_vector, Color.green);

            RaycastHit right_raycast, left_raycast;

            if (Physics.Raycast(transform.position, right_vector, out right_raycast))
            {
                Debug.Log("Found an object in the right side - distance: " + right_raycast.distance);
            }
            if (Physics.Raycast(transform.position, left_vector, out left_raycast))
            {
                Debug.Log("Found an object in the left side - distance: " + left_raycast.distance);
            }

        #endregion

        #region Movement (inputs)

            //Acceleration
            if (Input.GetKey(KeyCode.W))
            {
                actual_direction = this.transform.up;
                rb.AddForce(actual_direction * thrust);
            }

            //Friction
            rb.velocity *= friction;

            Debug.Log(actual_direction);

        #endregion
    }
}