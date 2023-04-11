using System;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;

public class Client : MonoBehaviour
{
    private UdpClient m_Client;
    public string m_Ip;
    public int m_Port;
    public bool testing = false;
    public ToServerPacket m_SendPacket = new ToServerPacket();
    public ToClientPacket m_ReceivePacket = new ToClientPacket();
    private IPEndPoint m_RemoteIpEndPoint;

    void Start()
    {
        Application.targetFrameRate = 60;
        InitClient();
    }

    void Update()
    {
        // Send();
        // Receive();    
    }

    void OnApplicationQuit()
    {
        CloseClient();
    }

//Client 초기 세팅
    void InitClient()
    {
        m_Client = new UdpClient(m_Port);
        m_Client.Client.Blocking = false;
        m_RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, m_Port);
    }

//UDP Server 전송
    public void Send(ToServerPacket sendPacket)
    {
        try
        {
            byte[] bytes = StructToByteArray(sendPacket);
            m_Client.Send(bytes, bytes.Length, m_Ip, m_Port);
            Debug.Log($"[Send] {m_Ip}:{m_Port} byte : {bytes.Length}");
            Debug.Log(sendPacket.m_StringlVariable);
            sendPacket.m_StringlVariable = "";
        }

        catch (Exception ex)
        {
            Debug.Log(ex.ToString());
            return;
        }
    }

//UDP Server 수신
    // void Receive()
    // {
    //     try
    //     {
    //         byte[] bytes = m_Client.Receive(ref m_RemoteIpEndPoint);
    //         Debug.Log($"[Receive] Remote IpEndPoint : {m_RemoteIpEndPoint.ToString()} Size : {bytes.Length} byte");
    //         m_ReceivePacket = ByteArrayToStruct<ToClientPacket>(bytes);
    //         DoReceivePacket(); // 받은 값 처리
    //     }

    //     catch (Exception ex)
    //     {
    //         Debug.Log(ex.ToString());
    //         return;
    //     }

    // }

    void DoReceivePacket()
    {
        Debug.LogFormat($"StringlVariable = {m_ReceivePacket.m_StringlVariable}");
    }

    void CloseClient()
    {
        m_Client.Close();
    }

    byte[] StructToByteArray(object obj)
    {
        int size = Marshal.SizeOf(obj);
        byte[] arr = new byte[size];
        IntPtr ptr = Marshal.AllocHGlobal(size);

        Marshal.StructureToPtr(obj, ptr, true);
        Marshal.Copy(ptr, arr, 0, size);
        Marshal.FreeHGlobal(ptr);
        return arr;
    }

    T ByteArrayToStruct<T>(byte[] buffer) where T : struct
    {
        int size = Marshal.SizeOf(typeof(T));
        if (size > buffer.Length)
        {
            throw new Exception();
        }

        IntPtr ptr = Marshal.AllocHGlobal(size);
        Marshal.Copy(buffer, 0, ptr, size);
        T obj = (T)Marshal.PtrToStructure(ptr, typeof(T));
        Marshal.FreeHGlobal(ptr);
        return obj;
    }
}