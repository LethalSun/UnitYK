using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeployShip : MonoBehaviour
{

    private GameObject selectedShip = null;
    public int shipNum = 0;
    public int shipNum2 = 0;
    private GameObject target;
    private GameObject targetBefore;
    public GameObject initTile;
    private int rotaion = 1;

    public GameObject harbor;
    private void Start()
    {
        targetBefore = initTile;
    }

    GameObject GetCursorObject()
    {
        RaycastHit hit;
        GameObject target = null;

        int rayMask = 1 << 31;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, rayMask) == true)
        {
            
            target = hit.collider.gameObject;
            if (target.layer == 31)
            {
                targetBefore = target;
                return target;
            }
            target = null;
        }

        return target;
    }

    GameObject ClickedShip()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            RaycastHit hit;
            GameObject target = null;
            int rayMask = 1 << 29;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, rayMask) == true)
            {

                target = hit.collider.gameObject;
                if (target.layer == 29)
                {
                    targetBefore = target;
                    return target;
                }
                target = null;
            }

            return target;
        }
        else
        {
            return null;
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

        if (selectedShip == null)
        {
            Debug.Log("ship null");
            selectedShip = ClickedShip();
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
                selectedShip.GetComponent<ShipProperties>().isDeployed = true;
                selectedShip = null;
                shipNum = 0;
            }
        }
    }

    void MakeShip(int shipN)
    {
        var ships = harbor.GetComponent<MakeShip>().ShipDock;

        foreach(var elem in ships)
        {
            var property = elem.GetComponent<ShipProperties>();
            if(property.shipId == shipN && property.isDeployed == false)
            {
                selectedShip = elem;
                break;
            }
        }
    }

    void UpdateShipPosition()
    {

        if (rotaion == 1)
        {
            selectedShip.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
            selectedShip.transform.position = new Vector3(target.transform.position.x + selectedShip.GetComponent<ShipProperties>().shipOffset, selectedShip.transform.position.y, target.transform.position.z);
        }
        else if (rotaion == 2)
        {
            selectedShip.transform.rotation = Quaternion.Euler(0.0f, 90.0f, 0.0f);
            selectedShip.transform.position = new Vector3(target.transform.position.x, selectedShip.transform.position.y, target.transform.position.z + selectedShip.GetComponent<ShipProperties>().shipOffset);

        }
        else if (rotaion == 3)
        {
            selectedShip.transform.rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
            selectedShip.transform.position = new Vector3(target.transform.position.x - selectedShip.GetComponent<ShipProperties>().shipOffset, selectedShip.transform.position.y, target.transform.position.z);

        }
        else if (rotaion == 4)
        {
            selectedShip.transform.rotation = Quaternion.Euler(0.0f, 90.0f, 0.0f);
            selectedShip.transform.position = new Vector3(target.transform.position.x, selectedShip.transform.position.y, target.transform.position.z - selectedShip.GetComponent<ShipProperties>().shipOffset);

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
