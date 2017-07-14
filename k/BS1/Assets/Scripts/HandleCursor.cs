using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleCursor : MonoBehaviour {

    public Texture2D BasicCursor;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;

	// Use this for initialization
	void Start () {
        //Screen.LockCursor
        // Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = true;
        //Cursor.lockState = CursorLockMode.Confined;
    }
	
	// Update is called once per frame
	void Update () {
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = true;
        Cursor.SetCursor(BasicCursor, hotSpot, cursorMode);
        //Cursor.lockState = CursorLockMode.Confined;
    }
}
