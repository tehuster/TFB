using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

namespace Core {
	public class EventManager : MonoBehaviour {
		public bool debugOn;

		private Dictionary<string, GameEvent> eventDictionary;

		private static EventManager eventManager;

		public static EventManager instance {
			get {
				if (!eventManager) {
					eventManager = FindObjectOfType(typeof(EventManager)) as EventManager;

					if (!eventManager) {
						Debug.LogError("There needs to be one active EventManager script on a GameObject in your scene.");
					} else {
						eventManager.Init();
					}
				}

				return eventManager;
			}
		} //!< Singleton for accessing the EventManager class

		///
		/// Initialization is being called when a new instance is created of the EventManager class.
		///
		void Init() {
			if (eventDictionary == null) {
				eventDictionary = new Dictionary<string, GameEvent>();
				DontDestroyOnLoad(gameObject);
			}
		}

		///
		/// Use this function to start listening to an event. The method to be called is stored within this class untill the function StopListening is being called.
		///
		public static void StartListening(string eventName, UnityAction<object[]> listener) {
			GameEvent thisEvent = null;
			if (instance.eventDictionary.TryGetValue(eventName, out thisEvent)) {
				thisEvent.AddListener(listener);
			} else {
				thisEvent = new GameEvent();
				thisEvent.AddListener(listener);
				instance.eventDictionary.Add(eventName, thisEvent);
			}
		}

		///
		/// Use this function to remove a listener from a function with a specific event.
		///
		public static void StopListening(string eventName, UnityAction<object[]> listener) {
			if (eventManager == null) return;
			GameEvent thisEvent = null;
			if (instance.eventDictionary.TryGetValue(eventName, out thisEvent)) {
				thisEvent.RemoveListener(listener);
			}
		}

		///
		/// When called, this function will execute all functions in the class's dictionary with the passed event name. The object passed to this function will be given to all listening functions.
		///
		public static void TriggerEvent(string eventName, params object[] arguments) {
			GameEvent thisEvent = null;

			if (instance.debugOn)
				Debug.Log(eventName + "\t" + arguments);

			if (instance.eventDictionary.TryGetValue(eventName, out thisEvent)) {
				thisEvent.Invoke(arguments);
			}
		}

		///
		/// Overload method to trigger an event without an argument.
		///
		public static void TriggerEvent(string eventName) {
			TriggerEvent(eventName, null);
		}
	}
}