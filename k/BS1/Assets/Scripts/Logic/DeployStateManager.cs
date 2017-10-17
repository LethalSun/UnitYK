using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DeployStateManager : MonoBehaviour
{
    AppManager appManager;
    GameObject mainCamera;
    GameObject loginCanvas;
    GameObject deployShipCanvas;

    Text gameStartButtonText;

    GameObject findingGameCanvas;

    Text cancleButtontext;

    GameObject playCanvas;
    GameObject playEndCanvas;

    GameObject[] myTiles;
    List<GameObject> shipDock;
    string shipDeplyInfo;
    int numberOfTile;
    int numberOfRaw;
    float accumulatedTime;

    HTTPLib httpNet;
    TcpIpLib tcpNet;
    NetworkManager networkManager;

 #region InitObject
    public void InitObject()
    {
        //초기화 Appmanager 에서 모두 각각의 오브젝트를 초기화시킨다.
        appManager = AppManager.GetInstance();

        mainCamera = appManager.mainCamera;

        loginCanvas = appManager.loginCanvas;

        deployShipCanvas = appManager.deployShipCanvas;

        gameStartButtonText = appManager.gameStartButtontext;

        findingGameCanvas = appManager.findingGameCanvas;

        cancleButtontext = appManager.cancleButtontext;

        playCanvas = appManager.playCanvas;

        playEndCanvas = appManager.playEndCanvas;

        myTiles = appManager.tileManager.GetComponent<DeployTile>().tiles;
        //Debug.Log("tilenum" + appManager.tileManager.GetComponent<DeployTile>().tiles.Length);
        shipDock = appManager.deployer.transform.Find("Harbor").gameObject.GetComponent<MakeShip>().ShipDock; ;

        networkManager = NetworkManager.GetInstance();
        httpNet = networkManager.httpNetwork;
        tcpNet = networkManager.tcpipNetwork;
        shipDeplyInfo = "";

        AddNetworkEvent();
    }

    void AddNetworkEvent()
    {
        networkManager.OnShipDeployInfoRes += OnShipDeployInfoRes;
        networkManager.OnGameReadyRes += OnGameReadyRes;
        networkManager.OnGameStartNtf += OnGameStartNtf;
    }

    void OnShipDeployInfoRes(Packet.GAMESEVER_RES_SHIP_DEPLOY_INFO pkt)
    {

        if (pkt.Result == (int)NetworkManager.TcpError.None)
        {
            Debug.Log("=================================response arrived");

            SendGameReady();
        }
        else
        {
            //TODO:실패 했을경우. AUTH실패인경우 다시 로그인을 하게 한다.
            //TODO:다른 이유일 경우
            Debug.Log("=================================fail response arrived");
        }
        
    }

    void OnGameReadyRes(Packet.GAMESEVER_RES_GAME_READY pkt)
    {
        if (pkt.Result == (int)NetworkManager.TcpError.None)
        {
            Debug.Log("=================================response arrived");
            //매치 스타트 알람을 기다린다.
        }
        else
        {
            //TODO:실패 했을경우. AUTH실패인경우 다시 로그인을 하게 한다.
            //TODO:다른 이유일 경우
            Debug.Log("=================================fail response arrived");
        }
    }

    void OnGameStartNtf(Packet.GAMESEVER_NTF_GAME_START pkt)
    {
        if (pkt.Result == (int)NetworkManager.TcpError.None)
        {
            Debug.Log("=================================response arrived Not Turn");

            MakeGameStart(false);
        }
        else if(pkt.Result == (int)NetworkManager.TcpError.MyTurn)
        {
            Debug.Log("=================================response arrived My Turn");

            MakeGameStart(true);

        }
        else
        {
            //TODO:실패 했을경우. 다시 로그인을 하게 한다.
            Debug.Log("=================================fail response arrived");
        }
    }
#endregion

    public void OnGameStartButtonPushed()
    {
        //deployer 에서 모든 배를 확인 하면서 배치가 되었는지 확인한다.
        bool isDeployDone = true;
        
        foreach(GameObject ship in shipDock)
        {
            if (ship.GetComponent<ShipProperties>().isDeployed == false)
            {
                isDeployDone = false;
                break;
            }
        }


        //모든 배가 배치가 되어있다면 
        if(isDeployDone == true)
        {
            //배배치 리퀘스트 보내기
            //타일 매니저 에서 내타일을 나타내는 1차원 배열을 순서대로 보면서 
            //배가 배치 되어있으면 1 아니면 0을 스트링에 저장하고 그 문자열을 보낸다.
            string shipInfo = GetShipDeployInfo();
            Debug.Log("==============shipInfo" + shipInfo);
            if(shipInfo == null)
            {
                gameStartButtonText.text = "Pleas Deploy All Ship and than click this button";
            }
            else
            {
                SendShipInfo(shipInfo);

                deployShipCanvas.SetActive(false);

                findingGameCanvas.SetActive(true);
            }

        }
        else
        {
            //아니면 배배치 부탁 메시지 띄우기.
            gameStartButtonText.text = "Pleas Deploy All Ship and than click this button";
        }

    }

    string GetShipDeployInfo()
    {
        string shipInfo = string.Empty;

        int numOfElem = myTiles.Length;
        Debug.Log("==============numOfElem" + numOfElem);
        for (int i = 0; i< numOfElem;++i)
        {
            if(myTiles[i].GetComponent<ShipOnTheTile>().ship == null)
            {
                shipInfo += "0";
            }
            else
            {
                shipInfo += "1";
            }
        }
        
        if(shipInfo.Length == numOfElem)
        {
            return shipInfo;
        }
        else
        {
            return null;
        }
        
    }

    public void SendShipInfo(string shipDeployInfo)
    {
        var pkt = new Packet.GAMESEVER_REQ_SHIP_DEPLOY_INFO();
        Debug.Log("SendStart");
        pkt.ShipOnTileInfo = shipDeployInfo;
        pkt.NumOfTile = (uint)shipDeployInfo.Length;
        pkt.ID = appManager.ID;
        pkt.AuthToken = appManager.AuthToken;

        NetworkManager.GetInstance().tcpipNetwork.SendPacket(pkt, Packet.PacketId.ID_GAMESEVER_REQ_SHIP_DEPLOY_INFO);

        Debug.Log("SendEnd");
    }

    public void SendGameReady()
    {
        Debug.Log("Send Ready");
        var pkt = new Packet.GAMESEVER_REQ_GAME_READY();

        pkt.ID = appManager.ID;
        pkt.AuthToken = appManager.AuthToken;
        NetworkManager.GetInstance().tcpipNetwork.SendPacket(pkt, Packet.PacketId.ID_GAMESEVER_REQ_GAME_READY);
    }

    void MakeGameStart(bool isTurn)
    {
        appManager.currentState = AppManager.State.PLAY_STATE;
        appManager.stateChaned = true;
        appManager.isMyTurn = isTurn;
    }

    void OnEnable()
    {
        mainCamera.transform.position = appManager.shipDepolyCameraTransform.localPosition;//new Vector3(0, 101, -60);//appManager.shipDepolyCameraTransform.position;
        //Debug.Log(appManager.shipDepolyCameraTransform.position);//하이라키 에서 차일드의 좌표도 그냥 얻어오면 월드 좌표가 얻어진다.

        mainCamera.transform.rotation = appManager.shipDepolyCameraTransform.localRotation;
       // Debug.Log(appManager.shipDepolyCameraTransform.rotation);

        loginCanvas.SetActive(false);

        deployShipCanvas.SetActive(true);
        gameStartButtonText.text = "Game Start";

        findingGameCanvas.SetActive(false);

        accumulatedTime = 0.0f;

        playCanvas.SetActive(false);

        playEndCanvas.SetActive(false);
    }

    private void OnDisable()
    {
        //디스에이블 될때는 string shipDeplyInfo 를 초기화 한다.
        gameStartButtonText.text = "Game Start";
        accumulatedTime = 0.0f;
        cancleButtontext.text = appManager.findingGameStr.Substring(0, 11);
    }

    // Update is called once per frame
    void Update()
    {
        //배의 배치를 정한다.
        //배배치 캔버스가 액티브로 셋되면서 배배치가 가능하게 된다.
        //게임 스타트 버튼을 누르면 우선 배배치 완료 패킷을 보내고
        //서버에서 답변이 오면 
        //다시 게임 레디 패킷을 보내고
        //게임 레디 완료 패킷을 받고 게임 스타트 노티파이를 기다린다.
        //매치가 성립에 되면 노티파이가 오고
        //그러면 앱매니져의 스테이트를 바꿔서 게임 캔버스와 카메라 변환을 해서 게임을 할수 있게 한다.

        if(findingGameCanvas.activeInHierarchy == true)
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

            if (appManager.findingGameStr.Length < 15)
            {
                appManager.findingGameStr += ".";
            }
            else
            {
                appManager.findingGameStr = appManager.findingGameStr.Substring(0, 12);
            }
            appManager.cancleButtontext.text = appManager.findingGameStr;
        }

    }
}
