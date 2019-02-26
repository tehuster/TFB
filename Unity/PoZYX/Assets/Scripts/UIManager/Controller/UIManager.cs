using UnityEngine;
using Core;
using Feature.CameraTopDown;

namespace Feature.UI {
    public class UIManager : MonoBehaviour {
        public void TurnCamera() {
            EventManager.TriggerEvent(CameraTopDownEventTypes.TURN_CAMERA);
        }
    }
}