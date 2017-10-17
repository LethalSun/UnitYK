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
    GameObject findingGameCanvas;
    HTTPLib httpNet;
    TcpIpLib tcpNet;
    NetworkManager networkManager;

    string ID;
    string PW;
    string AuthToken;

    float accumulatedTime = 0.0f;

    bool loginStart = false;
    public void InitObject()
    {
        //초기화 Appmanager 에서 모두 각각의 오브젝트를 초기화시킨다.
        appManager = AppManager.GetInstance();

        mainCamera = appManager.mainCamera;

        loginCanvas = appManager.loginCanvas;

        deployShipCanvas = appManager.deployShipCanvas;

        findingGameCanvas = appManager.findingGameCanvas;

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
            Debug.Log("response arrived");
            MakeLogin();
        }
        else
        {
            Debug.Log("response arrived but error");
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
        appManager.stateChaned = true;
        appManager.ID = ID;
        appManager.PW = PW;
        appManager.AuthToken = AuthToken;
        //AppManager의 현재스테이트를 DEPLOY_STATE로 바꾼다. stateChaned = true 로 한다.
        //ID, PW ,AuthKey를 AppManager에 저장한다.
        //그러면 AppManager가 관련된 오브젝트를 활성화 시킨다.

    }

    public void OnLoginButtenPushed()
    {
        if (ID.Length == 0 || PW.Length == 0)
        {
            appManager.loginCanvasStatetext.text = appManager.loginFailStr;
            return;
        }
        else
        {
            loginStart = true;
            StartCoroutine(TryLogin(ID, PW));
        }

    }

    public IEnumerator TryLogin(string id, string pw)
    {
        string auth = "";

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
        mainCamera.transform.position = appManager.loginCameraTransform.position;

        mainCamera.transform.rotation = appManager.loginCameraTransform.rotation;

        loginCanvas.SetActive(true);
        appManager.loginCanvasStatetext.text = appManager.notLoginedStr;
        loginStart = false;

        deployShipCanvas.SetActive(false);

        playCanvas.SetActive(false);

        playEndCanvas.SetActive(false);
    }

    private void OnDisable()
    {
        ID = "";
        PW = "";
        //TODO: 로그인 캔버스의 인풋필드들도 초기화 해야 한다.
    }

    void Update () {

        if(loginStart == false)
        {
            return;
        }
        accumulatedTime += Time.deltaTime;
        
        if (accumulatedTime <= 0.33f)
        {
            return;
        }
        else
        {
            accumulatedTime = 0.0f;
        }

        if (appManager.logingigStr.Length < 12)
        {
            appManager.logingigStr += ".";
        }
        else
        {
            appManager.logingigStr = appManager.logingigStr.Substring(0, 8);
        }
        appManager.loginCanvasStatetext.text = appManager.logingigStr;
    }
}
