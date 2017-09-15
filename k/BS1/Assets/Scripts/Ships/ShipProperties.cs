using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipProperties : MonoBehaviour
{
    public int shipId;
    public int shipUID;
    public float shipOffset;
    public bool isEncounterEdge = false;
    public bool isEncounterShip = false;
    public bool isDeployed = false;
    public int shipsTileNum = 1;

    public GameObject[] tileInfos;

    private void Start()
    {
        tileInfos = new GameObject[shipsTileNum];
    }
}
