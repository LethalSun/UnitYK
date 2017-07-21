using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeployShip : MonoBehaviour {

    public GameObject ship1;
    public GameObject ship2;
    public GameObject ship3;
    public GameObject ship4;
    public GameObject ship5;


    private GameObject selectedShip;
    public int shipNum = 1;
    private GameObject target;
    private GameObject[] ships;

    private void Start()
    {
        ships = new GameObject[5];
        ships[0] = ship1;
        ships[1] = ship2;
        ships[2] = ship3;
        ships[3] = ship4;
        ships[4] = ship5;
    }
    GameObject GetClickedObject()
    {
        RaycastHit hit;
        GameObject target = null;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.Log("ray");
        if (Physics.Raycast(ray, out hit,Mathf.Infinity) == true)
        {
            target = hit.collider.gameObject;
            if(target.layer == 31)
            {
                Debug.Log("rayhit", target);
            }

        }

        return target;
    }

    GameObject Checkselectedship()
    {

        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            shipNum = 1;
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            shipNum = 2;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            shipNum = 3;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            shipNum = 4;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            shipNum = 5;
        }

        return ships[shipNum-1];
    }
    void Update ()
    {
        selectedShip = Checkselectedship();

		if(Input.GetButtonDown("Fire1"))
        {
            target = GetClickedObject();
            if(target != null)
            {
                GameObject shipObj = Instantiate(selectedShip) as GameObject;
                shipObj.transform.position = new Vector3(target.transform.position.x + shipObj.GetComponent<ShipOffset>().offset, shipObj.transform.position.y, target.transform.position.z);
            }
        }
	}


}
