using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetShipByTrigger : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 29)
        {
            if(this.gameObject.GetComponent<ShipOnTheTile>().ship == null)
            {
                this.gameObject.GetComponent<ShipOnTheTile>().ship = other.gameObject;
            }

            //Debug.Log("ship in");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 29)
        {
            var a = this.gameObject.GetComponent<ShipOnTheTile>().ship.GetComponent<ShipProperties>();
            if (this.gameObject.GetComponent<ShipOnTheTile>().ship.GetComponent<ShipProperties>().shipUID == 
                other.gameObject.GetComponent<ShipProperties>().shipUID)
            {
                this.gameObject.GetComponent<ShipOnTheTile>().ship = null;
            }

            //Debug.Log("ship out");
        }
    }
}
