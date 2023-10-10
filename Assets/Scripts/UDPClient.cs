using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Collections;
using System;
using System.Runtime.InteropServices;

public class UDPClient : MonoBehaviour
{
    private UdpClient client;
    private IPEndPoint remoteEndPoint;
    public ToClientPacket m_ReceivePacket = new ToClientPacket();
    public string serverIP;  // 서버 IP 주소
    public int port;  // 수신서버 포트 번호
    public int sendPort; // 발신서버 포트 번호

    private void Start()
    {
        Application.targetFrameRate = 60;
        client = new UdpClient(port);
        remoteEndPoint = new IPEndPoint(IPAddress.Any, port);
      
        // StartCoroutine(ReceiveMessage());
    }

    private IEnumerator ReceiveMessage()
    {
        while(true)
        {
            byte[] receiveBytes = client.Receive(ref remoteEndPoint);
            m_ReceivePacket = ByteArrayToStruct<ToClientPacket>(receiveBytes);
            // string receiveMessage = Encoding.UTF8.GetString(receiveBytes);
            Debug.Log(m_ReceivePacket.m_BoolVariable);
            yield return null;
        }
    }

    public void Send(ToServerPacket sendPacket)
    {
        UdpClient SendClient = new UdpClient(sendPort);
        IPEndPoint sendEndPoint = new IPEndPoint(IPAddress.Parse(serverIP), sendPort);
        try
        {
            byte[] bytes = StructToByteArray(sendPacket);
            SendClient.Send(bytes, bytes.Length, serverIP, sendPort);
            Debug.Log($"[Send] {serverIP}:{sendPort} byte : {bytes.Length}");
            Debug.Log(sendPacket.m_StringlVariable);
            sendPacket.m_StringlVariable = "";
        }
        catch (Exception ex)
        {
            Debug.Log(ex.ToString());
            return;
        }
    }

    private byte[] StructToByteArray(object obj)
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

    private void OnApplicationQuit()
    {
        client.Close();
    }
}
