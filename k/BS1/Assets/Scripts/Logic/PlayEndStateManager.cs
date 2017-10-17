using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayEndStateManager : MonoBehaviour {


    AppManager appManager;
    GameObject mainCamera;
    GameObject loginCanvas;
    GameObject deployShipCanvas;
    GameObject findingGameCanvas;
    GameObject playCanvas;
    GameObject playEndCanvas;

    GameObject deployer;
    GameObject harbor;
    GameObject armory;
    GameObject tileManager;
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

        deployer = appManager.deployer; ;
        harbor = appManager.deployer.transform.Find("Harbor").gameObject;
        armory = appManager.deployer.transform.Find("Armory").gameObject;
        tileManager = appManager.tileManager;
        networkManager = NetworkManager.GetInstance();
        httpNet = networkManager.httpNetwork;
        tcpNet = networkManager.tcpipNetwork;

    }

    public void MakeDeployState()
    {
        appManager.currentState = AppManager.State.DEPLOY_STATE;
        appManager.stateChaned = true;
        deployer.GetComponent<MakeTile>().ResetObject();
        harbor.GetComponent<MakeShip>().ResetObject();
        armory.GetComponent<MakeBomb>().ResetObject();
        tileManager.GetComponent<DeployTile>().ResetObject();
    }

    private void OnEnable()
    {
        mainCamera.transform.position = appManager.gamePlayCameraTranseform.localPosition;

        mainCamera.transform.rotation = appManager.gamePlayCameraTranseform.localRotation;

        loginCanvas.SetActive(false);

        deployShipCanvas.SetActive(false);

        findingGameCanvas.SetActive(false);

        accumulatedTime = 0.0f;

        playCanvas.SetActive(false);

        playEndCanvas.SetActive(true);

        appManager.PlayEndText.text = appManager.gameEndMessage;
    }
}
