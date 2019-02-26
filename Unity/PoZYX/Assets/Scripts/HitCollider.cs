using UnityEngine;
using Core;
using Feature.Networking;

public class HitCollider : MonoBehaviour {
	public UDPSend UDPSend;
	public MotorSpeed MotorSpeed;

	public FloatVariable Intensity;
	public int MotorIndex;

	private void Awake() {
		MotorSpeed = UDPSend.motorSpeed;
	}
	void OnTriggerStay(Collider collider) {
		MotorSpeed.MotorsSpeed[MotorIndex] = (int)Intensity.Value;

		//UDPSend.sendMotorInfo();
		EventManager.TriggerEvent(NetworkingEventTypes.SEND_DATA);
	}

	void OnTriggerExit(Collider other) {
		for (int i = 0; i < 8; i++) {
			MotorSpeed.MotorsSpeed[i] = 0;
		}
		
		//UDPSend.sendMotorInfo();
		EventManager.TriggerEvent(NetworkingEventTypes.SEND_DATA);
	}
}
