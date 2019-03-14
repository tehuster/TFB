using System.Collections.Generic;
using UnityEngine;
using Core;
using Feature.Room;

public class LineDrawer : MonoBehaviour {
    [SerializeField] private LineRenderer line;
    [SerializeField] private Transform userObject;

    private Transform currentTarget;

    private void Start() {
        EventManager.StartListening(RoomEventTypes.NEW_CLOSEST_TARGET, OnNewClosestTarget);

        // Set the width of the Line Renderer
        line.SetWidth(0.1F, 0.1F);
        // Set the number of vertex fo the Line Renderer
        line.SetVertexCount(2);
    }

    private void OnDestroy() {
        EventManager.StopListening(RoomEventTypes.NEW_CLOSEST_TARGET, OnNewClosestTarget);
    }

    private void OnNewClosestTarget(object[] data){
        if (currentTarget == (Transform)data[0])
            return;

        currentTarget = (Transform)data[0];
    }

    private void Update() {
        if (userObject != null && currentTarget != null) {
            line.SetPosition(0, userObject.position);
            line.SetPosition(1, currentTarget.position);
        }
    }
}