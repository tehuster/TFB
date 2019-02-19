using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorDistanceIntensity : MonoBehaviour {

	public GameObject User;
	public GameObject Target;
	public FloatVariable distance;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		float d = Vector3.Distance (User.transform.position, Target.transform.position);	
		d = Mathf.Clamp(d, 0, 75);
		distance.Value = Remap(d, 0, 75, 100, 0);
	}

	 private float Remap (float from, int fromMin, int fromMax, int toMin,  int toMax)
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
