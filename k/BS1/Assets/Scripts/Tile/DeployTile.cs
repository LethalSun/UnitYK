using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeployTile : MonoBehaviour {

    //TODO:각각의 2차원배열을 1차원 배열로 수정
    public GameObject[] tiles;
    public GameObject tile;
    public GameObject[] enemyTiles;
    public GameObject enemyTile;
    public float startTileX = -60.0f;
    public float startTileZ = -15.0f;
    public float startEnemyTileX = -60.0f;
    public float startEnemyTileY = 80.0f;
    public float tileOffsetX = 10.0f;
    public float tileOffsetZ = -10.0f;
    public int tileNumX = 13;
    public int tileNumZ = 8;


    // Use this for initialization
    void Awake () {

        tiles = new GameObject[tileNumX * tileNumZ];
        enemyTiles = new GameObject[tileNumX * tileNumZ];
        float posX = startTileX;
        float posZ = startTileZ;

        float posEx = startEnemyTileX;
        float posEz = startEnemyTileY;

        for (int x = 0; x< tileNumX; ++x)
        {
            for(int z = 0;z< tileNumZ; ++z )
            {
                
                tiles[tileNumZ * x + z] = Instantiate(tile) as GameObject;
                tiles[tileNumZ * x + z].transform.position = new Vector3(posX, 10.0f, posZ);
                tiles[tileNumZ * x + z].GetComponent<TileInfo>().rowNumber = x;
                tiles[tileNumZ * x + z].GetComponent<TileInfo>().columnNumber = z;
                tiles[tileNumZ * x + z].GetComponent<TileInfo>().indexInList = tileNumZ * x + z;
                posZ += tileOffsetZ;

                enemyTiles[tileNumZ * x + z] = Instantiate(enemyTile) as GameObject;
                enemyTiles[tileNumZ * x + z].transform.position = new Vector3(posEx, 10.0f, posEz);
                enemyTiles[tileNumZ * x + z].GetComponent<TileInfo>().rowNumber = x;
                enemyTiles[tileNumZ * x + z].GetComponent<TileInfo>().columnNumber = z;
                enemyTiles[tileNumZ * x + z].GetComponent<TileInfo>().indexInList = tileNumZ * x + z;

                posEz += tileOffsetZ;


            }
            posX += tileOffsetX;
            posZ = startTileZ;
            posEx += tileOffsetX;
            posEz = startEnemyTileY;
        }
	}

    public void ResetObject()
    {
        int tileNum = tileNumX * tileNumZ;

        for (int i = 0; i< tileNum; ++i)
        {
            tiles[i].GetComponent<ShipOnTheTile>().ship = null;

            enemyTiles[i].GetComponent<EnemyTileProperty>().isThereEnemyShip = false;

            enemyTiles[i].GetComponent<EnemyTileProperty>().isBombDeployed = false;

            enemyTiles[i].GetComponent<BombOnTheEnemyTile>().bomb = null;
        }
    }

}
