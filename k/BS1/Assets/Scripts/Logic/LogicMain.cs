using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class LogicMain : MonoBehaviour {

    HTTPLib httpNet;
    TcpIpLib tcpNet;
    NetworkManager NetworkManager;
    // Use this for initialization
    void Start () {
        httpNet = NetworkManager.GetInstance().httpNetwork;
        tcpNet = NetworkManager.GetInstance().tcpipNetwork;
        NetworkManager = NetworkManager.GetInstance();
    }
	
    void SubscribeEventFunc()
    {
        NetworkManager.OnGameServerEnterRes += OnGameServerEnterRes;
    }

	// Update is called once per frame
	void Update () {

        //TODO: 앱매니저와 로직 메인을 합쳐야 한다.
        //앱매니져는 각각의 앱의 상태를 관리하는 상태 오브젝트를 변경한다.
        //상태 오브젝트는 한 씬 내에서의 앱의 상태에 게임 따른 오브젝트의 배치와 초기 상태를 알고있다
        //상태 오브젝트에 대해서 상호작용하는 게임 오브젝트들이 있고 이 오브젝트들의 입력에 대한 로직은
        //각각의 상태 오브젝트가 갖고있다.
        //각각의 상태 오브젝트는 active 가끝날때 스스와 스스로와 관계된 오브젝트를 초기화 한다.

    }

    #region 로그인 버튼이 눌렸을때

    void OnLoginButtenPushed()
    {
        string id = AppManager.GetInstance().ID;
        string pw = AppManager.GetInstance().PW;

        if(id.Length == 0 || pw.Length == 0)
        {
            //AppManager.GetInstance().currentStateTrigger = AppManager.State.LOGIN_FAILED;
            return;
        }
        else
        {
            StartCoroutine(TryLogin(id,pw));
        }
    }

   public IEnumerator TryLogin(string id,string pw)
   {
        string auth = "";

        yield return httpNet.RequestHttpLoginOrCreateUser(id, pw, (L) => { auth = L; });

        if(auth == "")
        {
            Debug.Log("LoginServer Fail");
            //AppManager.GetInstance().currentStateTrigger = AppManager.State.LOGIN_FAILED;
        }
        else
        {

            var pkt = new Packet.GAMESEVER_REQ_GAMESERVER_ENTER();

            pkt.ID = id;
            pkt.AuthToken = auth;
            pkt.GameServerID = AppManager.GetInstance().GameServerID;

            NetworkManager.GetInstance().tcpipNetwork.SendPacket(pkt,Packet.PacketId.ID_GAMESEVER_REQ_GAMESERVER_ENTER);
        }

   }

   void OnGameServerEnterRes(Packet.GAMESEVER_RES_GAMESERVER_ENTER pkt)
   {

   }

#endregion


}
