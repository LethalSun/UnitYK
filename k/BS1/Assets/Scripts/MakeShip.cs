using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeShip : MonoBehaviour {

    public GameObject[] shipPrefab;

    public int[] shipNumber;

    public List<GameObject> ShipDock;

    void Start ()
    {
        int shipIdx = 0;

        ShipDock = new List<GameObject>();

        foreach (int elem in shipNumber)
        {
            for(int i = 0; i<elem;++i)
            {
                GameObject newShip = Instantiate(shipPrefab[shipIdx]) as GameObject;
                newShip.transform.position = gameObject.transform.position;
                ShipDock.Add(newShip);  
            }
            ++shipIdx;
        }
	}
	
}
