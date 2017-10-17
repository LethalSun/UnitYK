using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayStateManager : MonoBehaviour
{
    AppManager appManager;
    GameObject mainCamera;
    GameObject loginCanvas;
    GameObject deployShipCanvas;
    GameObject findingGameCanvas;
    GameObject playCanvas;
    GameObject playEndCanvas;

    GameObject[] myTiles;
    GameObject[] enemyTiles;

    List<GameObject> shipDock;
    List<GameObject> bombStorage;
    List<GameObject> hitTile;
    List<GameObject> normalTile;

    int numberOfTile;
    int numberOfRaw;
    float accumulatedTime;

    HTTPLib httpNet;
    TcpIpLib tcpNet;
    NetworkManager networkManager;

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

        myTiles = appManager.tileManager.GetComponent<DeployTile>().tiles;

        bombStorage = appManager.deployer.GetComponent<DeployBomb>().Armory.GetComponent<MakeBomb>().bombStorage;

        shipDock = appManager.deployer.transform.Find("Harbor").gameObject.GetComponent<MakeShip>().ShipDock; ;

        hitTile = appManager.deployer.GetComponent<MakeTile>().tileStorage;

        normalTile =appManager.deployer.GetComponent<MakeTile>().tileStorageNormal;

        networkManager = NetworkManager.GetInstance();
        httpNet = networkManager.httpNetwork;
        tcpNet = networkManager.tcpipNetwork;

        AddNetworkEvent();
    }

    void AddNetworkEvent()
    {
        networkManager.OnBomoRes += OnBombRes;
        networkManager.OnBombNtf += OnBombNtf;
        networkManager.OnGameEndNtf += OnGameEndNtf;
    }

    void OnBombRes(Packet.GAMESEVER_RES_BOMB pkt)
    {
        //답변이 오면 맞았는지 여부를 확인하고 맞았으면 빨간색 아니면 초록색으로 폭탄을 만든다.
        if (pkt.Result != (int)NetworkManager.TcpError.Hit)
        {
            //TODO: 오류 가 발행했다면?
            return;
        }

        for (int i = 0; i < bombStorage.Count; ++i)
        {
            if (bombStorage[i].GetComponent<BombProperty>().tileIndex == pkt.BombedTile)
            {
                var randerer = bombStorage[i].GetComponent<ChangeBombColor>().rs;
                var mat = bombStorage[i].GetComponent<ChangeBombColor>().mats;

                for (int j = 0; j < randerer.Length; ++j)
                {
                    randerer[j].material = mat[0];
                }
            }
        }


    }

    void OnBombNtf(Packet.GAMESEVER_NTF_BOMB pkt)
    {
        //배가 맞았으면 배에 어떤 이펙트를 아니면 빈타일에 이펙트를 만든다.
        if (pkt.Result == (int)NetworkManager.TcpError.Hit)
        {
            hitTile[pkt.BombedTile].transform.position = myTiles[pkt.BombedTile].transform.position;
        }
        else
        {
            normalTile[pkt.BombedTile].transform.position = myTiles[pkt.BombedTile].transform.position;
        }

    }

    void OnGameEndNtf(Packet.GAMESEVER_NTF_GAMEND pkt)
    {
        //게임이 끝나면 결과값으로 부터 승패를 파악한후 게임 엔드 캔버스를 띄운다.
        if(pkt.Result == (int)NetworkManager.TcpError.Win)
        {
            appManager.gameEndMessage = "Win";
        }
        else if(pkt.Result == (int)NetworkManager.TcpError.None)
        {
            appManager.gameEndMessage = "Lose";
        }


    }

    public void MakeEnd()
    {
        appManager.currentState = AppManager.State.PLAY_END_STATE;
        appManager.stateChaned = true;
    }

    private void OnEnable()
    {
        mainCamera.transform.position = appManager.gamePlayCameraTranseform.localPosition;
       
        mainCamera.transform.rotation = appManager.gamePlayCameraTranseform.localRotation;

        loginCanvas.SetActive(false);

        deployShipCanvas.SetActive(false);

        findingGameCanvas.SetActive(false);

        accumulatedTime = 0.0f;

        playCanvas.SetActive(true);

        playEndCanvas.SetActive(false);
    }

    //void Update ()
    //{
	//	
	//}
}
