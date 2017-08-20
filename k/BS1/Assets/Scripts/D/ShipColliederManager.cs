using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipColliederManager : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Triggered");
        if (other.gameObject.tag == "Ship")
        {
            gameObject.GetComponent<ShipManager>().shipOnTheTile = other.gameObject;
            //Debug.Log("ship enter");

        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Ship")
        {
            gameObject.GetComponent<ShipManager>().shipOnTheTile = null;
            //Debug.Log("ship exit");
        }
    }
}
