using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Normal using statements here
using System.IO;
 
public class LogManager : MonoBehaviour
{

StreamWriter SW;

private void Start() {
	string path = "test.txt";
	File.Create(path).Dispose();
	//Write some text to the test.txt file
	SW = new StreamWriter(path, true);
	SW.AutoFlush = true;
	SW.WriteLine("Test");
	//SW.Close();
}
       
 
void Update()
{
//Do stuff
SW.WriteLine("Your Data");
Debug.Log("logged");
}
 
void OnApplicationExit()
{
//Remember to close your StreamWriter!
SW.Close();
}
}