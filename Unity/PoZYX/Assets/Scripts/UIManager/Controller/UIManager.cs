using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Core;
using TMPro;
using Feature.LoadingScreen;

namespace Feature.UI {
	/// <summary>
	/// UIManager.cs
	/// <summary>
	/// Author: Thomas Jonckheere
	/// </summary>
	public class UIManager : MonoBehaviour {
		[Header("Session Settings")]
		[SerializeField] private Button startSessionButton;
        [SerializeField] private Button stopSessionButton;
        [SerializeField] private TMP_InputField sessionNameInput;
        [SerializeField] private TMP_InputField sessionDisabilityInput;
        [SerializeField] private TMP_Dropdown scenarioDropDown;
		[Header("Session Black Panel")]
		[SerializeField] private float sessionBlackPanelFadeSpeed;
		[SerializeField] private Image sessionBlackPanel;
		[SerializeField] private float sessionBlackPanelAlpha;
		[Header("Camera Settings")]
        [SerializeField] private Button turnCameraButton;
		[Header("Loading Screen")]
		[SerializeField] private Image loadingScreenPanel;
		[SerializeField] private float loadingScreenFadeSpeed;

		private Coroutine blackPanelHideCoroutine;
		private Coroutine blackPanelShowCoroutine;

		private void Awake() {
			loadingScreenPanel.enabled = true;
			sessionBlackPanel.enabled = true;
		}

		private void Start() {
			SetupButtonListeners();

			EventManager.StartListening(LoadingScreenEventTypes.LAUNCH_INTERFACE, OnLaunchInterfaceRequest);
		}

		private void OnDestroy() {
			EventManager.StopListening(LoadingScreenEventTypes.LAUNCH_INTERFACE, OnLaunchInterfaceRequest);
		}

		private void OnLaunchInterfaceRequest(object[] arg0) {
			StartCoroutine(HidePanel(loadingScreenPanel, loadingScreenFadeSpeed));
		}

		private void SetupButtonListeners() {
			startSessionButton.onClick.AddListener(OnStartSessionClicked);
            stopSessionButton.onClick.AddListener(OnStopSessionClicked);
			turnCameraButton.onClick.AddListener(OnTurnCameraClicked);
		}

		private void OnStartSessionClicked() {
            startSessionButton.interactable = scenarioDropDown.interactable = sessionNameInput.interactable = sessionDisabilityInput.interactable = false;
            stopSessionButton.interactable = true;

            EventManager.TriggerEvent(Room.RoomEventTypes.LOAD_ROOM, scenarioDropDown.value);
            EventManager.TriggerEvent(SessionEventTypes.START, sessionNameInput.text, sessionDisabilityInput.text, scenarioDropDown.options[scenarioDropDown.value].text);

			if (blackPanelHideCoroutine != null)
				StopCoroutine(blackPanelShowCoroutine);

			blackPanelHideCoroutine = StartCoroutine(HidePanel(sessionBlackPanel, sessionBlackPanelFadeSpeed));
        }

        private void OnStopSessionClicked() {
            startSessionButton.interactable = scenarioDropDown.interactable = sessionNameInput.interactable = sessionDisabilityInput.interactable = true;
            stopSessionButton.interactable = false;

			EventManager.TriggerEvent(SessionEventTypes.STOP);

			if (blackPanelHideCoroutine != null)
				StopCoroutine(blackPanelHideCoroutine);

			blackPanelShowCoroutine = StartCoroutine(ShowPanel(sessionBlackPanel, sessionBlackPanelFadeSpeed, sessionBlackPanelAlpha));
        }

		private void OnTurnCameraClicked() {
			EventManager.TriggerEvent(CameraTopDown.CameraTopDownEventTypes.TURN_CAMERA);
		}

		private IEnumerator HidePanel(Image panel, float fadeSpeed) {
			panel.raycastTarget = false;

			float fadeTime = 5f;

			while (fadeTime >= 0f) {
				panel.color = new Color(0, 0, 0, Mathf.Lerp(panel.color.a, 0f, fadeSpeed * Time.deltaTime));
				fadeTime -= Time.deltaTime;
				yield return new WaitForEndOfFrame();
			}
		}

		private IEnumerator ShowPanel(Image panel, float fadeSpeed, float alpha) {
			panel.raycastTarget = true;

			float fadeTime = 5f;

			while (fadeTime >= 0f) {
				panel.color = new Color(0, 0, 0, Mathf.Lerp(panel.color.a, alpha, fadeSpeed * Time.deltaTime));
				fadeTime -= Time.deltaTime;
				yield return new WaitForEndOfFrame();
			}
		}
	}
}