using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeployTile : MonoBehaviour {

    public GameObject[,] tiles;
    public GameObject tile;
    private float startTileX = -55.0f;
    private float startTileZ = -73.3f;
    private float tileOffsetX = 10.0f;
    private float tileOffsetZ = 10.0f;
    private const int tileNumX = 12;
    private const int tileNumZ = 14;


    // Use this for initialization
    void Start () {

        tiles = new GameObject[tileNumX, tileNumZ];
        float posX = startTileX;
        float posZ = startTileZ;

        for (int x = 0; x< tileNumX; ++x)
        {
            for(int z = 0;z< tileNumZ; ++z )
            {
                tiles[x, z] = Instantiate(tile) as GameObject;
                tiles[x, z].transform.position = new Vector3(posX, 10.0f, posZ);
                posZ += tileOffsetZ;
            }
            posX += tileOffsetX;
            posZ = startTileZ;
        }
	}

}
