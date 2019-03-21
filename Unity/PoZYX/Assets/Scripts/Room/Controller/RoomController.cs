using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using Core;
using Feature.LoadingScreen;

namespace Feature.Room {
	/// <summary>
	/// RoomController.cs
	/// <summary>
	/// Author: Thomas Jonckheere
	/// </summary>
	public class RoomController : MonoBehaviour {
		[SerializeField] private RoomDataModel currentRoomData;
        [Header("Anchors")]
        [SerializeField] private GameObject anchorPrefab;
		[SerializeField] private Transform anchorParent;
        [Header("Targets")]
        [SerializeField] private GameObject targetPrefab;
        [SerializeField] private Transform targetParent;

		private RoomDataModel[] rooms;
		
        private string[] roomIDs;

		private void Start() {
			EventManager.StartListening(LoadingScreenEventTypes.LOAD_ROOMS, OnLoadRoomsRequest);
            EventManager.StartListening(RoomEventTypes.LOAD_ROOM, OnLoadRoom);
            EventManager.StartListening(RoomEventTypes.UPDATE_CURRENT_ROOM, OnRoomChange);

			rooms = Resources.LoadAll<RoomDataModel>("Scenarios/");
		}

		private void OnDestroy() {
			EventManager.StopListening(LoadingScreenEventTypes.LOAD_ROOMS, OnLoadRoomsRequest);
			EventManager.StopListening(RoomEventTypes.LOAD_ROOM, OnLoadRoom);
            EventManager.StopListening(RoomEventTypes.UPDATE_CURRENT_ROOM, OnRoomChange);
		}

		private void OnLoadRoomsRequest(object[] arg0) {
			List<string> roomNames = new List<string>();

			for (int i = 0; i < rooms.Length; i++)
				roomNames.Add(rooms[i].name);
			
			EventManager.TriggerEvent(RoomEventTypes.SET_ROOM_NAMES, roomNames);
		}

        private void OnRoomChange(object[] data){
            int id = (int)data[0];

            currentRoomData = rooms[id];
        }

        private void OnLoadRoom(object[] data) {
			if (currentRoomData == null || anchorPrefab == null || anchorParent == null || targetPrefab == null || targetParent == null) {
				Debug.LogError("Cannot load new room. Missing references!");
				return;
			}

            AdjustAnchors();

			if (SpawnTargets())
				EventManager.TriggerEvent(RoomEventTypes.LOADED_NEW_ROOM, anchorParent);
		}
        private void AdjustAnchors(){
			int id = 0;

			foreach (Transform anchor in anchorParent.GetComponentInChildren<Transform>()) {
				anchor.transform.position = new Vector3(currentRoomData.anchors[id].x, 2, currentRoomData.anchors[id].y);
				id++;
			}
		}

        private bool SpawnTargets() {
            foreach (Transform oldTarget in targetParent.GetComponentInChildren<Transform>())
                Destroy(oldTarget.gameObject);

			EventManager.TriggerEvent(RoomEventTypes.DELETE_TARGETS);

			if (currentRoomData.targets.Length == 0) {
				Debug.LogError("No Targets assigned to the room!");
				return false;
			}

            for (int i = 0; i < currentRoomData.targets.Length; i++) {
                GameObject newTarget = Instantiate(targetPrefab, new Vector3(currentRoomData.targets[i].x, 2, currentRoomData.targets[i].y), Quaternion.identity);
                newTarget.transform.parent = targetParent;
                newTarget.name = "Target_" + i;

                EventManager.TriggerEvent(RoomEventTypes.NEW_TARGET, newTarget.transform);
            }

			return true;
        }
	}
}