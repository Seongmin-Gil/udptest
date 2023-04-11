using System.Collections;
using System.Collections.Generic;
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
    public Text port;
    public GameObject updClient;
    public ToServerPacket m_SendPacket = new ToServerPacket();

    private string sendData;

    // Start is called before the first frame update
    void Start()
    {
        sendData = "";
        InitialToggle();
    }

//Start Btn 
    public void TestingStart()
    {
        Client m_upd = updClient.GetComponent<Client>();
        m_upd.m_Ip = ipAddress.text;
        m_upd.m_Port = int.Parse(port.text);
        m_SendPacket.m_StringlVariable = PackingData();
        ToServerPacket sendPacket = m_SendPacket;
        m_upd.Send(sendPacket);
        sendData = "";
        m_upd.testing = true;
    }

//Packing the Send Data to one string 
    public string PackingData()
    {
        ConvertString(inputMain[0].isOn);
        ConvertString(inputEmergency[0].isOn);
        ConvertString(inputAlarm[0].isOn);
        ConvertString(inputPilotBurner[0].isOn);
        ConvertString(inputMainBurner[0].isOn);
        Debug.Log("dd" + sendData);
        return sendData;   
    }

//Input Data Conver On = 1 , Off = 0
    public string ConvertString(bool state)
    {
        string word = state ? "1" : "0" ;
        sendData += word;
        return sendData;
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
