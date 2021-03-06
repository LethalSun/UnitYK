﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeBomb : MonoBehaviour {
    public GameObject bombPrefab;

    public List<GameObject> bombStorage;

    public int tileRow;
    public int tileColumn;
    // Use this for initialization
    void Start ()
    {
        int tileNum = tileRow * tileColumn;
        int uid = 0;
        for(int i = 0; i<tileNum;++i)
        {
            GameObject newBomb = Instantiate(bombPrefab) as GameObject;
            newBomb.transform.position = gameObject.transform.position;
            newBomb.GetComponent<BombProperty>().bombUID = uid;
            ++uid;
            bombStorage.Add(newBomb);
        }
	}

    public void ResetObject()
    {
        int tileNum = tileRow * tileColumn;
        for (int i = 0; i < tileNum; ++i)
        {
            bombStorage[i].transform.position = gameObject.transform.position;

            bombStorage[i].GetComponent<BombProperty>().isDeployed = false;

            bombStorage[i].GetComponent<BombProperty>().isEncounterBomb = false;

            bombStorage[i].GetComponent<BombProperty>().tileIndex = 0;

        }
    }

}
