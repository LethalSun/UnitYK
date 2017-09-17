using System;

public partial class TcpIpLib
{
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
