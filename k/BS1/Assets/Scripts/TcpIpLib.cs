using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System;
using System.Text;
using UnityEngine;
using Packet;

public class TcpIpLib : MonoBehaviour
{
    int bufferSize = 131072;
    int headerSize = sizeof(int)*2;
    bool isConnected = false;

    Socket socket;
    AsyncCallback recvCallback;
    AsyncCallback sendCallback;
    Queue<PacketRaw> packetQueue;

    // Use this for initialization
    void Start()
    {
        initSocket();
    }

    // Update is called once per frame
    void Update()
    {
        while (IsQueueEmpty() == false)
        {
            var packet = networkTcpIp.GetPacket();

            packetProcessor.ProcessPacket(packet);
        }
    }

    void OnApplicationQuit()
    {
        CloseConnection();
    }

    void initSocket()
    {
        socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        recvCallback = new AsyncCallback(RecvCallback);
        sendCallback = new AsyncCallback(SendCallback);
        packetQueue = new Queue<PacketRaw>();
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

    public void Connect(string ip, int port)
    {
        try
        {
            socket.BeginConnect(ip, port, ConnectCallback, 0);
        }
        catch
        {
            isConnected = false;
        }
    }

    void ConnectCallback(IAsyncResult asyncResult)
    {
        try
        {
            socket.EndConnect(asyncResult);
            isConnected = true;
        }
        catch
        {
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

    public bool IsQueueEmpty()
    {
        lock(this)
        {
            if(packetQueue.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public PacketRaw GetPacket()
    {
        lock(this)
        {
            return packetQueue.Dequeue();
        }
    }

    public void SendPacket<T>(T data, PacketId pktID)
    {
        if (isConnected == false)
        {
            return;
        }

        //뉴 딜리트가센드 할때마다 일어나는게 문제가 될까?
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
        while(true)
        {
            //헤더 조차 못받아 왔다면 더 기다린다.
            if(asyncRecvData.recvSize < headerSize)
            {
                break;
            }

            unsafe
            {
                fixed(byte* packetHeaderByte = &asyncRecvData.buffer[asyncRecvData.readPosition])
                {
                    PacketHeader* packetHeader = (PacketHeader*)packetHeaderByte;

                    int packetSize = packetHeader->BodySize + headerSize;

                    //헤더에 관련된 바디를 못받아 왔을때 더 기다린다.
                    if(asyncRecvData.recvSize < packetSize)
                    {
                        break;
                    }

                    PacketRaw packetRaw = new PacketRaw();

                    packetRaw.BodySize = packetHeader->BodySize;
                    packetRaw.PecketID = packetHeader->PacketID;

                    packetRaw.Data = Encoding.ASCII.GetString(
                        asyncRecvData.buffer,
                        asyncRecvData.readPosition + headerSize,
                        packetHeader->BodySize);

                    lock(this)
                    {
                        packetQueue.Enqueue(packetRaw);
                    }

                    asyncRecvData.readPosition += packetSize;
                    asyncRecvData.recvSize -= packetSize;
                }
            }
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
        }
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