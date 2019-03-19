using UnityEngine;
using System.Collections;
using Core;

namespace Feature.LoadingScreen {
	/// <summary>
	/// UIManager.cs
	/// <summary>
	/// Author: Thomas Jonckheere
	/// </summary>
	public class LoadingScreenController : MonoBehaviour {
		[SerializeField] private float loadingScreenPauses;

		private bool loadedRooms;

		private void Start() {
			StartCoroutine(LoadData());

			EventManager.StartListening(LoadingScreenEventTypes.LOADED_ROOMS_DATA, OnLoadedRoomsData);
		}

		private void OnDestroy() {
			EventManager.StopListening(LoadingScreenEventTypes.LOADED_ROOMS_DATA, OnLoadedRoomsData);
		}

		private IEnumerator LoadData() {
			yield return new WaitForSeconds(loadingScreenPauses);

			Debug.Log("LOADING... Rooms!");
			EventManager.TriggerEvent(LoadingScreenEventTypes.LOAD_ROOMS);

			while (!loadedRooms)
				yield return null;

			yield return new WaitForSeconds(loadingScreenPauses / 2);

			Debug.Log("LOADING... Interface!");
			EventManager.TriggerEvent(LoadingScreenEventTypes.LAUNCH_INTERFACE);
		}

		private void OnLoadedRoomsData(object[] arg0) {
			loadedRooms = true;
		}
	}
}