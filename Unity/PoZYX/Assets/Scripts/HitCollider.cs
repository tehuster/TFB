using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitCollider : MonoBehaviour {

    public UDPSend UDPSend;
    public MotorSpeed MotorSpeed;

    public FloatVariable Intensity;
    public int MotorIndex;

    private void Awake() {
        MotorSpeed = UDPSend.motorSpeed;
    }
	 void OnTriggerStay(Collider collider)
    {
          
            MotorSpeed.MotorsSpeed[MotorIndex] = (int) Intensity.Value;
         
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
