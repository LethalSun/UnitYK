using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetBombByTrigger : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 27)
        {
            if (this.gameObject.GetComponent<BombOnTheEnemyTile>().bomb == null)
            {
                this.gameObject.GetComponent<BombOnTheEnemyTile>().bomb = other.gameObject;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 27)
        {
            if (this.gameObject.GetComponent<BombOnTheEnemyTile>().bomb.GetComponent<BombProperty>().bombUID ==
                other.gameObject.GetComponent<BombProperty>().bombUID)
            { 
                this.gameObject.GetComponent<BombOnTheEnemyTile>().bomb = null;
            }
        }
    }
}
