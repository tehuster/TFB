using System;
using System.IO;
using UnityEngine;
using Feature.Session;
using Core;
using Feature.Room;

public class LogManager : MonoBehaviour {
    [SerializeField] private SessionDataModel sessionData;
    [SerializeField] private ParseDataToString parseDataToString;
    [SerializeField] private POZYXVariable pozyxData;
    [SerializeField] private MotorSpeed motorData;

    private StreamWriter SW;

    private void Start() {
        EventManager.StartListening(RoomEventTypes.LOADED_NEW_ROOM, OnRoomHasLoaded);
    }
    private void OnDestroy() {
        EventManager.StopListening(RoomEventTypes.LOADED_NEW_ROOM, OnRoomHasLoaded);
    }

    private void OnRoomHasLoaded(object[] data) {
        Debug.Log(Application.persistentDataPath);
        string path = Application.persistentDataPath + "/" + sessionData.Name + "_" + System.DateTime.Now.Millisecond + ".txt";
        CreateFile(path);
        InitStreamWriter(path);

        LogStartMessage();

        InvokeRepeating("LogData", 0f, 0.04f);
    }

    private void LogStartMessage() {
        WriteLineToFile("{");
        WriteLineToFile("\t\"session_info\": {");
        WriteLineToFile("\t\t\"id\": \"" + "\",");
        WriteLineToFile("\t\t\"websiteId\": \"" + "\",");
        WriteLineToFile("\t\t\"enabled\": " + ",");
        WriteLineToFile("\t\t\"starred\": " + ",");
        WriteLineToFile("\t\t\"name\": \"" + sessionData.Name + "\",");
        WriteLineToFile("\t\t\"date\": \"" + sessionData.Date + "\",");
        WriteLineToFile("\t\t\"disability\": \"" + sessionData.Disability + "\",");
        WriteLineToFile("\t},");
        WriteLineToFile("\t\"user_info\": [");
    }

    private void LogData() {
        WriteLineToFile("\t{");
        WriteLineToFile(GetCurrentDataString());
        WriteLineToFile("\t},");
    }

    public string GetCurrentDataString() {
        string dataString;
        DateTime currentDate = DateTime.Now;

        dataString = "\t\t\"timestamp\": \"" + currentDate.ToString("MM'/'dd'/'yyyy HH':'mm':'ss.fff") + "\" ," + SW.NewLine;
        dataString += "\t\t\"pos\": {" + SW.NewLine;
        dataString += "\t\t\t\"x\": \"" + pozyxData.x + "\"," + SW.NewLine;
        dataString += "\t\t\t\"y\": \"" + pozyxData.y + "\"," + SW.NewLine;
        dataString += "\t\t\t\"z\": \"" + pozyxData.yaw + "\"," + SW.NewLine;
        //dataString += pozyxData.z + ",";
        dataString += "\t\t}," + SW.NewLine;
        dataString += "\t\t\"motor_intensity\": [" + SW.NewLine;
        dataString += "\t\t\t";
        foreach (int Motor in motorData.MotorsSpeed)
            dataString += Motor + ",";
        
        dataString += SW.NewLine + "\t\t]" + SW.NewLine;
        //      Debug.Log(dataString);
        return dataString;
    }

    private void CreateFile(string fileName) {
        //Add timestamp
        //Unique ID
        //Identifier
        //Location
        File.Create(fileName).Dispose();
    }

    private void InitStreamWriter(string fileName) {
        //SW.AutoFlush = true;
        SW = new StreamWriter(fileName, true);
    }

    private void WriteLineToFile(string Line) {
        SW.WriteLine(Line);
        SW.Flush();
    }

    private void OnApplicationExit() {
        WriteLineToFile("}");
        SW.Close();
    }
}