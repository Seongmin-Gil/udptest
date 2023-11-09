using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Collections;
using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Linq;

public class UDPClient : MonoBehaviour
{
    public string serverIP;  // 서버 IP 주소
    public int sendPort; // 발신서버 포트 번호
    public SendPacket sendPacket = new SendPacket();

    private void Start()
    {
        Application.targetFrameRate = 60;
    }

    public void Send(SendPacket data)
    {
        UdpClient SendClient = new UdpClient(sendPort);
        IPEndPoint sendEndPoint = new IPEndPoint(IPAddress.Parse(serverIP), sendPort);
        try
        {
            byte[] bytes = SendPacket.Serialize(data);
            SendClient.Send(bytes, bytes.Length, serverIP, sendPort);
            Debug.Log($"[Send] {serverIP}:{sendPort} byte : {bytes.Length}");
        }
        catch (Exception ex)
        {
            Debug.Log(ex.ToString());
            
        }
        finally
        {
            SendClient.Close();
        }
    }
}
