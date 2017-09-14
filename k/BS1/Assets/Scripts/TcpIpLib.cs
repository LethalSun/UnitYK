using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
using Packet;

public partial class TcpIpLib
{
    int bufferSize = 131072;
    int headerSize = sizeof(int)*2;
    System.Text.Encoding NetworkEncoding = System.Text.Encoding.ASCII;
    bool isConnected = false;

    Socket socket;
    AsyncCallback recvCallback;
    AsyncCallback sendCallback;
    Queue<PacketRaw> packetQueue;

    private string ipAddress = "127.0.0.1";
    private int portNum = 23452;

    public TcpIpLib(string ipAddr,int port)
    {
        ipAddress = ipAddr;
        portNum = port;

        recvCallback = new AsyncCallback(RecvCallback);
        sendCallback = new AsyncCallback(SendCallback);

        initSocket();
    }

    void OnApplicationQuit()
    {
        CloseConnection();
    }

    void initSocket()
    {
        try
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }
        catch(Exception ex)
        {
            Debug.LogError("socket init fail" + ex.Message);
        }
    }

    public void CloseConnection()
    {
        if (socket.Connected == true)
        {
            socket.Close();
            packetQueue.Clear();
            isConnected = false;
        }
    }

    public bool IsConnected()
    {
        return isConnected;
    }

    public void Connect()
    {
        try
        {
            socket.BeginConnect(ipAddress, portNum, ConnectCallback, 0);
            Debug.Log("Try to connect Server IP: " + ipAddress + " Port Number: " + portNum);
        }
        catch (SocketException ex)
        {
            isConnected = false;
            Debug.LogAssertion(ex.ToString());
        }
    }

    void ConnectCallback(IAsyncResult asyncResult)
    {
        try
        {
            socket.EndConnect(asyncResult);
            isConnected = true;
            Debug.Log("Connected to Server IP: " + ipAddress + " Port Number: " + portNum);
        }
        catch(Exception ex)
        {
            Debug.Log("Connection failed" + ex.Message);
            isConnected = false;
            return;
        }

        AsyncRecvStateObject asyncRecvData = new AsyncRecvStateObject(bufferSize, socket);

        socket.BeginReceive(
            asyncRecvData.buffer,
            0,
            asyncRecvData.buffer.Length,
            SocketFlags.None,
            recvCallback,
            asyncRecvData);
    }

    public void SendPacket<T>(T data, PacketId pktID)
    {
        if (isConnected == false)
        {
            Debug.LogAssertion("Not Connected! Send Packet Failed");
            return;
        }

        AsyncSendStateObject asyncSendData = new AsyncSendStateObject(socket);

        unsafe
        {
            #region make empty packet
            string dataJson = JsonUtility.ToJson(data);

            int packetID = (int)pktID;
            int bodysize = dataJson.Length;

            asyncSendData.buffer = new byte[headerSize+ bodysize];
            asyncSendData.sendSize = headerSize + bodysize;
            #endregion

            #region pack data

            byte* packetIdByte = (byte*)&packetID;

            for(int i = 0; i<sizeof(int);++i)
            {
                asyncSendData.buffer[i] = packetIdByte[i];
            }

            byte* bodySizeByte = (byte*)&bodysize;

            for(int i = 0; i<sizeof(int);++i)
            {
                asyncSendData.buffer[i + sizeof(int)] = bodySizeByte[i];
            }

            char[] bodyChar = dataJson.ToCharArray();

            for(int i =0; i<bodysize;++i)
            {
                asyncSendData.buffer[i + bodysize] = (byte)bodyChar[i];
            }

            #endregion
        }

        try
        {
            socket.BeginSend(
                asyncSendData.buffer,
                0,
                asyncSendData.buffer.Length,
                SocketFlags.None,
                sendCallback,
                asyncSendData);

        }
        catch(SocketException ex)
        {
            ProcessException(ex);
        }
    }

    void RecvCallback(IAsyncResult asyncResult)
    {
        if(isConnected == false)
        {
            Debug.LogAssertion("Not Connected! Recv Packet Failed");
            return;
        }

        AsyncRecvStateObject asyncRecvData = (AsyncRecvStateObject)asyncResult.AsyncState;

        try
        {
            asyncRecvData.recvSize += asyncRecvData.socket.EndReceive(asyncResult);
            asyncRecvData.readPosition = 0;
        }
        catch(SocketException ex)
        {
            ProcessException(ex);
            return;
        }

        #region parse raw packet
        while (true)
        {
            //헤더 조차 못받아 왔다면 더 기다린다.
            if (asyncRecvData.recvSize < headerSize)
            {
                break;
            }


            PacketHeader packetHeader = new PacketHeader();

            var id = BitConverter.ToInt32(asyncRecvData.buffer, 0);
            var bodySize = BitConverter.ToInt32(asyncRecvData.buffer, 4);

            var packetSize = headerSize + bodySize;
            if (asyncRecvData.recvSize < headerSize + bodySize)
            {
                break;
            }
       
            var bodyJson = NetworkEncoding.GetString(asyncRecvData.buffer, 8, bodySize);

            PacketRaw packetRaw = new PacketRaw
            {
                PecketID = id,
                BodySize = bodySize,
                Data = bodyJson
            };

            Debug.Log("Receive Packet(id = " + id + " ,bodySize = " + bodySize);

            lock (this)
            {
                packetQueue.Enqueue(packetRaw);
            }

            asyncRecvData.readPosition += packetSize;
            asyncRecvData.recvSize -= packetSize;
        }
        

        #endregion

        #region shift remain data to start

        for(int i = 0; i< asyncRecvData.recvSize;++i)
        {
            asyncRecvData.buffer[i] = asyncRecvData.buffer[asyncRecvData.readPosition + i];
        }

        #endregion

        asyncRecvData.socket.BeginReceive(
            asyncRecvData.buffer,
            asyncRecvData.recvSize,
            asyncRecvData.buffer.Length - asyncRecvData.recvSize,
            SocketFlags.None,
            recvCallback,
            asyncRecvData);
    }

    void InvokePacketEvent(PacketRaw packetRaw)
    {
        switch ((PacketId)packetRaw.PecketID)
        {
            case PacketId.ID_GAMESEVER_RES_GAMESERVER_ENTER:
                break;
        }
    }

    void SendCallback(IAsyncResult asyncResult)
    {
        if(isConnected == false)
        {
            return;
        }

        AsyncSendStateObject asyncSendData = (AsyncSendStateObject)asyncResult;

        int sendSize = 0;

        try
        {
            sendSize = asyncSendData.socket.EndSend(asyncResult);
        }
        catch(SocketException ex)
        {
            ProcessException(ex);
        }

        //보낼게 다안보내 졌을 경우
        if(sendSize < asyncSendData.sendSize)
        {
            socket.BeginSend(
                asyncSendData.buffer,
                sendSize,
                asyncSendData.buffer.Length - sendSize,
                SocketFlags.Truncated,
                SendCallback,
                asyncSendData);

            Debug.Log("Send additionally");
        }
        Debug.Log("Send Complete");
    }

    void ProcessException(SocketException ex)
    {
        var errorCode = (SocketError)ex.ErrorCode;

        if (errorCode == SocketError.ConnectionAborted ||
            errorCode == SocketError.Disconnecting ||
            errorCode == SocketError.HostDown ||
            errorCode == SocketError.Shutdown ||
            errorCode == SocketError.SocketError ||
            errorCode == SocketError.ConnectionReset)
        {
            //TODO: 오류 일때
        }
    }
}

public class AsyncRecvStateObject
{
    public byte[] buffer;
    public Socket socket;
    public int recvSize;
    public int readPosition;

    public AsyncRecvStateObject(int buffersize, Socket sock)
    {
        recvSize = 0;
        readPosition = 0;
        buffer = new byte[buffersize];
        socket = sock;
    }
}

public class AsyncSendStateObject
{
    public byte[] buffer;
    public Socket socket;
    public int sendSize;

    public AsyncSendStateObject(Socket sock)
    {
        sendSize = 0;
        socket = sock;
    }
}