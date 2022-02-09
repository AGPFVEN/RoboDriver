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
    Texture2D original_map; //
    Texture2D modified_map; //
    GameObject modified_driving_lane;
    Sprite modified_map_sprite;


    // Start is called before the first frame update
    void Start()
    {
        //Components
        rb = gameObject.GetComponent<Rigidbody>();

        //Get the Sprite of the driving line (from the gameobject, which is a child object)
        original_map = GameObject.Find("Image Manager").transform.GetChild(0).GetComponent<SpriteRenderer>().sprite.texture;
        float heigth_of_images = original_map.height;
        float width_of_images = original_map.width;
        
        //Get the modified driving lane (child object)
        modified_driving_lane = GameObject.Find("Image Manager").transform.GetChild(1).gameObject;

        //Create new texture with properties that I want
        modified_map = new Texture2D(original_map.width, original_map.height, TextureFormat.RGBA32, false);

        //Copy texture from the original to the modified one
        modified_map.SetPixels(original_map.GetPixels());
        modified_map.Apply();

        modified_map_sprite = Sprite.Create(modified_map, new Rect(0, 0, modified_map.width, modified_map.height), new Vector3(0,0,0));

        modified_driving_lane.GetComponent<SpriteRenderer>().sprite = modified_map_sprite;

        Debug.Log(modified_driving_lane.name);
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

            Vector3 right_vector = this.transform.up;
            Vector3 left_vector = this.transform.up - this.transform.right;

            Debug.DrawRay(transform.position, right_vector * 10, Color.green);
            Debug.DrawRay(transform.position, left_vector, Color.green);

            RaycastHit right_raycast; //, left_raycast;

            if (Physics.Raycast(transform.position, right_vector, out right_raycast))
            {
                GameObject map_gameobject = right_raycast.transform.gameObject;
                Texture2D map = map_gameobject.GetComponent<SpriteRenderer>().sprite.texture;

                Debug.Log(map.GetPixel
                (
                    (int)right_raycast.point.x - (int)map_gameobject.transform.position.x + (int)(map.width / 2),
                    (int)right_raycast.point.y - (int)map_gameobject.transform.position.y + (int)(map.height / 2)
                ));

                Texture2D mod_ = modified_driving_lane.GetComponent<SpriteRenderer>().sprite.texture;

                int x_ = (int)right_raycast.point.x - (int)map_gameobject.transform.position.x + (int)(map.width / 2);
                int y_ = (int)right_raycast.point.y - (int)map_gameobject.transform.position.y + (int)(map.height / 2);
                
                mod_.SetPixel(x_, y_, Color.black);

                mod_.Apply();

                Debug.Log("heigth: " + (mod_.height).ToString() + "     width: " + mod_.width.ToString());
                Debug.Log("x: " + x_.ToString() + "      y: " + y_.ToString());
            }
            //if (Physics.Raycast(transform.position, left_vector, out left_raycast))
            //{
                //Debug.Log("Found an object in the left side - distance: " + left_raycast.distance);
            //}

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

        #endregion
    }

    void Search_color_pyxel(Vector3 origin, Vector3 direction)
    {
        //use sprite api and texture2d api to check the color of pyxel

    }
}