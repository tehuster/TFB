using UnityEngine;
using Core;
using Feature.Room;

public class LookAt : MonoBehaviour {
    [SerializeField] private Transform userObject;

    private Transform currentTarget;

    private void Start() {
        EventManager.StartListening(RoomEventTypes.NEW_CLOSEST_TARGET, OnNewClosestTarget);
    }

    private void OnDestroy() {
        EventManager.StopListening(RoomEventTypes.NEW_CLOSEST_TARGET, OnNewClosestTarget);
    }

    private void OnNewClosestTarget(object[] data) {
        if (currentTarget == (Transform)data[0])
            return;
        
        currentTarget = (Transform)data[0];
    }

    private void Update() {
        if (currentTarget == null)
            return;

        Vector3 relativePos = currentTarget.position - transform.position;
        
        // the second argument, upwards, defaults to Vector3.up
        Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
        transform.rotation = rotation;
    }
}