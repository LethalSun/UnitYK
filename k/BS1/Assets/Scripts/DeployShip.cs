using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeployShip : MonoBehaviour
{

    public GameObject ship1;
    public GameObject ship2;
    public GameObject ship3;
    public GameObject ship4;
    public GameObject ship5;


    private GameObject selectedShip = null;
    public int shipNum = 0;
    public int shipNum2 = 0;
    private GameObject target;
    private GameObject targetBefore;
    public GameObject initTile;
    private GameObject[] ships;
    private int rotaion = 1;
    
    private void Start()
    {
        targetBefore = initTile;
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
        if (Physics.Raycast(ray, out hit, Mathf.Infinity) == true)
        {
            
            target = hit.collider.gameObject;
            if (target.layer == 31)
            {
         //       Debug.Log("Hit");
                targetBefore = target;
                return target;
            }
            target = null;
        }

        return target;
    }

    void Checkselectedship()
    {

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            MakeShip(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
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
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            shipNum = 0;
            if (selectedShip != null)
            {
                Destroy(selectedShip);
            }
        }
    }

    public void SelectShip1()
    {
        shipNum2 = 1;
        MakeShip(shipNum2);
    }

    public void SelectShip2()
    {
        shipNum2 = 2;
        MakeShip(shipNum2);
    }

    public void SelectShip3()
    {
        shipNum2 = 3;
        MakeShip(shipNum2);
    }

    public void SelectShip4()
    {
        shipNum2 = 4;
        MakeShip(shipNum2);
    }

    public void SelectShip5()
    {
        shipNum2 = 5;
        MakeShip(shipNum2);
    }

    void Update()
    {
        //Checkselectedship();

        //Debug.Log("shipnum =" + shipNum2);
        //MakeShip(shipNum2);

        if (selectedShip == null)
        {
           // Debug.Log("null");
            return;
        }

        CheckRotation();
        target = GetCursorObject();



        if (target == null)
        {
            target = targetBefore;

        }

        UpdateShipPosition();

        if (Input.GetButtonDown("Fire2"))
        {
            if (target != null)
            {
                target.GetComponent<ShipManager>().shipOnTheTile = selectedShip;
                selectedShip = null;
                shipNum = 0;
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
            if(selectedShip !=null)
            {
                Destroy(selectedShip);
            }
            selectedShip = Instantiate(ships[shipNum - 1]) as GameObject;
        }
    }

    void UpdateShipPosition()
    {

        if (rotaion == 1)
        {
            selectedShip.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
            selectedShip.transform.position = new Vector3(target.transform.position.x + selectedShip.GetComponent<ShipOffset>().offset, selectedShip.transform.position.y, target.transform.position.z);
        }
        else if (rotaion == 2)
        {
            selectedShip.transform.rotation = Quaternion.Euler(0.0f, 90.0f, 0.0f);
            selectedShip.transform.position = new Vector3(target.transform.position.x, selectedShip.transform.position.y, target.transform.position.z + selectedShip.GetComponent<ShipOffset>().offset);

        }
        else if (rotaion == 3)
        {
            selectedShip.transform.rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
            selectedShip.transform.position = new Vector3(target.transform.position.x - selectedShip.GetComponent<ShipOffset>().offset, selectedShip.transform.position.y, target.transform.position.z);

        }
        else if (rotaion == 4)
        {
            selectedShip.transform.rotation = Quaternion.Euler(0.0f, 90.0f, 0.0f);
            selectedShip.transform.position = new Vector3(target.transform.position.x, selectedShip.transform.position.y, target.transform.position.z - selectedShip.GetComponent<ShipOffset>().offset);

        }
    }

    void CheckRotation()
    {
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            Debug.Log("wheel" + rotaion);
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
