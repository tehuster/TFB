using UnityEngine;
using Core;

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
		[SerializeField] private GameObject anchorPrefab;
		[SerializeField] private Transform anchorParent;

		private void Start() {
			EventManager.StartListening(RoomEventTypes.LOAD_ROOM, OnLoadRoom);
		}

		private void OnDestroy() {
			EventManager.StopListening(RoomEventTypes.LOAD_ROOM, OnLoadRoom);
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

        private void OnLoadRoom(object[] arg0) {
			if (roomData == null || anchorPrefab == null || anchorParent == null) {
				Debug.LogError("Cannot load new room. Missing references!");
				return;
			}

			foreach (Transform oldAnchor in anchorParent.GetComponentInChildren<Transform>())
				Destroy(oldAnchor.gameObject);

			for (int i = 0; i < roomData.anchors.Length; i++) {
				GameObject anchor = Instantiate(anchorPrefab, new Vector3(roomData.anchors[i].x, 2, roomData.anchors[i].y), Quaternion.identity);
				anchor.transform.parent = anchorParent;
				anchor.name = "Anchor_" + i;
			}

			Debug.Log("Succesfully loaded the new room: '" + roomData.name + "'.");

			EventManager.TriggerEvent(RoomEventTypes.LOADED_NEW_ROOM, anchorParent);
		}
	}
}