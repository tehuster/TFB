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
		d = Mathf.Clamp(d, 0, 50);
		distance.Value = Remap(d, 0, 50, 35, 0);
	}

	private float Remap (float value, float from1, float to1, float from2, float to2) {
    	return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
	}
}
