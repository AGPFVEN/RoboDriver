using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageConditioning : MonoBehaviour
{
    Texture2D original_map; //
    Texture2D modified_map; //
    GameObject modified_driving_lane;
    Sprite modified_map_sprite;

    void Start()
    {
        //Get the Sprite of the driving line (from the gameobject, which is a child object)
        original_map = transform.GetChild(0).GetComponent<SpriteRenderer>().sprite.texture;
        float heigth_of_images = original_map.height;
        float width_of_images = original_map.width;
        
        //Get the modified driving lane (child object)
        modified_driving_lane = transform.GetChild(1).gameObject;

        //Create new texture with properties that I want
        modified_map = new Texture2D(original_map.width, original_map.height, TextureFormat.RGBA32, false);

        //Copy texture from the original to the modified one
        modified_map.SetPixels(original_map.GetPixels());
        modified_map.Apply();

        modified_map_sprite = Sprite.Create(modified_map, new Rect(0, 0, modified_map.width, modified_map.height), new Vector3(0,0,0));

        modified_driving_lane.GetComponent<SpriteRenderer>().sprite = modified_map_sprite;

        Debug.Log(modified_driving_lane.name);
    }

    void Update()
    { 
        modified_driving_lane.GetComponent<SpriteRenderer>().sprite = modified_map_sprite;
    }
}
