using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Michsky.UI.ModernUIPack;

public class ShowMotorValues : MonoBehaviour
{

   public MotorSpeed MotorSpeed;
   public int MotorIndex;
   private float motorOutput;
   private void Awake() {
       motorOutput = GetComponent<ProgressBar>().currentPercent;
       Debug.Log(motorOutput);
   }
    
    void Update()
    {
        //motorOutput = MotorSpeed.MotorsSpeed[MotorIndex];
        Debug.Log(motorOutput);
    }
}
