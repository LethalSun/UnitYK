using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mmmm : MonoBehaviour
{
    public Material[] mats;
    public Renderer[] rs;

	
	void Update ()
    {
		if (Input.GetKeyDown(KeyCode.A))
        {
            for(int i=0; i< rs.Length;++i)
            {
                rs[i].material = mats[0];
            }
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            for (int i = 0; i < rs.Length; ++i)
            {
                rs[i].material = mats[1];
            }
        }

    }
}
