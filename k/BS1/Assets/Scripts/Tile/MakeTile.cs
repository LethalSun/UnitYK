using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeTile : MonoBehaviour {

    public GameObject tilePrefabRed;
    public GameObject tilePrefabBlue;

    public List<GameObject> tileStorage;
    public List<GameObject> tileStorageNormal;
    public int tileRow;
    public int tileColumn;
    // Use this for initialization
    void Start()
    {
        int tileNum = tileRow * tileColumn;
        int uid = 0;
        for (int i = 0; i < tileNum; ++i)
        {
            GameObject newTile = Instantiate(tilePrefabRed) as GameObject;
            newTile.transform.position = gameObject.transform.position;
            newTile.GetComponent<TileProperty>().UID = uid;
            tileStorage.Add(newTile);

            GameObject newTile1 = Instantiate(tilePrefabBlue) as GameObject;
            newTile1.transform.position = gameObject.transform.position;
            newTile1.GetComponent<TileProperty>().UID = uid;
            tileStorageNormal.Add(newTile1);

            ++uid;
        }
    }

    public void ResetObject()
    {
        int tileNum = tileRow * tileColumn;

        for (int i = 0; i < tileNum; ++i)
        {
            tileStorageNormal[i].transform.position = gameObject.transform.position;

            tileStorageNormal[i].GetComponent<TileProperty>().isUsed = false;

            tileStorage[i].transform.position = gameObject.transform.position;

            tileStorage[i].GetComponent<TileProperty>().isUsed = false;
        }
    }
}
