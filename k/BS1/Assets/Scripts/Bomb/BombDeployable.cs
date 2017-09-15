using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombDeployable : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer ==  27)
        {
            //bomb
            this.gameObject.GetComponent<BombProperty>().isEncounterBomb = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 27)
        {
            //bomb
            this.gameObject.GetComponent<BombProperty>().isEncounterBomb = false;
        }
    }

}
