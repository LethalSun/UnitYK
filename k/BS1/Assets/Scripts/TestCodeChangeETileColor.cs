using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCodeChangeETileColor : MonoBehaviour {

    public GameObject sphePrefeb;
    public GameObject sh;

    // Use this for initialization
    void Start()
    {
        sh = Instantiate(sphePrefeb) as GameObject;
        sh.transform.position = new Vector3(this.gameObject.transform.position.x,
            10,
            this.gameObject.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.GetComponent<BombOnTheEnemyTile>().bomb == null)
        {

            sh.GetComponent<MeshRenderer>().material.color = Color.cyan;
        }
        else 
        {
            
            sh.GetComponent<MeshRenderer>().material.color = Color.green;
        }

    }
}
