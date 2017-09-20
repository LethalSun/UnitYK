using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class NetworkManager : MonoBehaviour
{
    public HTTPLib httpNetwork;
    public TcpIpLib tcpipNetwork;

    public static NetworkManager instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            tcpipNetwork = new TcpIpLib();
            //커넥트는 나중에 다른곳에서.
            tcpipNetwork.Connect();
            instance = this;
        }
        else if (instance != this)
        {
           Destroy(gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    public static NetworkManager GetInstance()
    {
        return instance;
    }

    private void OnApplicationQuit()
    {
        tcpipNetwork.CloseConnection();
    }


    private void Update()
    {
        if(!tcpipNetwork.IsRecvPacketQueueEmpty())
        {
            var recvPacket = tcpipNetwork.GetPacket();

            InvokeRecvPacketEvent(recvPacket);
        }
    }

    private void InvokeRecvPacketEvent(Packet.PacketRaw pkt)
    {
        switch((Packet.PacketId) pkt.PecketID)
        {
            case Packet.PacketId.ID_GAMESEVER_RES_GAMESERVER_ENTER:
                {
                    this.OnGameServerEnterRes.Invoke(JsonUtility.FromJson<Packet.GAMESEVER_RES_GAMESERVER_ENTER>(pkt.Data));

                    break;
                }
            case Packet.PacketId.ID_GAMESEVER_NTF_NEW_USER:
                {
                    break;
                }
            case Packet.PacketId.ID_GAMESEVER_RES_GAMESERVER_INFO:
                {
                    break;
                }
            case Packet.PacketId.ID_GAMESEVER_RES_SHIP_DEPLOY_INFO:
                {
                    break;
                }
            case Packet.PacketId.ID_GAMESEVER_RES_GAME_READY:
                {
                    break;
                }
            case Packet.PacketId.ID_GAMESEVER_NTF_GAME_START:
                {
                    break;
                }
            case Packet.PacketId.ID_GAMESEVER_RES_BOMB:
                {
                    break;
                }
            case Packet.PacketId.ID_GAMESEVER_NTF_BOMB:
                {
                    break;
                }
            case Packet.PacketId.ID_GAMESEVER_NTF_GAMEND:
                {
                    break;
                }
            case Packet.PacketId.ID_GAMSERVER_RES_USER_HEARTBEAT:
                {
                    break;
                }
            case Packet.PacketId.ID_GAMSERVER_RES_USER_LOGOUT:
                {
                    break;
                }
            case Packet.PacketId.ID_GAMSERVER_NTF_USER_LOGOUT:
                {
                    break;
                }

        }
    }

    public event Action<Packet.GAMESEVER_RES_GAMESERVER_ENTER> OnGameServerEnterRes = delegate { };
    public event Action<Packet.GAMESEVER_NTF_NEW_USER> OnNewUserNtf = delegate { };
    public event Action<Packet.GAMESEVER_RES_GAMESERVER_INFO> OnGameServerInfoRes = delegate { };
    public event Action<Packet.GAMESEVER_RES_SHIP_DEPLOY_INFO> OnShipDeployInfoRes = delegate { };
    public event Action<Packet.GAMESEVER_RES_GAME_READY> OnGameReadyRes = delegate { };
    public event Action<Packet.GAMESEVER_NTF_GAME_START> OnGameStartNtf = delegate { };
    public event Action<Packet.GAMESEVER_RES_BOMB> OnBomoRes = delegate { };
    public event Action<Packet.GAMESEVER_NTF_BOMB> OnBombNtf = delegate { };
    public event Action<Packet.GAMESEVER_NTF_GAMEND> OnGameEndNtf = delegate { };
    public event Action<Packet.GAMSERVER_RES_USER_HEARTBEAT> OnHeartBeatRes = delegate { };
    public event Action<Packet.GAMSERVER_RES_USER_LOGOUT> OnLogoutRes = delegate { };
    public event Action<Packet.GAMSERVER_NTF_USER_LOGOUT> OnLogoutNtf = delegate { };


}
