using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Output : MonoBehaviour
{
    public Toggle[] outMode;
    public Toggle[] outMain;
    public Toggle[] outEmergency;
    public Toggle[] outAlarm;
    public Toggle[] outPilot;
    public Toggle[] outBurner;
    public Toggle[] outResult;

    // Start is called before the first frame update
    void Start()
    {
        InitialToggle();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitialToggle()
    {
        outMode[1].isOn = true;
        outMain[1].isOn = true;
        outEmergency[1].isOn = true;
        outAlarm[1].isOn = true;
        outPilot[1].isOn = true;
        outBurner[1].isOn = true;
        outResult[1].isOn = true;
    }

    public void OutputHandle(List<bool> bools)
    {
        if(bools.Count > 0)
        {
            ChangeValue(bools[0], outMode);
            ChangeValue(bools[1], outMain);
            ChangeValue(bools[2], outEmergency);
            ChangeValue(bools[3], outAlarm);
            ChangeValue(bools[4], outPilot);
            ChangeValue(bools[5], outBurner);
            ChangeValue(bools[6], outResult);
        }
        //else
        //{
        //    Debug.Log("error result Num:" + bools.Count);
        //}
        Debug.Log("check");
    }

    public void ChangeValue(bool result, Toggle[] toggles)
    {
        if (result == true)
        {
            toggles[0].isOn = true;
        } else
        {
            toggles[1].isOn = true;
        }
    }
}
