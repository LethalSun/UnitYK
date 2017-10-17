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
    public Text statetext;

    public string ID;
    public string PW;

    string notLoginedStr;
    string logingigStr;

    float accumulatedTime = 0.0f;

    enum State
    {
        NOTLOGINED = 1,
        LOGINGING = 2,
        LOGINED = 3,
    }

    State state = State.NOTLOGINED;

    private void Start()
    {
        notLoginedStr = "Please Enter ID and PW.";
        logingigStr = "Loading";
    }

    

    private void Update()
    {
        switch(state)
        {
            case State.NOTLOGINED :
                {
                    statetext.text = notLoginedStr;
                    break;
                }
            case State.LOGINGING:
                {
                    accumulatedTime += Time.deltaTime;
                    
                    if(accumulatedTime <= 0.33f)
                    {
                        break;
                    }
                    else
                    {
                        accumulatedTime = 0.0f;
                    }

                    if (logingigStr.Length < 12)
                    {
                        logingigStr += ".";
                    }
                    else
                    {
                        logingigStr = logingigStr.Substring(0, 7);
                    }
                    statetext.text = logingigStr;
                    break;
                }
            case State.LOGINED:
                {
                    MakeLogin();
                    break;
                }
            default:
                break;

        }
    }

    public void IDFieldChanged(string id)
    {
        ID = id;
    }


    public void PWFieldChanged(string pw)
    {
        PW = pw;
    }

    public void MakeLogin()
    {
        loginCanvas.SetActive(false);
        mainCamera.transform.position = playCameraPosition;
        uiCanvas.SetActive(true);
    }

    public void TryLogin()
    {
        StartCoroutine(TempFunc());
    }

    public IEnumerator TempFunc()
    {
        state = State.LOGINGING;

        string isOK = "";
        yield return GetComponent<HTTPLib>().RequestHttpLoginOrCreateUser(ID, PW,(L)=> { isOK = L; });

        if(isOK == "")
        {
            state = State.NOTLOGINED;
        }
        else
        {
            state = State.LOGINED;
        }

        //state = State.LOGINED;
    }


}
