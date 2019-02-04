using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Normal using statements here
using System;
using System.IO;

public class LogManager : MonoBehaviour
{

    StreamWriter SW;
	ParseDataToString ParseDataToString;

	private void Awake() {
		ParseDataToString = GetComponent<ParseDataToString>();
	}
    private void Start()
    {
        string path = "USER_LOGS/test.txt";
        CreateFile(path);
		InitStreamWriter(path); //Don't forget to close;      
        WriteLineToFile("Started");
		InvokeRepeating("LogData", 0f, 0.04f);
    }
 
    

    void LogData()
    {	
		WriteLineToFile(ParseDataToString.GetCurrentDataString());
    }

	void CreateFile(string fileName){
		//Add timestamp
		//Unique ID
		//Identifier
		//Location
		File.Create(fileName).Dispose();
	}

	void InitStreamWriter(string fileName){
		//SW.AutoFlush = true;
		SW = new StreamWriter(fileName, true);		
       
	}

	void WriteLineToFile(string Line){
		SW.WriteLine(Line);
		SW.Flush();
	}

    void OnApplicationExit()
    {
        //Remember to close your StreamWriter!
        SW.Close();
    }
}