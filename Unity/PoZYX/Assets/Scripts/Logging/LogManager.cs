using UnityEngine;
using System.IO;
using Feature.Session;
using Core;
using Feature.Room;

public class LogManager : MonoBehaviour {
    [SerializeField] private SessionDataModel sessionData;
    [SerializeField] private ParseDataToString parseDataToString;
    private StreamWriter SW;

    private void Start() {
        EventManager.StartListening(RoomEventTypes.LOADED_NEW_ROOM, OnRoomHasLoaded);
    }
    private void OnDestroy() {
        EventManager.StopListening(RoomEventTypes.LOADED_NEW_ROOM, OnRoomHasLoaded);
    }

    private void OnRoomHasLoaded(object[] data) {
        string path = "USER_LOGS/" + sessionData.Name + ".txt";
        CreateFile(path);
        InitStreamWriter(path);
        WriteLineToFile("Started");
        InvokeRepeating("LogData", 0f, 0.04f);
    }

    private void LogData() {
        WriteLineToFile(parseDataToString.GetCurrentDataString());
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
        //Remember to close your StreamWriter!
        SW.Close();
    }
}