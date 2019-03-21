using UnityEngine;
using Core;
using Feature.Networking;

public class HitCollider : MonoBehaviour {
	public UDPSend UDPSend;
	public MotorSpeed MotorSpeed;

	public FloatVariable Intensity;
	public int MotorIndex;

	private void Awake() {
		MotorSpeed.MotorsSpeed[MotorIndex] = 0;
		MotorSpeed = UDPSend.motorSpeed;
	}
	void OnTriggerStay(Collider collider) {
		MotorSpeed.MotorsSpeed[MotorIndex] = (int)Intensity.Value;
		EventManager.TriggerEvent(NetworkingEventTypes.SEND_DATA);
	}

	void OnTriggerExit(Collider other) {
		MotorSpeed.MotorsSpeed[MotorIndex] = 0;
		EventManager.TriggerEvent(NetworkingEventTypes.SEND_DATA);
	}
}
