using UnityEngine;
using Core;
using Feature.Room;
using System.Collections.Generic;

namespace Feature.CameraTopDown {
	/// <summary>
	/// CameraTopDownController.cs
	/// <summary>
	/// Author: Thomas Jonckheere
	/// </summary>
	public class CameraTopDownController : MonoBehaviour {
		[SerializeField] private Camera cam;
		[Header("Move Settings")]
		[SerializeField] private float dampSpeed = 0.5f;

        List<Vector3> anchorPositions = new List<Vector3>();
		private Vector3 dampVelocity = new Vector3(1f, 1f, 1f);
		private Vector3 newPosition;
		private float anchorDistance = 75f;

		private void Start() {
			newPosition = transform.position;

			EventManager.StartListening(RoomEventTypes.LOADED_NEW_ROOM, OnLoadedNewRoom);
            EventManager.StartListening(CameraTopDownEventTypes.TURN_CAMERA, TurnCamera);
		}

		private void OnDestroy() {
			EventManager.StopListening(RoomEventTypes.LOADED_NEW_ROOM, OnLoadedNewRoom);
            EventManager.StopListening(CameraTopDownEventTypes.TURN_CAMERA, TurnCamera);
		}

		private void LateUpdate() {
			MoveCamera();
			ZoomCamera();
		}

		private void MoveCamera() {
			newPosition.y = transform.position.y;
			transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref dampVelocity, dampSpeed);
		}

		private void ZoomCamera() {
			cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, anchorDistance / 2f, Time.deltaTime);
		}

		private void OnLoadedNewRoom(object[] data) {
			Transform parent = (Transform)data[0];

			foreach (Transform child in parent.GetComponentInChildren<Transform>())
				anchorPositions.Add(child.position);

			Bounds anchorBounds = new Bounds(anchorPositions[0], Vector3.zero);

			for (int i = 0; i < anchorPositions.Count; i++)
				anchorBounds.Encapsulate(anchorPositions[i]);

			newPosition = anchorBounds.center;
			anchorDistance = (anchorBounds.size.x > anchorBounds.size.z) ? anchorBounds.size.x : anchorBounds.size.z;
		}

        private void TurnCamera(object[] arg0) {
            transform.Rotate(new Vector3(0f, -90f, 0f), Space.World);
        }
	}
}