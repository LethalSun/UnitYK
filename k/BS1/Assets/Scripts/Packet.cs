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
    // C++ IOCP서버와 Unity C#이 통신하기 위한 패킷 정의
    //게임서버 접속요청
    public class GAMESEVER_REQ_GAMESERVER_ENTER
{
	public string				ID;
	public string				AuthToken;
	public int					GameServerID;
	}

	//게임서버 접속답변
	public class GAMESEVER_RES_GAMESERVER_ENTER
{
	public int					Result;
	}

	//게임서버 접속 통지
	public class GAMESEVER_NTF_NEW_USER
{
	public string				NewUserID;
	public string				NewUserIndex;
	}

	//게임서버 정보 요청
	public class GAMESEVER_REQ_GAMESERVER_INFO
{
	public string				ID;
	public string				AuthToken;
	}

	//게임서버 정보 답변
	public class GAMESEVER_RES_GAMESERVER_INFO
{
	public int					Result;
	public int					ServerUserNum;
	public int					ServerMatchNum;
	}

	//게임서버 배 배치 정보 입력 요청
	public class GAMESEVER_REQ_SHIP_DEPLOY_INFO
{
	public string				ID;
	public string				AuthToken;
	public uint				NumOfTile;
	public int[]				ShipOnTileInfo;
	}

	//게임서버 배 배치 정보 입력 답변
	public class GAMESEVER_RES_SHIP_DEPLOY_INFO
{
	public int					Result;
	}

	//게임서버 게임시작 요청
	public class GAMESEVER_REQ_GAME_READY
{
	public string				ID;
	public string				AuthToken;
	}

	//게임서버 게임시작 답변
	public class GAMESEVER_RES_GAME_READY
{
	public int					Result;
	}

	//게임서버 게임시작 당사자들에게 통지 누구의 턴인지 리절트로 알수 있다.
	public class GAMESEVER_NTF_GAME_START
{
	public int					Result;
	}

	//게임서버 폭탄 배치 요청
	public class GAMESEVER_REQ_BOMB
{
	public string				ID;
	public string				AuthToken;
	public int					BombedTile;
	}

	//게임서버 폭탄 배치 답변
	public class GAMESEVER_RES_BOMB
{
	public int					Result;
	public int					BombedTile;
	}

	//게임서버 폭탄 배치 통지 Result로 맞았는지 아닌지 그리고 턴을 알수 있다.
	public class GAMESEVER_NTF_BOMB
{
	public int					Result;
	public int					BombedTile;
	}

	//게임서버 종료 통지
	public class GAMESEVER_NTF_GAMEND
{
	public int					Result;
	}

	//게임서버 유저 하트비트 요청
	public class GAMSERVER_REQ_USER_HEARTBEAT
{
	public string				ID;
	public string				AuthToken;
	}

	//게임서버 유저 하트비트 답변
	public class GAMSERVER_RES_USER_HEARTBEAT
{
	public int					Result;
	}

	//게임서버 로그아웃 요청
	public class GAMSERVER_REQ_USER_LOGOUT
{
	public string				ID;
	public string				AuthToken;
	}

	//게임서버 로그아웃 답변 답변이 오면 끊는다.
	public class GAMSERVER_RES_USER_LOGIN
{
	public int					Result;
	}

	//게임서버 로그아웃 통지
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