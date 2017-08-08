using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeployTile : MonoBehaviour {

    public GameObject[,] tiles;
    public GameObject tile;
    public float startTileX = -55.0f;
    public float startTileZ = -5f;
    public float tileOffsetX = 10.0f;
    public float tileOffsetZ = 10.0f;
    public int tileNumX = 18;
    public int tileNumZ = 10;


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
                tiles[x, z].GetComponent<TileInfo>().rowNumber = x;
                tiles[x, z].GetComponent<TileInfo>().columnNumber = z;
                posZ += tileOffsetZ;
            }
            posX += tileOffsetX;
            posZ = startTileZ;
        }
	}

}
