using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatesManager : MonoBehaviour
{
    //Array of gates
    public GameObject[] gates;

    //Selected gate (Gate that the car needs to pass)
    public int selected_gate;

    void Awake(){
        //All gates in an array of gates
        gates = new GameObject[transform.childCount];

        gates[0] = transform.GetChild(0).gameObject; // I add the first separeted because it simplify the calculations
        
        //Select the first gate as the selected gate
        selected_gate = 0;

        for (int i = 1; i < transform.childCount; i++){
            // Add gate to the array
            gates[i] = transform.GetChild(i).gameObject;

            // Desactivate gate
            gates[i].SetActive(false);
        }
    }

    //Desactivate current gate and activate next
    public void ActivateNextGate()
    {
        Debug.Log(gameObject.name);
        //Desactivate selected
        gates[selected_gate].SetActive(false);

        //Select new selected gate
        if (selected_gate == gates.Length - 1){
            selected_gate = 0;
        }
        else{
            selected_gate++;
        }

        //Activate next gate
        gates[selected_gate].SetActive(true);

    }
}
