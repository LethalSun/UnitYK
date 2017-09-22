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
        ////로그인 과정에서 "로그인중 ..." 메세지를 갱신 
        //LOGINGING = 1,
        ////로그인이 실패하면 
        //LOGIN_FAILED,
        ////디플로이 형태로 카메라, ui변경
        //LOGINED_DEPLOY_SHIP,
        ////매치찾기로 카메라 ui변경
        //LOGINED_MATCH_REQ,
        ////게임 화면으로 카메라, ui변경 배 이동 불가능 하게 변경.
        //LOGINED_GAME_START,
        ////내 턴이므로 폭탄 배치 버튼 활성화
        //LOGINED_GAME_MY_TURN,
        ////내 턴이 아니므로 폭탄 배치 비활성화
        //LOGINED_GAME_ENEMY_TURN,
        ////게임 종료 화면으로 카메라, ui변경
        //LOGINED_GAME_END,
        ////내정보 확인 화면으로 카메라 ui 변경
        ////TODO: 혹은 어딘가에 계속 띄워 놓는것도.
        //LOGINED_CHECK_MY_INFO,
        ////게임을 초기 설정으로 변경
        //LOGOUTING,
        ////1번만 실행되야 되는 동작들을 위한 스테이트
        //TRIGGER_OFF_STATE,
    }

    public bool stateChaned = true;

    public State currentState;

    public GameObject[] stateObject;

    GameObject currentObject;

    HTTPLib httpNet;
    TcpIpLib tcpNet;
    NetworkManager NetworkManager;

    public GameObject mainCamera;

    public Transform shipDepolyCameraTransform;
    public Transform loginCameraTransform;
    public Transform gamePlayCameraTranseform;

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

    public GameObject deployShipCanvas;
    public GameObject playCanvas;
    public GameObject playEndCanvas;

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
        currentState = State.LOGIN_STATE;
    }

    void Update()
    {
        if(stateChaned)
        {
            ChaneObject(currentState);
            stateChaned = false;
        }


        //switch (currentState)
        //{
        //
        //        // case State.LOGINGING:
        //        //     OnLogining();
        //        //     break;
        //        // case State.LOGINED_DEPLOY_SHIP:
        //        //     OnDeployShip();
        //        //     break;
        //        // case State.LOGINED_MATCH_REQ:
        //        //     OnMatchReq();
        //        //     break;
        //        // case State.LOGINED_GAME_START:
        //        //     OnGameStart();
        //        //     break;
        //        // case State.LOGINED_GAME_MY_TURN:
        //        //     break;
        //        // case State.LOGINED_GAME_ENEMY_TURN:
        //        //     break;
        //        // case State.LOGINED_GAME_END:
        //        //     break;
        //        // case State.LOGINED_CHECK_MY_INFO:
        //        //     break;
        //        // case State.LOGOUTING:
        //        //     break;
        //        // default:
        //        //     break;
        //}

    }

    void ChaneObject(State curState)
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

        //currentStateTrigger = State.TRIGGER_OFF_STATE;
    }

    void OnLogining()
    {
        accumulatedTime += Time.deltaTime;

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

        loginCanvasStatetext.text = logingigStr;
    }

    void OnLoginFailed()
    {
        loginCanvasStatetext.text = loginFailStr;
    }

    void OnDeployShip()
    {
        //카메라 배치와 유아이 교환
        mainCamera.transform.position = shipDepolyCameraTransform.position;
        mainCamera.transform.rotation = shipDepolyCameraTransform.rotation;

        loginCanvas.SetActive(false);
        loginCanvasStatetext.text = notLoginedStr;

        deployShipCanvas.SetActive(true);


        playCanvas.SetActive(false);


        //currentStateTrigger = State.TRIGGER_OFF_STATE;
    }

    void OnMatchReq()
    {
       // currentStateTrigger = State.TRIGGER_OFF_STATE;
    }

    void OnGameStart()
    {
        //currentStateTrigger = State.TRIGGER_OFF_STATE;
    }

    void OnGameMyturn()
    {
        //currentStateTrigger = State.TRIGGER_OFF_STATE;
    }

    void OnGameEnemyTurn()
    {
        //currentStateTrigger = State.TRIGGER_OFF_STATE;
    }

    void OnGameEnd()
    {
        //currentStateTrigger = State.TRIGGER_OFF_STATE;
    }

    void OnCheckMyInfo()
    {
        //currentStateTrigger = State.TRIGGER_OFF_STATE;
    }

    void OnLogout()
    {
        //currentStateTrigger = State.TRIGGER_OFF_STATE;
    }
}

