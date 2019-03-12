using UnityEngine;
using System;

public class ParseDataToString : MonoBehaviour {

    public POZYXVariable POZYX; //Do i want POZYX or VIRTUAL TRANSFORM POS?
    public MotorSpeed MSpeed;

    public string GetCurrentDataString() {
        string dataString;
        DateTime currentDate = DateTime.Now;

        dataString = currentDate.ToString("MM'/'dd'/'yyyy HH':'mm':'ss.fff") + ",";
        dataString += POZYX.x + ",";
        dataString += POZYX.y + ",";
        dataString += POZYX.z + ",";
        dataString += POZYX.yaw + ",";

        foreach (int Motor in MSpeed.MotorsSpeed) {
            dataString += Motor + ",";
        }
        dataString += ";";
        //		Debug.Log(dataString);
        return dataString;
    }
}