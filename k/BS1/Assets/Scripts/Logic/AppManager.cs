using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AppManager : MonoBehaviour
{
    public enum State
    {
        //로그인 과정에서 "로그인중 ..." 메세지를 갱신 
        LOGINGING = 1,
        //로그인이 실패하면 
        LOGIN_FAILED,
        //디플로이 형태로 카메라, ui변경
        LOGINED_DEPLOY_SHIP,
        //매치찾기로 카메라 ui변경
        LOGINED_MATCH_REQ,
        //게임 화면으로 카메라, ui변경 배 이동 불가능 하게 변경.
        LOGINED_GAME_START,
        //내 턴이므로 폭탄 배치 버튼 활성화
        LOGINED_GAME_MY_TURN,
        //내 턴이 아니므로 폭탄 배치 비활성화
        LOGINED_GAME_ENEMY_TURN,
        //게임 종료 화면으로 카메라, ui변경
        LOGINED_GAME_END,
        //내정보 확인 화면으로 카메라 ui 변경
        //TODO: 혹은 어딘가에 계속 띄워 놓는것도.
        LOGINED_CHECK_MY_INFO,
        //게임을 초기 설정으로 변경
        LOGOUTING,
        //1번만 실행되야 되는 동작들을 위한 스테이트
        TRIGGER_OFF_STATE,
    }

    public State currentStateTrigger;

    public Transform shipDepolyCameraTransform;
    public Transform loginCameraTransform;
    public Transform gamePlayCameraTranseform;

    public GameObject mainCamera;

    public GameObject loginCanvas;
    public InputField inputFieldID;
    public InputField inputFieldPW;
    public Text statetext;

    public string ID;
    public string PW;

    public string notLoginedStr;
    public string logingigStr;
    public string loginFailStr;

    float accumulatedTime = 0.0f;

    public GameObject deployShipCanvas;
    public GameObject gameCanvas;
    public GameObject PlayerInfoCanvas;

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

        DontDestroyOnLoad(this.gameObject);
    }

    public static AppManager GetInstance()
    {
        return instance;
    }

    void Start()
    {
        InitApp();
    }

    void Update()
    {

        switch (currentStateTrigger)
        {
            case State.LOGINGING:
                OnLogining();
                break;
            case State.LOGINED_DEPLOY_SHIP:
                OnDeployShip();
                break;
            case State.LOGINED_MATCH_REQ:
                OnMatchReq();
                break;
            case State.LOGINED_GAME_START:
                OnGameStart();
                break;
            case State.LOGINED_GAME_MY_TURN:
                break;
            case State.LOGINED_GAME_ENEMY_TURN:
                break;
            case State.LOGINED_GAME_END:
                break;
            case State.LOGINED_CHECK_MY_INFO:
                break;
            case State.LOGOUTING:
                break;
            default:
                break;
        }

    }

    void InitApp()
    {
        //카메라 배치와 캔버스
        mainCamera.transform.position = loginCameraTransform.position;
        mainCamera.transform.rotation = loginCameraTransform.rotation;

        loginCanvas.SetActive(true);
        statetext.text = notLoginedStr;

        deployShipCanvas.SetActive(false);
        gameCanvas.SetActive(false);

        currentStateTrigger = State.TRIGGER_OFF_STATE;
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

        statetext.text = logingigStr;
    }

    void OnLoginFailed()
    {
        statetext.text = loginFailStr;
    }

    void OnDeployShip()
    {
        //카메라 배치와 유아이 교환
        mainCamera.transform.position = shipDepolyCameraTransform.position;
        mainCamera.transform.rotation = shipDepolyCameraTransform.rotation;

        loginCanvas.SetActive(false);
        statetext.text = notLoginedStr;

        deployShipCanvas.SetActive(true);


        gameCanvas.SetActive(false);


        currentStateTrigger = State.TRIGGER_OFF_STATE;
    }

    void OnMatchReq()
    {
        currentStateTrigger = State.TRIGGER_OFF_STATE;
    }

    void OnGameStart()
    {
        currentStateTrigger = State.TRIGGER_OFF_STATE;
    }

    void OnGameMyturn()
    {
        currentStateTrigger = State.TRIGGER_OFF_STATE;
    }

    void OnGameEnemyTurn()
    {
        currentStateTrigger = State.TRIGGER_OFF_STATE;
    }

    void OnGameEnd()
    {
        currentStateTrigger = State.TRIGGER_OFF_STATE;
    }

    void OnCheckMyInfo()
    {
        currentStateTrigger = State.TRIGGER_OFF_STATE;
    }

    void OnLogout()
    {
        currentStateTrigger = State.TRIGGER_OFF_STATE;
    }
}

