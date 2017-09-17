using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicMain : MonoBehaviour {

    HTTPLib httpNet;
    TcpIpLib tcpNet;
    // Use this for initialization
    void Start () {
        httpNet = NetworkManager.GetInstance().httpNetwork;
        tcpNet = NetworkManager.GetInstance().tcpipNetwork;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

#region 로그인 버튼이 눌렸을때

    void OnLoginButtenPushed()
    {
        string id = AppManager.GetInstance().ID;
        string pw = AppManager.GetInstance().PW;

        if(id.Length == 0 || pw.Length == 0)
        {
            AppManager.GetInstance().currentStateTrigger = AppManager.State.LOGIN_FAILED;
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
            AppManager.GetInstance().currentStateTrigger = AppManager.State.LOGIN_FAILED;
        }
        else
        {

            var pkt = new Packet.GAMESEVER_REQ_GAMESERVER_ENTER();

            pkt.ID = id;
            pkt.AuthToken = auth;
            pkt.GameServerID = AppManager.GetInstance().GameServerID;

            NetworkManager.GetInstance().tcpipNetwork.SendPacket(pkt,Packet.PacketId.ID_GAMESEVER_REQ_GAMESERVER_ENTER);
        }

        //Tcp에서 게임 서버 입장 요청

   }

#endregion


}
