using System.Collections;
using System.Collections.Generic;
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



}
