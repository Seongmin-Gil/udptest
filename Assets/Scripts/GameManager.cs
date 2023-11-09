using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Toggle[] inputMain;
    public Toggle[] inputEmergency;
    public Toggle[] inputAlarm;
    public Toggle[] inputPilotBurner;
    public Toggle[] inputMainBurner;
    public Text ipAddress;
    public Text receivePort;
    public Text sendPort;
    public GameObject updClient;
    public GameObject udpReceiver;
    public SendPacket sendPacket;
    public GameObject outputResult;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        sendPacket = new SendPacket();
        InitialToggle();
    }

    public void ReceiveResult(List<bool> bools)
    {
        Output outCtrl = outputResult.GetComponent<Output>();
        outCtrl.OutputHandle(bools);
    }

//Start Btn 
    public void TestingStart()
    {
        UDPClient m_upd = updClient.GetComponent<UDPClient>();
        Server r_udp = udpReceiver.GetComponent<Server>();
        m_upd.serverIP = ipAddress.text;
        r_udp.port = int.Parse(receivePort.text);
        m_upd.sendPort = int.Parse(sendPort.text);
        byte[] dataArray = PackingData();
        sendPacket.Data = dataArray;
        m_upd.Send(sendPacket);
        r_udp.StartReceive();
    }

//Packing the Send Data to one string 
    public byte[] PackingData()
    {
        byte[] byteArray = new byte[5];
        bool[] bools = new bool[5];
        bools[0] = inputMain[0].isOn;
        bools[1] = inputEmergency[0].isOn;
        bools[2] = inputAlarm[0].isOn;
        bools[3] = inputPilotBurner[0].isOn;
        bools[4] = inputMainBurner[0].isOn;
        for(int i = 0;i < bools.Length; i++)
        {
            byteArray[i] = ConvertByte(bools[i]);
        }
        return byteArray;   
    }

    public byte ConvertByte(bool boolean)
    {
        byte result = Convert.ToByte(boolean);
        return result;
    }

//Initial Toggle Setting
    public void InitialToggle()
    {
        inputMain[1].isOn = true;
        inputEmergency[1].isOn = true;
        inputAlarm[1].isOn = true;
        inputPilotBurner[1].isOn = true;
        inputMainBurner[1].isOn = true;
    }

//Case1 Btn Event
    public void OnClickCaseOne()
    {
        InitialToggle();
        inputMain[0].isOn = true;
        ChangeEmergency();
        inputMainBurner[0].isOn = true;
        inputPilotBurner[0].isOn = true;
    }

//Case2 Btn Event
    public void OnClickCaseTwo()
    {
        InitialToggle();
        inputMain[0].isOn = true;
        ChangeEmergency();
        inputMainBurner[1].isOn = true;
        inputPilotBurner[1].isOn = true;
    }

//Case3 Btn Event
    public void OnClickCaseThree()
    {
        InitialToggle();
        inputMain[0].isOn = true;
        ChangeEmergency();
        inputMainBurner[1].isOn = true;
        inputPilotBurner[0].isOn = true;
    }

//Case4 Btn Event
    public void OnClickCaseFour()
    {
        InitialToggle();
        inputMain[1].isOn = true;
        ChangeEmergency();
        inputMainBurner[1].isOn = true;
        inputPilotBurner[1].isOn = true;
    }

//Conver Toggle State depending on Main Toggle
    public void ChangeEmergency()
    {
        if(inputMain[0].isOn)
        {
            inputEmergency[1].isOn = true;
            inputAlarm[1].isOn = true;
        } else {
            inputEmergency[0].isOn = true;
            inputAlarm[0].isOn = true;
        }
    }
}
