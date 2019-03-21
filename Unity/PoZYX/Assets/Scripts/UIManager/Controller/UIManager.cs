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
        [SerializeField] private GameObject areYouSurePanel;
        [SerializeField] private Button areYouSureYesButton;
        [SerializeField] private Button areYouSureNoButton;
		[Header("Session Black Panel")]
		[SerializeField] private float sessionBlackPanelFadeSpeed;
		[SerializeField] private Image sessionBlackPanel;
		[SerializeField] private float sessionBlackPanelAlpha;
        [Header("Session Comments")]
        [SerializeField] private TMP_InputField sessionCommentField;
        [Header("Scenario Start")]
        [SerializeField] private GameObject sessionStartPanel;
        [SerializeField] private TextMeshProUGUI sessionStartText;
        [SerializeField] private float showStartPanelTime;
		[Header("Camera Settings")]
        [SerializeField] private Button turnCameraButton;
		[Header("Loading Screen")]
		[SerializeField] private Image loadingScreenPanel;
		[SerializeField] private float loadingScreenFadeSpeed;
        [Header("Motors Sliders")]
        [SerializeField] private Slider intensityMinSlider;
        [SerializeField] private Slider intensityMaxSlider;
        [Space]
        [SerializeField] private Toggle motorsToggle;
        [Space]
        [SerializeField] private Slider distanceSlider;
        [SerializeField] private TextMeshProUGUI distanceSliderText;

		private Coroutine blackPanelHideCoroutine;
		private Coroutine blackPanelShowCoroutine;

        private bool commentsAreSelected;

		private void Awake() {
            loadingScreenPanel.gameObject.SetActive(true);
            loadingScreenPanel.enabled = true;
			sessionBlackPanel.enabled = true;
		}

		private void Start() {
            EventManager.StartListening(LoadingScreenEventTypes.LAUNCH_INTERFACE, OnLaunchInterfaceRequest);

            SetupListeners();
		}

		private void OnDestroy() {
			EventManager.StopListening(LoadingScreenEventTypes.LAUNCH_INTERFACE, OnLaunchInterfaceRequest);
		}

		private void OnLaunchInterfaceRequest(object[] arg0) {
			StartCoroutine(HidePanel(loadingScreenPanel, loadingScreenFadeSpeed));
		}

		private void SetupListeners() {
			startSessionButton.onClick.AddListener(OnStartSessionClicked);
            stopSessionButton.onClick.AddListener(OnStopSessionClicked);
            areYouSureYesButton.onClick.AddListener(OnAreYouSureYesClicked);
            areYouSureNoButton.onClick.AddListener(OnAreYouSureNoClicked);
            //turnCameraButton.onClick.AddListener(OnTurnCameraClicked);

            motorsToggle.onValueChanged.AddListener(OnToggleMotorsChanged);
            intensityMinSlider.onValueChanged.AddListener(OnMinIntensitySliderChanged);
            intensityMaxSlider.onValueChanged.AddListener(OnMaxIntensitySliderChanged);
            distanceSlider.onValueChanged.AddListener(OnDistanceSliderChanged);

            sessionCommentField.onSubmit.AddListener(OnSessionCommentFieldSubmitted);
		}

		private void OnStartSessionClicked() {
            EventManager.TriggerEvent(Room.RoomEventTypes.LOAD_ROOM, scenarioDropDown.value);
            EventManager.TriggerEvent(SessionEventTypes.START, sessionNameInput.text, sessionDisabilityInput.text, scenarioDropDown.options[scenarioDropDown.value].text);
            EventManager.TriggerEvent(Networking.NetworkingEventTypes.TOGGLE_MOTORS, true);

            startSessionButton.interactable = scenarioDropDown.interactable = sessionNameInput.interactable = sessionDisabilityInput.interactable = false;
            stopSessionButton.interactable = true;
            sessionStartText.text = (sessionNameInput.text == "") ? sessionStartText.text : sessionNameInput.text;

            if (blackPanelHideCoroutine != null)
                StopCoroutine(blackPanelShowCoroutine);

            blackPanelHideCoroutine = StartCoroutine(HidePanel(sessionBlackPanel, sessionBlackPanelFadeSpeed));
            StartCoroutine(ShowStartSessionPanel());
        }

        private void OnStopSessionClicked() {
            areYouSurePanel.SetActive(true);
        }

		//private void OnTurnCameraClicked() {
		//	EventManager.TriggerEvent(CameraTopDown.CameraTopDownEventTypes.TURN_CAMERA);
		//}

        private void OnSessionCommentFieldSubmitted(string value) {
            if (sessionCommentField.text == "")
                return;
            
            sessionCommentField.text = "";
            EventManager.TriggerEvent(SessionEventTypes.ADD_COMMENT, value);
        }

        private void OnAreYouSureYesClicked() {
            startSessionButton.interactable = scenarioDropDown.interactable = sessionNameInput.interactable = sessionDisabilityInput.interactable = true;
            stopSessionButton.interactable = false;
            areYouSurePanel.SetActive(false);

            EventManager.TriggerEvent(SessionEventTypes.STOP);
            EventManager.TriggerEvent(Networking.NetworkingEventTypes.TOGGLE_MOTORS, false);

            if (blackPanelHideCoroutine != null)
                StopCoroutine(blackPanelHideCoroutine);

            blackPanelShowCoroutine = StartCoroutine(ShowPanel(sessionBlackPanel, sessionBlackPanelFadeSpeed, sessionBlackPanelAlpha));
        }

        private void OnAreYouSureNoClicked() {
            areYouSurePanel.SetActive(false);
        }

        private void OnMinIntensitySliderChanged(float value) {
            EventManager.TriggerEvent(SessionEventTypes.UPDATE_MIN_INTENSITY, (int)value);
        }

        private void OnMaxIntensitySliderChanged(float value) {
            EventManager.TriggerEvent(SessionEventTypes.UPDATE_MAX_INTENSITY, 100 - (int)value);
        }

        private void OnDistanceSliderChanged(float value) {
            distanceSliderText.text = value.ToString();

            EventManager.TriggerEvent(SessionEventTypes.UPDATE_MAX_DISTANCE, (int)value);
        }

        private void OnToggleMotorsChanged(bool value) {
            EventManager.TriggerEvent(Networking.NetworkingEventTypes.TOGGLE_MOTORS, value);
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

        private IEnumerator ShowStartSessionPanel() {
            sessionStartText.gameObject.SetActive(true);
            sessionStartPanel.SetActive(true);

            yield return new WaitForSeconds(showStartPanelTime);

            sessionStartText.gameObject.SetActive(false);
            sessionStartPanel.SetActive(false);
        }
	}
}