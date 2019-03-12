using UnityEngine;
using Core;

namespace Feature.UI {
	/// <summary>
	/// UIManager.cs
	/// <summary>
	/// Author: Thomas Jonckheere
	/// </summary>
	public class UIManager : MonoBehaviour {
        public void TurnCamera() {
            EventManager.TriggerEvent(CameraTopDown.CameraTopDownEventTypes.TURN_CAMERA);
        }

        public void StartSession() {
            EventManager.TriggerEvent(Room.RoomEventTypes.LOAD_ROOM);
        }
    }
}