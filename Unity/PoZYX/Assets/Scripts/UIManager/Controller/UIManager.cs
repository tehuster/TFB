using UnityEngine;
using Core;
using Feature.CameraTopDown;

namespace Feature.UI {
	/// <summary>
	/// UIManager.cs
	/// <summary>
	/// Author: Thomas Jonckheere
	/// </summary>
	public class UIManager : MonoBehaviour {
        public void TurnCamera() {
            EventManager.TriggerEvent(CameraTopDownEventTypes.TURN_CAMERA);
        }
    }
}