using System.Collections.Generic;
using UnityEngine;
using Core;
using Feature.Room;

public class SensorDistanceIntensity : MonoBehaviour {
    [SerializeField] private Transform user;
	[SerializeField] private FloatVariable distance;
	[Space]
	[Header("Motors Limitations")]
	[SerializeField] private int maxDistance;
	[Space]
	[Range(0, 100)]
    [SerializeField] private int minIntensity;
	[Range(0, 100)]
    [SerializeField] private int maxIntensity;

    private List<Transform> targets = new List<Transform>();
    private Transform currentTarget;
	private Transform oldTarget;
	private float oldDistance = -1f;

    private void Start() {
        EventManager.StartListening(RoomEventTypes.DELETE_TARGETS, OnDeleteTargets);
        EventManager.StartListening(RoomEventTypes.NEW_TARGET, OnNewTarget);
    }

    private void OnDestroy() {
        EventManager.StopListening(RoomEventTypes.DELETE_TARGETS, OnDeleteTargets);
        EventManager.StopListening(RoomEventTypes.NEW_TARGET, OnNewTarget);
    }

    private void OnDeleteTargets(object[] arg0) {
		oldDistance = -1f;
        targets = new List<Transform>();
    }

    private void OnNewTarget(object[] data) {
        targets.Add((Transform)data[0]);
    }

    private void Update() {
		SetClosestTarget();

        if (currentTarget == null)
            return;

        float d = Vector3.Distance(user.position, currentTarget.transform.position);
        d = Mathf.Clamp(d, 0, maxDistance);
        distance.Value = Remap(d, 0, maxDistance, maxIntensity, minIntensity);
    }

    private void SetClosestTarget(){
        for (int i = 0; i < targets.Count; i++) {
            float d = Vector3.Distance(user.position, targets[i].transform.position);

			if (d < oldDistance || oldDistance <= -1f) {
				oldDistance = d;
				currentTarget = targets[i];
				if (currentTarget != oldTarget)
					EventManager.TriggerEvent(RoomEventTypes.NEW_CLOSEST_TARGET, currentTarget);

				oldTarget = currentTarget;
			}

			if (targets[i] == currentTarget)
				oldDistance = d;
        }
    }

    private float Remap(float from, int fromMin, int fromMax, int toMin, int toMax) {
        var fromAbs = from - fromMin;
        var fromMaxAbs = fromMax - fromMin;

        var normal = fromAbs / fromMaxAbs;

        var toMaxAbs = toMax - toMin;
        var toAbs = toMaxAbs * normal;

        var to = toAbs + toMin;

        return to;
    }
}