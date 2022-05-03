using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateScript : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.name == "Robodriver"){
            transform.parent.GetComponent<GatesManager>().ActivateNextGate();
        }
    }
}
