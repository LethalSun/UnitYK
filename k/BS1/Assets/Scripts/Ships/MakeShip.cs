﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeShip : MonoBehaviour {

    public GameObject[] shipPrefab;

    public int[] shipNumber;

    public List<GameObject> ShipDock;

    void Start ()
    {
        int shipIdx = 0;

        int shipUID = 0;
        ShipDock = new List<GameObject>();

        foreach (int elem in shipNumber)
        {
            for(int i = 0; i<elem;++i)
            {
                GameObject newShip = Instantiate(shipPrefab[shipIdx]) as GameObject;
                newShip.transform.position = gameObject.transform.position;
                newShip.GetComponent<ShipProperties>().shipUID = shipUID;
                ++shipUID;
                ShipDock.Add(newShip);  
            }
            ++shipIdx;
        }
	}

    public void ResetObject()
    {
        for (int i = 0; i < ShipDock.Count;++i)
        {
            ShipDock[i].transform.position = gameObject.transform.position;

            ShipDock[i].GetComponent<ShipProperties>().isDeployed = false;

            ShipDock[i].GetComponent<ShipProperties>().isEncounterEdge = false;

            ShipDock[i].GetComponent<ShipProperties>().isEncounterShip = false;
        }
    }
}
