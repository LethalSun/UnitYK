using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeployTile : MonoBehaviour {

    public GameObject[,] tiles;
    public GameObject tile;
    public GameObject[,] enemyTiles;
    public GameObject enemyTile;
    public float startTileX = -60.0f;
    public float startTileZ = -15.0f;
    public float startEnemyTileX = -60.0f;
    public float startEnemyTileY = 80.0f;
    public float tileOffsetX = 10.0f;
    public float tileOffsetZ = -10.0f;
    public int tileNumX = 13;
    public int tileNumZ = 18;


    // Use this for initialization
    void Start () {

        tiles = new GameObject[tileNumX, tileNumZ];
        enemyTiles = new GameObject[tileNumX, tileNumZ];
        float posX = startTileX;
        float posZ = startTileZ;

        float posEx = startEnemyTileX;
        float posEz = startEnemyTileY;

        for (int x = 0; x< tileNumX; ++x)
        {
            for(int z = 0;z< tileNumZ; ++z )
            {
                tiles[x, z] = Instantiate(tile) as GameObject;
                tiles[x, z].transform.position = new Vector3(posX, 10.0f, posZ);
                tiles[x, z].GetComponent<TileInfo>().rowNumber = x;
                tiles[x, z].GetComponent<TileInfo>().columnNumber = z;
                posZ += tileOffsetZ;

                enemyTiles[x,z] = Instantiate(enemyTile) as GameObject;
                enemyTiles[x, z].transform.position = new Vector3(posEx, 10.0f, posEz);
                enemyTiles[x, z].GetComponent<TileInfo>().rowNumber = x;
                enemyTiles[x, z].GetComponent<TileInfo>().columnNumber = z;

                //TODO: 테스트 코드 나중에 값을 받아와서 채워넣어야 함.
                if(Random.Range(0,2) == 0)
                {
                    enemyTiles[x, z].GetComponent<EnemyTileProperty>().isThereEnemyShip = false;
                }
                else
                {
                    enemyTiles[x, z].GetComponent<EnemyTileProperty>().isThereEnemyShip = true;
                }

                posEz += tileOffsetZ;


            }
            posX += tileOffsetX;
            posZ = startTileZ;
            posEx += tileOffsetX;
            posEz = startEnemyTileY;
        }
	}

}
