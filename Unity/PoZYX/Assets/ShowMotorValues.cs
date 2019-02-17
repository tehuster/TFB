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
       motorOutput = gameObject.GetComponent<ProgressBar>().currentPercent;
   }
    
    void Update()
    {
        motorOutput = MotorSpeed.MotorsSpeed[MotorIndex];
    }
}
