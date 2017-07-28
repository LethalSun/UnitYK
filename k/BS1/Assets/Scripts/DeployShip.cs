using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeployShip : MonoBehaviour {

    public GameObject ship1;
    public GameObject ship2;
    public GameObject ship3;
    public GameObject ship4;
    public GameObject ship5;


    private GameObject selectedShip = null;
    public int shipNum = 0;
    private GameObject target;
    public GameObject initTile;
    private GameObject[] ships;
    private int rotaion = 1;

    private void Start()
    {
        ships = new GameObject[5];
        ships[0] = ship1;
        ships[1] = ship2;
        ships[2] = ship3;
        ships[3] = ship4;
        ships[4] = ship5;
    }

    GameObject GetCursorObject()
    {
        RaycastHit hit;
        GameObject target = null;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit,Mathf.Infinity) == true)
        {
            target = hit.collider.gameObject;
            if(target.layer == 31)
            {

                return target;
            }
            target = null;
        }

        return target;
    }

    void Checkselectedship()
    {

        if(Input.GetKeyDown(KeyCode.Alpha1))
        {

            MakeShip(1);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            MakeShip(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            MakeShip(3);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            MakeShip(4);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            MakeShip(5);
        }
        else if(Input.GetKeyDown(KeyCode.Escape))
        {
            shipNum = 0;
            if(selectedShip != null)
            {
                Destroy(selectedShip);
            }
        }
    }
    void Update()
    {
        Checkselectedship();
        CheckRotation();
        if (selectedShip == null)
        {
            return;
        }

        target = GetCursorObject();

        if(target == null)
        {
            target = initTile;
        }

        UpdateShipPosition();

        if (Input.GetButtonDown("Fire1"))
        {
            if (target != null)
            {
                target.GetComponent<ShipManager>().shipOnTheTile = selectedShip;
                selectedShip = null;
            }
        }
    }

    void MakeShip(int shipN)
    {
        if (shipNum == shipN)
        {
            return;
        }
        else
        {

            shipNum = shipN;
            Destroy(selectedShip);
            selectedShip = Instantiate(ships[shipNum - 1]) as GameObject;
        }
    }

    void UpdateShipPosition()
    {
        if(rotaion ==1)
        {
            selectedShip.transform.rotation = Quaternion.Euler(0.0f,0.0f,0.0f);
            selectedShip.transform.position = new Vector3(target.transform.position.x + selectedShip.GetComponent<ShipOffset>().offset, selectedShip.transform.position.y, target.transform.position.z);
        }
        else if( rotaion ==2)
        {
            selectedShip.transform.rotation = Quaternion.Euler(0.0f, 90.0f, 0.0f);
            selectedShip.transform.position = new Vector3(target.transform.position.x , selectedShip.transform.position.y, target.transform.position.z + selectedShip.GetComponent<ShipOffset>().offset);

        }
        else if(rotaion ==3)
        {
            selectedShip.transform.rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
            selectedShip.transform.position = new Vector3(target.transform.position.x - selectedShip.GetComponent<ShipOffset>().offset, selectedShip.transform.position.y, target.transform.position.z);

        }
        else if(rotaion == 4)
        {
            selectedShip.transform.rotation = Quaternion.Euler(0.0f, 90.0f, 0.0f);
            selectedShip.transform.position = new Vector3(target.transform.position.x , selectedShip.transform.position.y, target.transform.position.z - selectedShip.GetComponent<ShipOffset>().offset);

        }
    }

    void CheckRotation()
    {
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            Debug.Log("wheel"+ rotaion);
            if (rotaion == 4)
            {
                rotaion = 0;
            }

            ++rotaion;
            Debug.Log("wheel" + rotaion);
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            Debug.Log("wheel" + rotaion);
            if (rotaion == 1)
            {
                rotaion = 5;
            }

            --rotaion;
            Debug.Log("wheel" + rotaion);
        }
    }
}
