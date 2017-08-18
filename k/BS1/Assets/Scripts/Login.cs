using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Login : MonoBehaviour
{
    public GameObject uiCanvas;
    public GameObject loginCanvas;
    public GameObject mainCamera;

    public Vector3 playCameraPosition;

    public InputField inputFieldID;
    public InputField inputFieldPW;

    public string ID;
    public string PW;

    public void IDFieldChanged(string id)
    {
        string test = "test";
        Debug.Log(test);

        ID = id;
        Debug.Log(ID);
    }


    public void PWFieldChanged(string pw)
    {
        string test = "test";
        Debug.Log(test);
        PW = inputFieldPW.text;
        Debug.Log(PW);
    }

    public void MakeLogin()
    {
        loginCanvas.SetActive(false);
        mainCamera.transform.position = playCameraPosition;
        uiCanvas.SetActive(true);
    }
}
