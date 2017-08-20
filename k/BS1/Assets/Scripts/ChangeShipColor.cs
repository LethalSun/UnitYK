using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeShipColor : MonoBehaviour
{

    public Material[] mats;
    public Renderer[] rs;

	// Update is called once per frame
	void Update ()
    {
        bool isEdge = this.gameObject.GetComponent<ShipProperties>().isEncounterEdge;
        bool isShip = this.gameObject.GetComponent<ShipProperties>().isEncounterShip;
        bool isDeployed = this.gameObject.GetComponent<ShipProperties>().isDeployed;

        if (isDeployed == true)
        {
            return;
        }

        if (isEdge == true || isShip == true)
        {
            for (int i = 0; i < rs.Length; ++i)
            {
                rs[i].material = mats[0];
            }
        }
        else
        {
            for (int i = 0; i < rs.Length; ++i)
            {
                rs[i].material = mats[1];
            }
        }
	}
}
