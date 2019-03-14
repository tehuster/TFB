using UnityEditor;
using UnityEngine;
using Core;
using System.Collections.Generic;
using System.IO;

namespace Feature.Room {
	/// <summary>
	/// RoomController.cs
	/// <summary>
	/// Author: Thomas Jonckheere
	/// </summary>
	public class RoomController : MonoBehaviour {
		[SerializeField] private bool debug;
        [Space]
		[SerializeField] private RoomDataModel roomData;
        [Header("Anchors")]
        [SerializeField] private GameObject anchorPrefab;
		[SerializeField] private Transform anchorParent;
        [Header("Targets")]
        [SerializeField] private GameObject targetPrefab;
        [SerializeField] private Transform targetParent;

        private string[] roomIDs;

		private void Start() {
            GetRooms();

            EventManager.StartListening(RoomEventTypes.LOAD_ROOM, OnLoadRoom);
            EventManager.StartListening(RoomEventTypes.UPDATE_CURRENT_ROOM, OnRoomChange);
		}

		private void OnDestroy() {
			EventManager.StopListening(RoomEventTypes.LOAD_ROOM, OnLoadRoom);
            EventManager.StopListening(RoomEventTypes.UPDATE_CURRENT_ROOM, OnRoomChange);
		}

        private void GetRooms(){
            roomIDs = AssetDatabase.FindAssets("t:object", new[] { "Assets/Scripts/Room/Data" });

            List<string> roomNames = new List<string>();

            for (int i = 0; i < roomIDs.Length; i++)
                roomNames.Add(Path.GetFileName(AssetDatabase.GUIDToAssetPath(roomIDs[i])));

            EventManager.TriggerEvent(RoomEventTypes.SET_ROOM_NAMES, roomNames);
        }

        private void OnGUI() {
			if (!debug)
				return;

			GUILayout.BeginVertical();

			if (GUILayout.Button("Load Room"))
				EventManager.TriggerEvent(RoomEventTypes.LOAD_ROOM);

			GUILayout.EndVertical();
		}

        private void Update() {
            if (debug && Input.GetKeyDown(KeyCode.L))
                EventManager.TriggerEvent(RoomEventTypes.LOAD_ROOM);
        }

        private void OnRoomChange(object[] data){
            int id = (int)data[0];

            roomData = (RoomDataModel)AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(roomIDs[id]), typeof(RoomDataModel));

        }

        private void OnLoadRoom(object[] data) {
			if (roomData == null || anchorPrefab == null || anchorParent == null) {
				Debug.LogError("Cannot load new room. Missing references!");
				return;
			}

            SpawnAnchors();
            SpawnTargets();

			Debug.Log("Succesfully loaded the new room: '" + roomData.name + "'.");

			EventManager.TriggerEvent(RoomEventTypes.LOADED_NEW_ROOM, anchorParent);
		}
        private void SpawnAnchors(){
            foreach (Transform oldAnchor in anchorParent.GetComponentInChildren<Transform>())
                Destroy(oldAnchor.gameObject);

            if (roomData.targets.Length == 0) {
                Debug.LogError("No Anchors detected! Check Room Data Model.");
                return;
            }

            for (int i = 0; i < roomData.anchors.Length; i++) {
                GameObject anchor = Instantiate(anchorPrefab, new Vector3(roomData.anchors[i].x, 2, roomData.anchors[i].y), Quaternion.identity);
                anchor.transform.parent = anchorParent;
                anchor.name = "Anchor_" + i;
            }
        }

        private void SpawnTargets() {
            foreach (Transform oldTarget in targetParent.GetComponentInChildren<Transform>())
                Destroy(oldTarget.gameObject);

            if (roomData.targets.Length == 0) {
                Debug.LogError("No Targets detected! Check Room Data Model.");
                return;
            }

            for (int i = 0; i < roomData.targets.Length; i++) {
                GameObject newTarget = Instantiate(targetPrefab, new Vector3(roomData.targets[i].x, 2, roomData.targets[i].y), Quaternion.identity);
                newTarget.transform.parent = targetParent;
                newTarget.name = "Target_" + i;

                EventManager.TriggerEvent(RoomEventTypes.NEW_TARGET, newTarget.transform);
            }
        }
	}
}