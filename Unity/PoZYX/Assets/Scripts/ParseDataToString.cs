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

//{
//    "session_info": {
//        "id": "5048fde7c4aa917cbd4d8e13",
//        "websiteId": "50295e80e4b096e761d7e4d3",
//        "enabled": true,
//        "starred": false,
//    },

//    "user_data": [
//        {
//            "timestamp": "5048fde7c4aa917cbd4d8e13",
//            "pos": {
//                "x": 89,
//                "y": 37,
//                "z": 94            
//            },    
//            "motor_intensity": [
//                10, 10, 10, 10, 10, 10, 10, 10                          
//            ],   
//            "distance": 23,
//        },
//        {
//            "timestamp": "5048fde7c4aa917cbd4d8e13",
//            "pos": {
//                "x": 89,
//                "y": 37,
//                "z": 94            
//            },    
//            "motor_intensity": [
//                10, 10, 10, 10, 10, 10, 10, 10                          
//            ],   
//            "distance": 23,
//        }
//    ]  
//}