using System;
using System.IO;
using UnityEngine;
using Feature.Session;
using Core;
using System.Collections;
using System.Collections.Generic;

public class LogManager : MonoBehaviour {
	[SerializeField] private SessionDataModel sessionData;
	[SerializeField] private POZYXVariable pozyxData;
	[SerializeField] private MotorSpeed motorData;
	[Header("Logging Tweakables")]
	[SerializeField] private float logIntervalTime;

	private StreamWriter SW;
	private Coroutine logUserDataCoroutine;
    private bool isLogging;
    private Dictionary<string, string> comments = new Dictionary<string, string>();

	private void Start() {
        EventManager.StartListening(SessionEventTypes.START, OnSessionStarted);
        EventManager.StartListening(SessionEventTypes.STOP, OnSessionStopped);
        EventManager.StartListening(SessionEventTypes.ADD_COMMENT, OnAddComment);
	}
	private void OnDestroy() {
        EventManager.StopListening(SessionEventTypes.START, OnSessionStarted);
        EventManager.StopListening(SessionEventTypes.STOP, OnSessionStopped);
        EventManager.StopListening(SessionEventTypes.ADD_COMMENT, OnAddComment);
	}

	private void OnSessionStarted(object[] data) {
        isLogging = true;
        comments.Clear();

        SetSessionID(data);
		CreateLogFile();
		LogSessionInfo();
		logUserDataCoroutine = StartCoroutine(LogUserData());
	}

    private void OnSessionStopped(object[] arg0) {
        SaveLogFile();
    }

    private void OnAddComment(object[] data){
        string text = (string)data[0];

        comments.Add(GetCurrentDateAndTime(), text);
    }

	private void CreateLogFile() {
		string path = Application.persistentDataPath + "/" + sessionData.Name + "_" + sessionData.SessionID + ".txt";

        Debug.Log("Created a new file at: " + path);

		File.Create(path).Dispose();
		SW = new StreamWriter(path, true);
	}

    private void SetSessionID(object[] data){
        string[] split = DateTime.Now.TimeOfDay.ToString().Split(new Char[] { ':', '.' });
        string id = "";

        for (int i = 0; i < split.Length; i++)
            id += split[i];

        sessionData.Name = (string)data[0];
        sessionData.Disability = (string)data[1];
        sessionData.Scenario = (string)data[2];
        sessionData.SessionID = id;
        sessionData.Date = DateTime.Now.Day.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Year.ToString();
    }

	private void LogSessionInfo() {
		string dataString = "{" + SW.NewLine;
		dataString += "\t\"session_info\": {" + SW.NewLine;
		dataString += "\t\t\"id\": \"" + sessionData.SessionID + "\"," + SW.NewLine;
        dataString += "\t\t\"name\": \"" + sessionData.Name + "\"," + SW.NewLine;
        dataString += "\t\t\"date\": \"" + sessionData.Date + "\"," + SW.NewLine;
        dataString += "\t\t\"scenario\": " + sessionData.Scenario + "," + SW.NewLine;
		dataString += "\t\t\"disability\": \"" + sessionData.Disability + "\"," + SW.NewLine;
		dataString += "\t\t\"log_interval\": \"" + logIntervalTime + "\"," + SW.NewLine;
		dataString += "\t}," + SW.NewLine;
		dataString += "\t\"user_info\": [";

		WriteLineToFile(dataString);
	}

	private IEnumerator LogUserData() {
		while (true) {
			GetCurrentUserDataString();
			yield return new WaitForSeconds(logIntervalTime);
		}
	}

	private void GetCurrentUserDataString() {
		string dataString = "\t\t{" + SW.NewLine;
        dataString += "\t\t\t\"timestamp\": \"" + GetCurrentDateAndTime() + "\"," + SW.NewLine;
		dataString += "\t\t\t\"pos\": {" + SW.NewLine;
		dataString += "\t\t\t\t\"x\": \"" + pozyxData.x + "\"," + SW.NewLine;
		dataString += "\t\t\t\t\"y\": \"" + pozyxData.y + "\"," + SW.NewLine;
		dataString += "\t\t\t\t\"z\": \"" + pozyxData.yaw + "\"," + SW.NewLine;
		dataString += "\t\t\t}," + SW.NewLine;
		dataString += "\t\t\t\"motor_intensity\": [" + SW.NewLine;
		dataString += "\t\t\t\t";

		foreach (int Motor in motorData.MotorsSpeed)
			dataString += Motor + ", ";

		dataString += SW.NewLine + "\t\t\t]" + SW.NewLine;
		dataString += "\t\t},";

		WriteLineToFile(dataString);
	}

    private void LogComments() {
        if (comments.Count <= 0){
            WriteLineToFile(SW.NewLine);
            return;
        }

        WriteLineToFile("\t\"comments\": [");

        foreach (KeyValuePair<string, string> comment in comments)
            SetSessionCommentDataString(comment.Key, comment.Value);

        WriteLineToFile("\t]" + SW.NewLine + "}");
    }

    private void SetSessionCommentDataString(string date, string value) {
        string dataString = "\t\t{" + SW.NewLine;
        dataString += "\t\t\t\"timestamp\": \"" + date + "\"," + SW.NewLine;
        dataString += "\t\t\t\"comment\": \"" + value + "\"," + SW.NewLine;
        dataString += "\t\t},";

        WriteLineToFile(dataString);
    }

    private string GetCurrentDateAndTime(){
        return DateTime.Now.ToString("dd'/'MM'/'yyyy HH':'mm':'ss.fff");
    }

	private void WriteLineToFile(string Line) {
		SW.WriteLine(Line);
		SW.Flush();
	}

	private void OnApplicationExit() {
		SaveLogFile();
	}

	private void SaveLogFile() {
        if (!isLogging)
            return;
        
        isLogging = false;
		StopCoroutine(logUserDataCoroutine);

        if (comments.Count <= 0)
            WriteLineToFile("\t]" + SW.NewLine + "}");
        else
            WriteLineToFile("\t],");
        
        LogComments();

		SW.Close();
	}
}