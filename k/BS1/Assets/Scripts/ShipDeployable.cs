using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipDeployable : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 30)
        {
            //edge
            this.gameObject.GetComponent<ShipProperties>().isEncounterEdge = true;
        }
        else if(other.gameObject.layer ==29)
        {
            //othership
            this.gameObject.GetComponent<ShipProperties>().isEncounterShip = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 30)
        {
            //edge
            this.gameObject.GetComponent<ShipProperties>().isEncounterEdge = false;
        }
        else if (other.gameObject.layer == 29)
        {
            //othership
            this.gameObject.GetComponent<ShipProperties>().isEncounterShip = false;
        }
    }
}
