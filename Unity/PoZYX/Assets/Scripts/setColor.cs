using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setColor : MonoBehaviour
{
        private Renderer renderer;
        public MotorSpeed MotorSpeed;
        public int MotorIndex;
    private void Awake() {
         renderer = GetComponent<Renderer>();
    }
    
    // Update is called once per frame
    void Update()
    {   
        float colorIntensity = Remap(MotorSpeed.MotorsSpeed[MotorIndex], 0, 99, 0.1f , 1f);        
        Color color = new Color(colorIntensity, 0f, 0f, colorIntensity);     
        renderer.material.color = color;
    }

    private float Remap (float from, int fromMin, int fromMax, float toMin,  float toMax)
    {
        var fromAbs  =  from - fromMin;
        var fromMaxAbs = fromMax - fromMin;      
       
        var normal = fromAbs / fromMaxAbs;
 
        var toMaxAbs = toMax - toMin;
        var toAbs = toMaxAbs * normal;
 
        var to = toAbs + toMin;
       
        return to;
    }
}
