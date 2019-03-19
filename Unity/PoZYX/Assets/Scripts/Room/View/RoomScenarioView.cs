using System.Collections.Generic;
using UnityEngine;
using Core;
using TMPro;
using Feature.LoadingScreen;

namespace Feature.Room {
    public class RoomScenarioView : MonoBehaviour {
        [SerializeField] private TMP_Dropdown scenarioDropDown;

        private List<string> roomNames = new List<string>();

        private void Start() {
            EventManager.StartListening(RoomEventTypes.SET_ROOM_NAMES, SetRoomNames);

            scenarioDropDown.onValueChanged.AddListener(OnScenarioChanged);
        }

        private void OnDestroy() {
            EventManager.StopListening(RoomEventTypes.SET_ROOM_NAMES, SetRoomNames);
        }

        private void SetRoomNames(object[] data) {
            scenarioDropDown.options.RemoveRange(0, scenarioDropDown.options.Count);

            roomNames = (List<string>)data[0];

            for (int i = 0; i < roomNames.Count; i++) {
                scenarioDropDown.options.Add(new TMP_Dropdown.OptionData(roomNames[i]));
            }

			EventManager.TriggerEvent(LoadingScreenEventTypes.LOADED_ROOMS_DATA);
        }

        private void OnScenarioChanged(int id){
            EventManager.TriggerEvent(RoomEventTypes.UPDATE_CURRENT_ROOM, id);
        }
    }
}