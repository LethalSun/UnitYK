using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollideManager : MonoBehaviour
{

    private Color normColor;

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Triggered");
        if(other.gameObject.tag == "Ship")
        {
            gameObject.GetComponent<ShipManager>().shipOnTheTile = other.gameObject;
            //Debug.Log("ship deploy");

        }
       
    }

    private void OnTriggerExit(Collider other)
    {
        //Debug.Log("Triggered exit");
        if (other.gameObject.tag == "Ship")
        {
            gameObject.GetComponent<ShipManager>().shipOnTheTile = null;

        }
    }
}
