using UnityEngine;
using UnityEngine.SceneManagement;

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
    void Start(){
        //Components
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        #region Rotation (inputs)
        
            if (Input.GetKey(KeyCode.D)){
                transform.Rotate(-Vector3.forward * rotation_coefficient);
            }
            else if (Input.GetKey(KeyCode.A)){
                transform.Rotate(Vector3.forward * rotation_coefficient);
            }

        #endregion

        #region Raycasts
            //All the Inputs of the Neural Network
            NewRoboRay(transform.up);
            NewRoboRay(transform.right);
            NewRoboRay(-transform.right);
            NewRoboRay(transform.up + transform.right);
            NewRoboRay(transform.up - transform.right);

        #endregion

        #region Movement (inputs)

            //Acceleration
            if (Input.GetKey(KeyCode.W)){
                actual_direction = transform.up;
                rb.AddForce(actual_direction * thrust);
            }

            //Friction
            rb.velocity *= friction;

        #endregion
    }

    void OnTriggerEnter2D(Collider2D other) {
        Debug.Log(other.gameObject.transform.parent.name);

        if (other.gameObject.transform.parent.name == "Image Manager"){
            SceneManager.LoadScene("SampleScene");
        }
    }

    //change void to float
    float NewRoboRay(Vector2 robovector){
        //Draw the vector of the ray
        Debug.DrawRay(transform.position, robovector.normalized * 20, Color.green);

        //Create the ray array
        RaycastHit2D[] roboraycast = Physics2D.RaycastAll(transform.position, robovector);

        if (roboraycast.Length >= 2)
        {
            return roboraycast.Length;
        }
        else
        {
            return 0;
        }
    }
}