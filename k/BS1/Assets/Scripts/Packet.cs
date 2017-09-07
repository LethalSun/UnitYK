using System.Runtime.InteropServices;

namespace Packet
{
    [StructLayout(LayoutKind.Sequential,Pack = 1)]
    public struct PacketHeader
    {
        public int PacketID;
        public int BodySize;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct PacketRaw
    {
        public int PecketID;
        public int BodySize;
        public string Data;
    }
    // C++ IOCP������ Unity C#�� ����ϱ� ���� ��Ŷ ����
    //���Ӽ��� ���ӿ�û
    public class GAMESEVER_REQ_GAMESERVER_ENTER
{
	public string				ID;
	public string				AuthToken;
	public int					GameServerID;
	}

	//���Ӽ��� ���Ӵ亯
	public class GAMESEVER_RES_GAMESERVER_ENTER
{
	public int					Result;
	}

	//���Ӽ��� ���� ����
	public class GAMESEVER_NTF_NEW_USER
{
	public string				NewUserID;
	public string				NewUserIndex;
	}

	//���Ӽ��� ���� ��û
	public class GAMESEVER_REQ_GAMESERVER_INFO
{
	public string				ID;
	public string				AuthToken;
	}

	//���Ӽ��� ���� �亯
	public class GAMESEVER_RES_GAMESERVER_INFO
{
	public int					Result;
	public int					ServerUserNum;
	public int					ServerMatchNum;
	}

	//���Ӽ��� �� ��ġ ���� �Է� ��û
	public class GAMESEVER_REQ_SHIP_DEPLOY_INFO
{
	public string				ID;
	public string				AuthToken;
	public uint				NumOfTile;
	public int[]				ShipOnTileInfo;
	}

	//���Ӽ��� �� ��ġ ���� �Է� �亯
	public class GAMESEVER_RES_SHIP_DEPLOY_INFO
{
	public int					Result;
	}

	//���Ӽ��� ���ӽ��� ��û
	public class GAMESEVER_REQ_GAME_READY
{
	public string				ID;
	public string				AuthToken;
	}

	//���Ӽ��� ���ӽ��� �亯
	public class GAMESEVER_RES_GAME_READY
{
	public int					Result;
	}

	//���Ӽ��� ���ӽ��� ����ڵ鿡�� ���� ������ ������ ����Ʈ�� �˼� �ִ�.
	public class GAMESEVER_NTF_GAME_START
{
	public int					Result;
	}

	//���Ӽ��� ��ź ��ġ ��û
	public class GAMESEVER_REQ_BOMB
{
	public string				ID;
	public string				AuthToken;
	public int					BombedTile;
	}

	//���Ӽ��� ��ź ��ġ �亯
	public class GAMESEVER_RES_BOMB
{
	public int					Result;
	public int					BombedTile;
	}

	//���Ӽ��� ��ź ��ġ ���� Result�� �¾Ҵ��� �ƴ��� �׸��� ���� �˼� �ִ�.
	public class GAMESEVER_NTF_BOMB
{
	public int					Result;
	public int					BombedTile;
	}

	//���Ӽ��� ���� ����
	public class GAMESEVER_NTF_GAMEND
{
	public int					Result;
	}

	//���Ӽ��� ���� ��Ʈ��Ʈ ��û
	public class GAMSERVER_REQ_USER_HEARTBEAT
{
	public string				ID;
	public string				AuthToken;
	}

	//���Ӽ��� ���� ��Ʈ��Ʈ �亯
	public class GAMSERVER_RES_USER_HEARTBEAT
{
	public int					Result;
	}

	//���Ӽ��� �α׾ƿ� ��û
	public class GAMSERVER_REQ_USER_LOGOUT
{
	public string				ID;
	public string				AuthToken;
	}

	//���Ӽ��� �α׾ƿ� �亯 �亯�� ���� ���´�.
	public class GAMSERVER_RES_USER_LOGIN
{
	public int					Result;
	}

	//���Ӽ��� �α׾ƿ� ����
	public class GAMSERVER_NTF_USER_LOGOUT
{
	public int					Result;
	}

	public enum PacketId
	{
		ID_GAMESEVER_REQ_GAMESERVER_ENTER			= 101,
		ID_GAMESEVER_RES_GAMESERVER_ENTER			= 102,
		ID_GAMESEVER_NTF_NEW_USER			= 103,
		ID_GAMESEVER_REQ_GAMESERVER_INFO			= 104,
		ID_GAMESEVER_RES_GAMESERVER_INFO			= 105,
		ID_GAMESEVER_REQ_SHIP_DEPLOY_INFO			= 106,
		ID_GAMESEVER_RES_SHIP_DEPLOY_INFO			= 107,
		ID_GAMESEVER_REQ_GAME_READY			= 108,
		ID_GAMESEVER_RES_GAME_READY			= 109,
		ID_GAMESEVER_NTF_GAME_START			= 110,
		ID_GAMESEVER_REQ_BOMB			= 111,
		ID_GAMESEVER_RES_BOMB			= 112,
		ID_GAMESEVER_NTF_BOMB			= 113,
		ID_GAMESEVER_NTF_GAMEND			= 114,
		ID_GAMSERVER_REQ_USER_HEARTBEAT			= 115,
		ID_GAMSERVER_RES_USER_HEARTBEAT			= 116,
		ID_GAMSERVER_REQ_USER_LOGOUT			= 117,
		ID_GAMSERVER_RES_USER_LOGIN			= 118,
		ID_GAMSERVER_NTF_USER_LOGOUT			= 119,
	};
}