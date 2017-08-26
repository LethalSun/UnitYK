using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeBombColor : MonoBehaviour
{
    public Material[] mats;
    public Renderer[] rs;
    // Update is called once per frame
    void Update () {

        bool isBomb = this.gameObject.GetComponent<BombProperty>().isEncounterBomb;
        bool isDeployed = this.gameObject.GetComponent<BombProperty>().isDeployed;
        if (isDeployed == true)
        {
            return;
        }

        if(isBomb == true)
        {
            for(int i = 0;i<rs.Length; ++i)
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
