using UnityEngine;
using Core;
using Feature.Room;
using System.Collections.Generic;
using Cinemachine;

namespace Feature.CameraTopDown {
	/// <summary>
	/// CameraTopDownController.cs
	/// <summary>
	/// Author: Thomas Jonckheere
	/// </summary>
	public class CameraTopDownController : MonoBehaviour {
		//[SerializeField] private Camera cam;
		//[Header("Move Settings")]
		//[SerializeField] private float dampSpeed = 0.5f;
		//[Range(-50,50)]
		//[SerializeField] private float zoomOffset;
		[SerializeField] private Transform virtualCamera;
		[SerializeField] private CinemachineTargetGroup targetGroup;

		private void Start() {
			//newPosition = transform.position;

			EventManager.StartListening(RoomEventTypes.LOADED_NEW_ROOM, OnLoadedNewRoom);
            EventManager.StartListening(CameraTopDownEventTypes.TURN_CAMERA, TurnCamera);
            EventManager.StartListening(SessionEventTypes.STOP, OnSessionStopped);
		}

		private void OnDestroy() {
			EventManager.StopListening(RoomEventTypes.LOADED_NEW_ROOM, OnLoadedNewRoom);
            EventManager.StopListening(CameraTopDownEventTypes.TURN_CAMERA, TurnCamera);
            EventManager.StopListening(SessionEventTypes.STOP, OnSessionStopped);
		}

		//private void LateUpdate() {
		//          if (!roomLoaded)
		//              return;

		//	MoveCamera();
		//	ZoomCamera();
		//}

		//private void MoveCamera() {
		//	newPosition.y = transform.position.y;
		//	transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref dampVelocity, dampSpeed);
		//}

		//private void ZoomCamera() {
		//	cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, (anchorDistance / 2f) + zoomOffset, Time.deltaTime);
		//}

		private void OnLoadedNewRoom(object[] data) {
			Transform parent = (Transform)data[0];

			List<Vector3> anchorPositions = new List<Vector3>();

			foreach (Transform child in parent.GetComponentInChildren<Transform>())
				anchorPositions.Add(child.position);

			//	Bounds anchorBounds = new Bounds(anchorPositions[0], Vector3.zero);

			//	for (int i = 0; i < anchorPositions.Count; i++)
			//		anchorBounds.Encapsulate(anchorPositions[i]);

			//	newPosition = anchorBounds.center;
			//	anchorDistance = (anchorBounds.size.x > anchorBounds.size.z) ? anchorBounds.size.x : anchorBounds.size.z;
			//          roomLoaded = true;
		}

		private void OnSessionStopped(object[] arg0) {
			//roomLoaded = false;
		}

        private void TurnCamera(object[] arg0) {
			//transform.Rotate(new Vector3(0f, -90f, 0f), Space.World);
		}
	}
}