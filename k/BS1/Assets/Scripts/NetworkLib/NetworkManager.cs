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


}
