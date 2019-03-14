using UnityEngine;
using Core;
using UnityEngine.UI;
using TMPro;

namespace Feature.UI {
	/// <summary>
	/// UIManager.cs
	/// <summary>
	/// Author: Thomas Jonckheere
	/// </summary>
	public class UIManager : MonoBehaviour {
		[SerializeField] private Button startSessionButton;
        [SerializeField] private Button stopSessionButton;
        [SerializeField] private TMP_InputField sessionNameInput;
        [SerializeField] private TMP_InputField sessionDisabilityInput;
        [SerializeField] private TMP_Dropdown scenarioDropDown;
        [SerializeField] private Button turnCameraButton;

		private void Start() {
			SetupButtonListeners();
		}

		private void SetupButtonListeners() {
			startSessionButton.onClick.AddListener(OnStartSessionClicked);
            stopSessionButton.onClick.AddListener(OnStopSessionClicked);
			turnCameraButton.onClick.AddListener(OnTurnCameraClicked);
		}

		private void OnStartSessionClicked() {
			Debug.Log("New Session started!");
            startSessionButton.interactable = scenarioDropDown.interactable = sessionNameInput.interactable = sessionDisabilityInput.interactable = false;
            stopSessionButton.interactable = true;

            EventManager.TriggerEvent(Room.RoomEventTypes.LOAD_ROOM, scenarioDropDown.value);
            EventManager.TriggerEvent(SessionEventTypes.START, sessionNameInput.text, sessionDisabilityInput.text, scenarioDropDown.options[scenarioDropDown.value].text);
        }

        private void OnStopSessionClicked() {
            Debug.Log("Session stopped!");
            startSessionButton.interactable = scenarioDropDown.interactable = sessionNameInput.interactable = sessionDisabilityInput.interactable = true;
            stopSessionButton.interactable = false;

            EventManager.TriggerEvent(SessionEventTypes.STOP);
        }

		private void OnTurnCameraClicked() {
			EventManager.TriggerEvent(CameraTopDown.CameraTopDownEventTypes.TURN_CAMERA);
		}
	}
}