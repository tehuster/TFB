﻿using UnityEngine;
using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Feature.Networking;

public class UDPReceive : MonoBehaviour {
	[SerializeField] private NetworkingDataModel networkData;

	//public StringVariable UDP_Message;
	// receiving Thread
	Thread receiveThread;

	// udpclient object
	UdpClient client;

	// public
	//public string IP;
	//public int port;

	// infos
	//public string lastReceivedUDPPacket = "";
	//public string allReceivedUDPPackets = ""; // clean up this from time to time!


	// start from shell
	private static void Main() {
		UDPReceive receiveObj = new UDPReceive();
		receiveObj.init();

		string text = "";
		do {
			text = Console.ReadLine();
		}
		while (!text.Equals("exit"));
	}
	// start from unity3d
	public void Start() {
		init();
	}

	// OnGUI
	// void OnGUI()
	// {
	//     Rect rectObj = new Rect(40, 10, 200, 400);
	//     GUIStyle style = new GUIStyle();
	//     style.alignment = TextAnchor.UpperLeft;
	//     GUI.Box(rectObj, "# UDPReceive\n127.0.0.1 " + port + " #\n"
	//                 + "shell> nc -u 127.0.0.1 : " + port + " \n"
	//                 + "\nLast Packet: \n" + lastReceivedUDPPacket
	//                 + "\n\nAll Messages: \n" + allReceivedUDPPackets
	//             , style);
	// }

	// init
	private void init() {
		// Status
		Debug.Log("Receiving on " + networkData.IP + ":" + networkData.receivePort);

		receiveThread = new Thread(
			new ThreadStart(ReceiveData));
		receiveThread.IsBackground = true;
		receiveThread.Start();
	}

	// receive thread
	private void ReceiveData() {
		client = new UdpClient(networkData.receivePort);
		while (true) {
			try {
				// Bytes empfangen.
				IPEndPoint anyIP = new IPEndPoint(IPAddress.Any, networkData.receivePort);
				byte[] data = client.Receive(ref anyIP);

				// Bytes mit der UTF8-Kodierung in das Textformat kodieren.
				string text = Encoding.UTF8.GetString(data);

				// Den abgerufenen Text anzeigen.
				//Debug.Log(">> " + text);

				// latest UDPpacket
				networkData.dataString = text;

				// ....
				//allReceivedUDPPackets = allReceivedUDPPackets + text;

			} catch (Exception err) {
				print(err.ToString());
			}
		}
	}

	// getLatestUDPPacket
	// cleans up the rest
	public string getLatestUDPPacket() {
		//allReceivedUDPPackets = "";
		//return lastReceivedUDPPacket;
        return "";
	}

	void OnDisable() {
		if (receiveThread != null)
			receiveThread.Abort();

		client.Close();
	}
}
