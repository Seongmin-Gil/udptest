using System;
using UnityEngine;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Collections;
using System.Collections.Generic;

public class Server : MonoBehaviour
{
    private UdpClient receiver;
    private IPEndPoint remoteEndPoint;
    public int port;  // 수신서버 포트 번호
    public ReceivePacket receivePacket = new ReceivePacket();
    public List<bool> bools;
    public GameManager gameManager;

    public void StartReceive()
    {
        receiver = new UdpClient(port);
        receiver.Client.Blocking = false;
        IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, port);
        StartCoroutine(ReceiveMessages());
    }

    private IEnumerator ReceiveMessages()
    {
        while (true)
        {
            try
            {
                
                // Use BeginReceive to perform asynchronous receive
                IAsyncResult result = receiver.BeginReceive(ReceiveCallback, null);
                // Wait until data is received or timeout (adjust timeout as needed)
                // Process any other logic while waiting for data
                if(result != null)
                {
                    gameManager.ReceiveResult(bools);
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e.ToString());
            }
            yield return null;
        }
    }

    private void ReceiveCallback(IAsyncResult result)
    {
        try
        {
            // End the asynchronous receive operation and get received data
            byte[] receiveBytes = receiver.EndReceive(result, ref remoteEndPoint);
            var receiveResult = ReceivePacket.Deserialize(receiveBytes);
            bools = new List<bool>();
            // Process received data (you need to implement the logic for handling the data)
            ConvertData(receiveResult.Data);
        }
        catch (Exception e)
        {
            Debug.LogError(e.ToString());
        }
    }

    private void ConvertData(byte[] data)
    {
        foreach (byte b in data)
        {
            bool convertResult = Convert.ToBoolean(b);
            bools.Add(convertResult);
        }
    }
}