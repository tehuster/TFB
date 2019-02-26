using UnityEngine;
using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using Core;
using Feature.Networking;

public class UDPSend : MonoBehaviour {
	public MotorSpeed motorSpeed;
	private static int localPort;
	// prefs
	public string IP;  // define in init
	public int port;  // define in init

	IPEndPoint remoteEndPoint;
	UdpClient client;
	// gui
	string strMessage = "";

	private bool MotorToggle = true;

	private static void Main() {
		UDPSend sendObj = new UDPSend();
		sendObj.init();
	}



	private void Start() {
		init();

		EventManager.StartListening(NetworkingEventTypes.SEND_DATA, SendMotorInfo);
	}

	private void OnDestroy() {
		EventManager.StopListening(NetworkingEventTypes.SEND_DATA, SendMotorInfo);
	}

	public void init() {
		remoteEndPoint = new IPEndPoint(IPAddress.Parse(IP), port);
		client = new UdpClient();
		Debug.Log("Sending to " + IP + ":" + port);
	}

	public void turnOffBelt() {
		try {
			byte[] data = Encoding.UTF8.GetBytes("0000000000000000;");
			client.Send(data, data.Length, remoteEndPoint);
		} catch (Exception err) {
			print(err.ToString());
		}
	}

	private void SendMotorInfo(object[] arg0 = null) {
		if (MotorToggle) {
			string motorInfo = "";
			for (int i = 0; i < 8; i++) {
				motorInfo += motorSpeed.MotorsSpeed[i].ToString("D2");
			}
			motorInfo += ";";
			//Debug.Log(motorInfo);
			try {
				byte[] data = Encoding.UTF8.GetBytes(motorInfo);

				client.Send(data, data.Length, remoteEndPoint);
			} catch (Exception err) {
				print(err.ToString());
			}
		} else {
			turnOffBelt();
		}
	}

	public void toggleMotors(bool toggle) {
		MotorToggle = toggle;
	}
}