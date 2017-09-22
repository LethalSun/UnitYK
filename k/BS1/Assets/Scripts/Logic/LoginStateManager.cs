using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginStateManager : MonoBehaviour {

    AppManager appManager;
    GameObject mainCamera;
    GameObject loginCanvas;
    GameObject deployShipCanvas;
    GameObject playCanvas;
    GameObject playEndCanvas;

    HTTPLib httpNet;
    TcpIpLib tcpNet;
    NetworkManager networkManager;

    string ID;
    string PW;
    string AuthToken;

    float accumulatedTime = 0.0f;

    public void InitObject()
    {
        //초기화 Appmanager 에서 모두 각각의 오브젝트를 초기화시킨다.
        appManager = AppManager.GetInstance();

        mainCamera = appManager.mainCamera;

        loginCanvas = appManager.loginCanvas;

        deployShipCanvas = appManager.deployShipCanvas;

        playCanvas = appManager.playCanvas;

        playEndCanvas = appManager.playEndCanvas;

        networkManager = NetworkManager.GetInstance();
        httpNet = networkManager.httpNetwork;
        tcpNet = networkManager.tcpipNetwork;

        AddNetworkEvent();
    }

    void AddNetworkEvent()
    {
        networkManager.OnGameServerEnterRes += OnGameServerEnterRes;
    }

    void OnGameServerEnterRes(Packet.GAMESEVER_RES_GAMESERVER_ENTER pkt)
    {
        if(pkt.Result ==(int)NetworkManager.TcpError.None)
        {
            MakeLogin();
        }
        else
        {
            appManager.loginCanvasStatetext.text = appManager.loginFailStr;
            AuthToken = "";
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
        appManager.currentState = AppManager.State.DEPLOY_STATE;
        appManager.ID = ID;
        appManager.PW = PW;
        appManager.AuthToken = AuthToken;
        //AppManager의 현재스테이트를 DEPLOY_STATE로 바꾼다. stateChaned = true 로 한다.
        //ID, PW ,AuthKey를 AppManager에 저장한다.
        //그러면 AppManager가 관련된 오브젝트를 활성화 시킨다.

    }

    public void OnLoginButtenPushed()
    {
        Debug.Log(ID + " " + PW + " Try Login");
        if (ID.Length == 0 || PW.Length == 0)
        {
            appManager.loginCanvasStatetext.text = appManager.loginFailStr;
            return;
        }
        else
        {
            StartCoroutine(TryLogin(ID, PW));
        }

    }

    public IEnumerator TryLogin(string id, string pw)
    {
        string auth = "";

        Debug.Log("Login Start");

        yield return httpNet.RequestHttpLoginOrCreateUser(id, pw, (L) => { auth = L; });

        if (auth == "")
        {
            Debug.Log("LoginServer Fail");
            appManager.loginCanvasStatetext.text = appManager.loginFailStr;

        }
        else
        {
            Debug.Log("LoginServer success");
            AuthToken = auth;
            var pkt = new Packet.GAMESEVER_REQ_GAMESERVER_ENTER();

            pkt.ID = id;
            pkt.AuthToken = auth;
            pkt.GameServerID = AppManager.GetInstance().GameServerID;

            NetworkManager.GetInstance().tcpipNetwork.SendPacket(pkt, Packet.PacketId.ID_GAMESEVER_REQ_GAMESERVER_ENTER);
        }

    }

    void OnEnable()
    {
        mainCamera.transform.position = new Vector3(0.0f, 0.0f, 0.0f);//appManager.loginCameraTransform.position;

        mainCamera.transform.rotation = appManager.loginCameraTransform.rotation;

        loginCanvas.SetActive(true);
        appManager.loginCanvasStatetext.text = appManager.notLoginedStr;

        deployShipCanvas.SetActive(false);

        playCanvas.SetActive(false);
    }

    private void OnDisable()
    {
        ID = "";
        PW = "";
        //TODO: 로그인 캔버스의 인풋필드들도 초기화 해야 한다.
    }

    void Update () {

        accumulatedTime += Time.deltaTime;
        var logingigStr = appManager.logingigStr;
        if (accumulatedTime <= 0.33f)
        {
            return;
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
        appManager.loginCanvasStatetext.text = logingigStr;
    }
}
