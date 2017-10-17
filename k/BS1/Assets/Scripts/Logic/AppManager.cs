using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AppManager : MonoBehaviour
{
    public enum State
    {   
        LOGIN_STATE = 0,
        DEPLOY_STATE,
        PLAY_STATE,
        PLAY_END_STATE,
        size
    }

    public bool stateChaned = true;

    public State currentState;

    public GameObject[] stateObject;

    GameObject currentObject;

    HTTPLib httpNet;
    TcpIpLib tcpNet;
    NetworkManager netWorkManager;

    public GameObject mainCamera;

    public Transform shipDepolyCameraTransform;
    public Transform loginCameraTransform;
    public Transform gamePlayCameraTranseform;

    #region for login state object
    public GameObject loginCanvas;

    public InputField inputFieldID;
    public InputField inputFieldPW;
    public Text loginCanvasStatetext;

    public string ID;
    public string PW;
    public string AuthToken;

    public string notLoginedStr;
    public string logingigStr;
    public string loginFailStr;

    public int GameServerID;

    float accumulatedTime = 0.0f;
    #endregion

    #region for deploy ship state object

    public GameObject deployShipCanvas;

    public GameObject tileManager;

    public GameObject deployer;

    public Text gameStartButtontext;

    public GameObject findingGameCanvas;

    public string findingGameStr;

    public Text cancleButtontext;
    #endregion

    #region for play state object
    public GameObject playCanvas;
    #endregion

    #region for play end state object
    public GameObject playEndCanvas;
    #endregion

    public bool isMyTurn;

    public string gameEndMessage;

    public Text PlayEndText;

    public static AppManager instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public static AppManager GetInstance()
    {
        return instance;
    }

    void Start()
    {
        //TODO: 각각의 오브젝트 초기화. 싱글톤으로 구현된 클래스나, 앱매니져가 초기화 된후. 초기화 해야 하므로 start 에서 초기화.
        stateObject[(int)State.LOGIN_STATE].GetComponent<LoginStateManager>().InitObject();
        stateObject[(int)State.DEPLOY_STATE].GetComponent<DeployStateManager>().InitObject();
        stateObject[(int)State.PLAY_STATE].GetComponent<PlayStateManager>().InitObject();
        stateObject[(int)State.PLAY_END_STATE].GetComponent<PlayEndStateManager>().InitObject();
        currentState = State.LOGIN_STATE;
    }

    void Update()
    {
        if(stateChaned)
        {
            ChangeObject(currentState);
            stateChaned = false;
        }
        //TODO: 로그인이 되면 일정 기간에 하트 비트를 보낸다.

    }

    void ChangeObject(State curState)
    {
        int size = (int)State.size;
        for (int state = 0; state < size; ++state)
        {
            if(state == (int)curState)
            {
                stateObject[state].SetActive(true);
            }
            else
            {
                stateObject[state].SetActive(false);
            }
        }

    }

    void InitApp()
    {
        //카메라 배치와 캔버스
        mainCamera.transform.position = loginCameraTransform.position;
        mainCamera.transform.rotation = loginCameraTransform.rotation;

        loginCanvas.SetActive(true);
        loginCanvasStatetext.text = notLoginedStr;

        deployShipCanvas.SetActive(false);
        playCanvas.SetActive(false);

        netWorkManager.OnGameServerInfoRes += OnGameServerInfo;
        netWorkManager.OnNewUserNtf += OnNewUserNtf;
        netWorkManager.OnLogoutRes += OnLogoutRes;
        netWorkManager.OnLogoutNtf += OnLogoutNtf;
        netWorkManager.OnHeartBeatRes += OnHeartBeatRes;
    }

    void OnGameServerInfo(Packet.GAMESEVER_RES_GAMESERVER_INFO pkt)
    {

    }

    void OnNewUserNtf(Packet.GAMESEVER_NTF_NEW_USER pkt)
    {

    }

    void OnLogoutNtf(Packet.GAMSERVER_NTF_USER_LOGOUT pkt)
    {

    }

    void OnLogoutRes(Packet.GAMSERVER_RES_USER_LOGOUT pkt)
    {

    }

    void OnHeartBeatRes(Packet.GAMSERVER_RES_USER_HEARTBEAT pkt)
    {

    }
}

