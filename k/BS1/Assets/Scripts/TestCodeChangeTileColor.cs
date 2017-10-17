using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCodeChangeTileColor : MonoBehaviour {

    public GameObject sphePrefeb;
    public GameObject sh;

	// Use this for initialization
	void Start () {
        sh = Instantiate(sphePrefeb) as GameObject;
        sh.transform.position = new Vector3(this.gameObject.transform.position.x,
            10,
            this.gameObject.transform.position.z);
    }
	
	// Update is called once per frame
	void Update () {
		if(this.gameObject.GetComponent<ShipOnTheTile>().ship == null)
        { 
            sh.GetComponent<MeshRenderer>().material.color = Color.cyan;
        }
        else if (this.gameObject.GetComponent<ShipOnTheTile>().ship.GetComponent<ShipProperties>().shipId == 1 )
        {
            sh.GetComponent<MeshRenderer>().material.color = Color.green;
        }
        else if (this.gameObject.GetComponent<ShipOnTheTile>().ship.GetComponent<ShipProperties>().shipId == 2)
        {
            sh.GetComponent<MeshRenderer>().material.color = Color.blue;
        }
        else if (this.gameObject.GetComponent<ShipOnTheTile>().ship.GetComponent<ShipProperties>().shipId == 3)
        {
            sh.GetComponent<MeshRenderer>().material.color = Color.white;
        }
        else if (this.gameObject.GetComponent<ShipOnTheTile>().ship.GetComponent<ShipProperties>().shipId == 4)
        {
            sh.GetComponent<MeshRenderer>().material.color = Color.red;
        }
        else if (this.gameObject.GetComponent<ShipOnTheTile>().ship.GetComponent<ShipProperties>().shipId == 5)
        {
            sh.GetComponent<MeshRenderer>().material.color = Color.yellow;
        }
    }
}
