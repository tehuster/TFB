using UnityEngine;
using Core;
using UnityEngine.UI;

namespace Feature.UI {
	/// <summary>
	/// UIManager.cs
	/// <summary>
	/// Author: Thomas Jonckheere
	/// </summary>
	public class UIManager : MonoBehaviour {
		[SerializeField] private Button startSessionButton;
		[SerializeField] private Button turnCameraButton;

		private void Start() {
			SetupButtonListeners();
		}

		private void SetupButtonListeners() {
			startSessionButton.onClick.AddListener(OnStartSessionClicked);
			turnCameraButton.onClick.AddListener(OnTurnCameraClicked);
		}

		private void OnStartSessionClicked() {
			Debug.Log("New Session started!");
            EventManager.TriggerEvent(Room.RoomEventTypes.LOAD_ROOM);
        }

		private void OnTurnCameraClicked() {
			EventManager.TriggerEvent(CameraTopDown.CameraTopDownEventTypes.TURN_CAMERA);
		}
	}
}