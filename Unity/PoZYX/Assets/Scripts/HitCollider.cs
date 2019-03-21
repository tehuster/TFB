using UnityEngine;
using Core;
using Feature.Networking;
using System.Collections;

public class HitCollider : MonoBehaviour {
	public UDPSend UDPSend;
	public MotorSpeed MotorSpeed;

	public FloatVariable Intensity;
	public int MotorIndex;

    private bool isActive;

	private void Start() {
		MotorSpeed.MotorsSpeed[MotorIndex] = 0;
		MotorSpeed = UDPSend.motorSpeed;

        StartCoroutine(TickRate());
	}
	void OnTriggerStay(Collider collider) {
		MotorSpeed.MotorsSpeed[MotorIndex] = (int)Intensity.Value;
        isActive = true;
	}

	void OnTriggerExit(Collider other) {
		MotorSpeed.MotorsSpeed[MotorIndex] = 0;
		EventManager.TriggerEvent(NetworkingEventTypes.SEND_DATA);
        isActive = false;
	}

    private IEnumerator TickRate() {
        while (true) {
            if (isActive)
                EventManager.TriggerEvent(NetworkingEventTypes.SEND_DATA);

            yield return new WaitForSeconds(MotorSpeed.sendInterval);
        }
    }
}
