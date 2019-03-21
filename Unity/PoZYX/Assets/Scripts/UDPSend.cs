using UnityEngine;
using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using Core;
using Feature.Networking;

public class UDPSend : MonoBehaviour {
	[SerializeField] private NetworkingDataModel networkData;

	public MotorSpeed motorSpeed;
	private static int localPort;
	private byte[] sendData = new byte[21];
	// prefs
	//public string IP;  // define in init
	//public int port;  // define in init

	IPEndPoint remoteEndPoint;
	UdpClient client;
	// gui
	string strMessage = "";

	//private bool MotorToggle = true;

	private static void Main() {
		UDPSend sendObj = new UDPSend();
		sendObj.init();
	}

	private void Start() {
		init();

		EventManager.StartListening(NetworkingEventTypes.SEND_DATA, SendMotorInfo);
        EventManager.StartListening(NetworkingEventTypes.TOGGLE_MOTORS, ToggleMotors);
	}

	private void OnDestroy() {
		EventManager.StopListening(NetworkingEventTypes.SEND_DATA, SendMotorInfo);
        EventManager.StopListening(NetworkingEventTypes.TOGGLE_MOTORS, ToggleMotors);
	}

	public void init() {
		remoteEndPoint = new IPEndPoint(IPAddress.Parse(networkData.IP), networkData.sendPort);
		client = new UdpClient();
		Debug.Log("Sending to " + networkData.IP + ":" + networkData.sendPort);
	}

	private void TurnOffBelt() {
        if (motorSpeed.MotorState)
            motorSpeed.MotorState = false;

		try {
			byte[] data = Encoding.UTF8.GetBytes("0000000000000000;");
			client.Send(data, data.Length, remoteEndPoint);
		} catch (Exception err) {
			print(err.ToString());
		}
	}

	private void SendMotorInfo(object[] arg0 = null) {		
        if (motorSpeed.MotorState) {			
			sendData[0] = 255; //START BYTE
			sendData[1] = 1;  //ADDRESS	
			sendData[2] = 16;  //MODE
			sendData[3] = 16;  //PACKETSIZE			
			for (int i = 0; i < 16; i++) {
				sendData[i+4] = Convert.ToByte(motorSpeed.MotorsSpeed[i]);

				if (motorSpeed.MotorsSpeed[i] >= 256 || motorSpeed.MotorsSpeed[i] <= -1) 
					Debug.Log("MOTOR: " + i);			
			}
			sendData[20] = 254;

			try {
				client.Send(sendData, sendData.Length, remoteEndPoint);
			} catch (Exception err) {
				print(err.ToString());
			}
		} else {
            TurnOffBelt();
		}
	}

	private void ToggleMotors(object[] data) {
        if (data[0] == null)
            return;
        
        motorSpeed.MotorState = (bool)data[0];
	}
}