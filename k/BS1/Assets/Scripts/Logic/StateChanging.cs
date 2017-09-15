using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StateChanging : MonoBehaviour
{
    public enum State
    {
        //초기화 해준다. 1번호출
        NOTLOGINED = 1,
        //로그인 과정에서 로그인중 ...을 갱신 
        LOGINGING = 2,
        //디플로이 형태로 카메라, ui변경
        LOGINED_DEPLOY_SHIP = 3,
        //매치찾기로 카메라 ui변경
        LOGINED_MATCH_REQ = 4,
        //게임 화면으로 카메라, ui변경 배 이동 불가능 하게 변경.
        LOGINED_GAME_START = 5,
        //내 턴이므로 폭탄 배치 버튼 활성화
        LOGINED_GAME_MY_TURN = 6,
        //내 턴이 아니므로 폭탄 배치 비활성화
        LOGINED_GAME_ENEMY_TURN = 7,
        //게임 종료 화면으로 카메라, ui변경
        LOGINED_GAME_END = 8,
        //내정보 확인 화면으로 카메라 ui 변경
        //TODO: 혹은 어딘가에 계속 띄워 놓는것도.
        LOGINED_CHECK_MY_INFO = 9,
        //게임을 초기 설정으로 변경
        LOGOUTING = 10,
        //1번만 실행되야 되는 동작들을 위한 스테이트
        TRIGGER_OFF_STATE = 11,
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

    float accumulatedTime = 0.0f;

    public GameObject deployShipCanvas;
    public GameObject gameCanvas;
    public GameObject PlayerInfoCanvas;

    public static StateChanging instance = null;

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

    public static StateChanging GetInstance()
    {
        return instance;
    }

    // Use this for initialization
    void Start()
    {
        //Debug.Log("init start");

        InitApp();
    }

    // Update is called once per frame
    void Update()
    {

        switch (currentStateTrigger)
        {
            case State.NOTLOGINED:
                OnNotLogedIn();
                break;
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
        //처음은 로그인 안됨으로 설정
        currentStateTrigger = State.NOTLOGINED;

        //카메라 배치와 캔버스
        mainCamera.transform.position = loginCameraTransform.position;
        mainCamera.transform.rotation = loginCameraTransform.rotation;
        loginCanvas.SetActive(true);
        deployShipCanvas.SetActive(false);
        gameCanvas.SetActive(false);

        OnNotLogedIn();

        currentStateTrigger = State.TRIGGER_OFF_STATE;
    }

    void OnNotLogedIn()
    {
        statetext.text = notLoginedStr;
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

    void OnDeployShip()
    {

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
