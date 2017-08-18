using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoveLikeRTS : MonoBehaviour
{

    
    public float worldMaxX = 200.0f;
    public float worldMaxY = 200.0f;

    public float screenMaxX = (float)Screen.width / 2;
    public float screenMaxY = (float)Screen.height / 2;

    public float cursorAtWorldX = 0.0f;
    public float cursorAtWorldY = 0.0f;

    public float cursorAtScreenX = 0.0f;
    public float cursorAtScreenY = 0.0f;

    public float sensitivity = 200.0f;

    float beforeDirectionX;
    float beforeDirectionY;
	// Update is called once per frame
	void Update ()
    {
        float dx = Input.GetAxis("Mouse X");
        float dy = Input.GetAxis("Mouse Y");

        

        if (cursorAtWorldX <= worldMaxX &&
            cursorAtWorldX >= -worldMaxX)
        {
            cursorAtWorldX += dx * sensitivity * Time.deltaTime;
        }
        else
        {
            dx = 0.0f;
        }

        if (cursorAtWorldY <= worldMaxY &&
            cursorAtWorldY >= -worldMaxY)
        {
            cursorAtWorldY += dy * sensitivity * Time.deltaTime;
        }
        else
        {
            dy = 0.0f;
        }

        if(cursorAtScreenX <= screenMaxX &&
            cursorAtScreenX >= -screenMaxX)
        {
            cursorAtScreenX += dx * sensitivity * Time.deltaTime;
        }
        else
        {
            dx = dx* sensitivity *Time.deltaTime;
            transform.position += new Vector3(dx, 0,0);
        }

        if (cursorAtScreenY <= screenMaxY &&
            cursorAtScreenY >= -screenMaxY)
        {
            cursorAtScreenY += dy * sensitivity * Time.deltaTime;
        }
        else
        {
            dy = dy * sensitivity * Time.deltaTime;
            transform.position += new Vector3(0, 0, dy);
        }


    }
    
}
