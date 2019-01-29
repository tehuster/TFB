using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitCollider : MonoBehaviour {

    public UDPSend UDPSend;
    public MotorSpeed MotorSpeed;

    private void Awake() {
        MotorSpeed = UDPSend.motorSpeed;
    }
	 void OnTriggerStay(Collider collider)
    {
            Debug.Log("Entering");
		    for(int i=0; i<8; i++){
                MotorSpeed.MotorsSpeed[i] = 50;
            }    
            UDPSend.sendMotorInfo();
    }

    void OnTriggerExit(Collider other) {
             Debug.Log("Leaving");
               for(int i=0; i<8; i++){
                MotorSpeed.MotorsSpeed[i] = 0;
            } 
            UDPSend.sendMotorInfo();
    }
}
