using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeployBomb : MonoBehaviour {

    public GameObject Armory;

    public GameObject initTile;

    GameObject selectedBomb = null;


    private GameObject target;
    private GameObject targetBefore;
	// Use this for initialization
	void Start ()
    {
        targetBefore = initTile;
    }
	
	// Update is called once per frame
	void Update () {
		
        if(selectedBomb == null)
        {
            return;
        }

        selectedBomb.GetComponent<BombProperty>().isDeployed = false;
        target = GetCursorObject();

        if(target == null)
        {
            target = targetBefore;
        }

        UpdateBombPosition();


        if (Input.GetButtonDown("Fire2"))
        {
            if (target != null)
            {
                if (selectedBomb.GetComponent<BombProperty>().isEncounterBomb == true)
                {
                    return;
                }

                var randerer = selectedBomb.GetComponent<ChangeBombColor>().rs;
                var mat = selectedBomb.GetComponent<ChangeBombColor>().mats;

                for (int i = 0; i < randerer.Length; ++i)
                {
                    randerer[i].material = mat[2];
                }

                selectedBomb.GetComponent<BombProperty>().isDeployed = true;
                selectedBomb = null;
            }
        }

    }

    void makeBomb()
    {
        var bombs = Armory.GetComponent<MakeBomb>().bombStorage;

        foreach(var elem in bombs)
        {
            var property = elem.GetComponent<BombProperty>();
            if(property.isDeployed == false)
            {
                selectedBomb = elem;
                break;
            }
        }
    }

    void UpdateBombPosition()
    {
        selectedBomb.transform.position = target.transform.position;
    }

    GameObject GetCursorObject()
    {
        RaycastHit hit;
        GameObject target = null;

        int rayMask = 1 << 28;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, rayMask) == true)
        {

            target = hit.collider.gameObject;
            if (target.layer == 28)
            {
                targetBefore = target;
                return target;
            }
            target = null;
        }

        return target;
    }

    public void SelectBomb()
    {
        makeBomb();
    }
}
