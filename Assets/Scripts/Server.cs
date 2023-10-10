using System;
using UnityEngine;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;

public class Server : MonoBehaviour
{
    private UdpClient receiver;
    private int port = 11004; // 수신할 포트 번호

    void Start()
    {
        receiver = new UdpClient(port);
        receiver.Client.Blocking = false;
        IPEndPoint senderIP = new IPEndPoint(IPAddress.Any, port);
        byte[] bytes = receiver.Receive(ref senderIP);
        Debug.Log($"[Receive] Remote IpEndPoint : {senderIP.ToString()} Size : {bytes.Length} byte");
        // DoReceivePacket(); // 받은 값 처리
        // receiver.BeginReceive(new AsyncCallback(ReceiveData), null);
    }

    private void ReceiveData(IAsyncResult result)
    {
        Debug.Log("-------");
        IPEndPoint senderIP = new IPEndPoint(IPAddress.Any, port);
        byte[] receivedBytes = receiver.EndReceive(result, ref senderIP);
        string receivedData = System.Text.Encoding.ASCII.GetString(receivedBytes);

        Debug.Log("Received data: " + receivedData);

        receiver.BeginReceive(new AsyncCallback(ReceiveData), null);
    }

    void OnDisable()
    {
        receiver.Close();
    }
    // private UdpClient m_Receiver;
    // public int m_Port = 21004;
    // public string m_ReceiveMessage;
    // private IPEndPoint iPEndPoint;
    // // public ToServerPacket m_ReceivePacket = new ToServerPacket();
    // // public ToClientPacket m_SendPacket = new ToClientPacket();
    
    // void Awake()
    // {
    //     Application.targetFrameRate = 60;
    //     InitReceiver();
    // }

    // void Update()
    // {
    //     //Receive();
    // }

    // void OnApplicationQuit()
    // {
    //     CloseReceiver();
    // }

    // void InitReceiver()
    // {
    //     m_Receiver = new UdpClient(m_Port);
    //     // Debug.Log(m_Receiver.Receive(ref iPEndPoint));
    //     m_Receiver.BeginReceive(new AsyncCallback(ReceiveCallback), null);
    // }

    // void ReceiveCallback(IAsyncResult ar)
    // {
    //     Debug.Log("----------");
    //     iPEndPoint = new IPEndPoint(IPAddress.Any, m_Port);
    //     byte[] received;
    //     received = m_Receiver.EndReceive(ar, ref iPEndPoint);
    //     m_ReceiveMessage = Encoding.Default.GetString(received);
    //     DoReceive();
    //     // if(m_Receiver != null)
    //     // {
    //     //     received = m_Receiver.EndReceive(ar, ref iPEndPoint);
    //     // }
    //     // else
    //     // {
    //     //     return;
    //     // }

    //     m_Receiver.BeginReceive(new AsyncCallback(ReceiveCallback), null);
        
    //     // m_ReceiveMessage = m_ReceiveMessage.Trim();

        
    // }

    // void DoReceive()
    // {
    //     Debug.Log(m_ReceiveMessage);
    // }

    // void CloseReceiver()
    // {
    //     if(m_Receiver != null)
    //     {
    //         m_Receiver.Close();
    //         m_Receiver = null;
    //     }
    // }
    // void Receive()
    // {
    //     try
    //     {
    //         byte[] bytes = m_Receiver.Receive(ref iPEndPoint);
    //         Debug.Log($"[Receive] Remote IpEndPoint : {iPEndPoint.ToString()} Size : {bytes.Length} byte");
    //         // m_ReceivePacket = ByteArrayToStruct<ToServerPacket>(bytes);
    //         // DoReceivePacket(); // 받은 값 처리
    //     }

    //     catch (Exception ex)
    //     {
    //         Debug.Log(ex.ToString());
    //         return;
    //     }
    // }

    // void DoReceivePacket()
    // {
    //     Debug.LogFormat($"StringlVariable = {m_ReceivePacket.m_StringlVariable} ");
    // }

    // public void Send()
    // {
    //     try
    //     {
    //         SetSendPacket();
    //         byte[] bytes = StructToByteArray(m_SendPacket);
    //         m_Server.Send(bytes, bytes.Length, m_RemoteIpEndPoint);
    //         Debug.Log($"[Send] Remote IpEndPoint : {m_RemoteIpEndPoint.ToString()} Size : {bytes.Length} byte");
    //     }

    //     catch (Exception ex)
    //     {
    //         Debug.Log(ex.ToString());
    //         return;
    //     }
    // }

    // void SetSendPacket()
    // {
    //     // m_SendPacket.m_StringlVariable = "1100111";
    // }

    // void CloseServer()
    // {
    //     m_Server.Close();
    // }

    // byte[] StructToByteArray(object obj)
    // {
    //     int size = Marshal.SizeOf(obj);
    //     byte[] arr = new byte[size];
    //     IntPtr ptr = Marshal.AllocHGlobal(size);

    //     Marshal.StructureToPtr(obj, ptr, true);
    //     Marshal.Copy(ptr, arr, 0, size);
    //     Marshal.FreeHGlobal(ptr);
    //     return arr;
    // }

    // T ByteArrayToStruct<T>(byte[] buffer) where T : struct
    // {
    //     int size = Marshal.SizeOf(typeof(T));
    //     if (size > buffer.Length)
    //     {
    //         throw new Exception();
    //     }

    //     IntPtr ptr = Marshal.AllocHGlobal(size);
    //     Marshal.Copy(buffer, 0, ptr, size);
    //     T obj = (T)Marshal.PtrToStructure(ptr, typeof(T));
    //     Marshal.FreeHGlobal(ptr);
    //     return obj;
    // }
}