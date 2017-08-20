using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class HTTPLib : MonoBehaviour {

    public string address;

    bool isLogin;

    public enum ERROR_CODE
    {
        NONE = 0,

        REDIS_START_SET_TEST = 21,
        REDIS_START_EXCEPTION = 22,


        PREV_REQUEST_NOT_COMPLETE = 101,
        PREV_REQUEST_FAIL_REDIS = 102,

        REQ_CREATE_USER_INVALID_ID = 111,
        REQ_CREATE_USER_DUPLICATE_USER_ID = 112,

        REQ_LOGIN_INVALID_USER = 121,
        REQ_LOGIN_PW = 122,

        REQ_LOAD_BASIC_GAME_DATA_INVALID_AUTH = 131,
        REQ_LOAD_BASIC_GAME_DATA_INVALID_ID = 132,
    }

    public IEnumerator RequestHttpLogin(string id ,string pw)
    {

        REQ_LOGIN pkt = new REQ_LOGIN();
        pkt.UserID = id;

        pkt.PW = pw;

        string reqLogin = "/Request/Login";

        var request = RequestHttp<REQ_LOGIN>(pkt, reqLogin);

        yield return request.Send();

        Debug.Log("Status Code: " + request.responseCode);

        var responseJson = JsonUtility.FromJson<RES_LOGIN>(request.downloadHandler.ToString());

    }

    public IEnumerator RequestHttpCreateUser(string id, string pw)
    {
        REQ_CREATE_USER pkt = new REQ_CREATE_USER();
        pkt.UserID = id;

        pkt.PW = pw;

        string reqCreateUser = "/Request/CreateUser";

        var request = RequestHttp<REQ_CREATE_USER>(pkt, reqCreateUser);

        yield return request.Send();

        Debug.Log("Status Code: " + request.responseCode);

        var responseJson = JsonUtility.FromJson<RES_LOGIN>(request.downloadHandler.ToString());
    }

    public IEnumerator RequestHttpLogout(string id, string tok)
    {
        REQ_LOGOUT pkt = new REQ_LOGOUT();

        pkt.UserID = id;

        pkt.AuthToken = tok;

        string reqLogout = "/Request/Logout";

        var request = RequestHttp<REQ_LOGOUT>(pkt, reqLogout);

        yield return request.Send();

        Debug.Log("Status Code: " + request.responseCode);

        var responseJson = JsonUtility.FromJson<RES_LOGIN>(request.downloadHandler.ToString());
    }

    public UnityWebRequest RequestHttp<REQUEST_T>(REQUEST_T reqPacket, string reqAPI)
    {
        var api = "http://" + address + reqAPI;
        var requestJson = JsonUtility.ToJson(reqPacket);

        var request = new UnityWebRequest(api, "POST");
        byte[] contentRaw = Encoding.UTF8.GetBytes(requestJson);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(contentRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        return request;

    }


    #region REQ_DEV_ECHO
    [SerializeField]
    public struct REQ_DEV_ECHO
    {
        public int WaitSec;
        public string ReqData;
    }

    [SerializeField]
    public struct RES_DEV_ECHO
    {
        public bool Result;
        public string ResData;
    }
    #endregion


    #region REQ_CREATE_USER
    [SerializeField]
    public struct REQ_CREATE_USER
    {
        public string UserID;
        public string PW;
    }

    [SerializeField]
    public struct RES_CREATE_USER
    {
        public RES_CREATE_USER Return(ERROR_CODE error)
        {
            Result = (short)error; return this;
        }

        public void SetResult(ERROR_CODE error) { Result = (short)error; }


        public short Result;
    }
    #endregion


    #region REQ_LOGIN
    [SerializeField]
    public struct REQ_LOGIN
    {
        public string UserID;
        public string PW;
    }

    [SerializeField]
    public struct RES_LOGIN
    {
        public RES_LOGIN Return(ERROR_CODE error)
        {
            Result = (short)error; return this;
        }

        public void SetResult(ERROR_CODE error) { Result = (short)error; }


        public short Result;
        public string AuthToken;
    }
    #endregion

    #region REQ_LOGOUT
    [SerializeField]
    public struct REQ_LOGOUT
    {
        public string UserID;
        public string AuthToken;
    }

    [SerializeField]
    public struct RES_LOGOUT
    {
        public RES_LOGOUT Return(ERROR_CODE error)
        {
            Result = (short)error; return this;
        }

        public void SetResult(ERROR_CODE error) { Result = (short)error; }
        public short Result;
    }
    #endregion

    #region REQ_LOAD_BASIC_GAME_DATA
    [SerializeField]
    public struct REQ_LOAD_BASIC_GAME_DATA
    {
        public string UserID;
        public string AuthToken;
    }

    [SerializeField]
    public struct RES_LOAD_BASIC_GAME_DATA
    {
        public RES_LOAD_BASIC_GAME_DATA Return(ERROR_CODE error)
        {
            Result = (short)error; return this;
        }

        public void SetResult(ERROR_CODE error) { Result = (short)error; }


        public short Result;
        public int Level;
        public string Money;
    }
    #endregion
}
