using System;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Lobby.UI
{
    public class Room : MonoBehaviour
    {
        private Action<bool> _onSelection { get; set; }
        private RoomInfo _roomInfo { get; set; }
        [SerializeField] private TextMeshProUGUI _nameText;
        private ToggleGroup _toggleGroup;
        private Toggle _toggle;

        public static Room Create(Room room, Transform parent, RoomInfo roomInfo, Action<bool> onSelection)
        {
            var tempRoom = Instantiate(room, parent);
            tempRoom._roomInfo = roomInfo;
            tempRoom._onSelection = onSelection;
            return tempRoom;
        }

        private void Start()
        {
            _nameText.text = _roomInfo.Name;
            _toggle = GetComponent<Toggle>();
            _toggleGroup = transform.parent.GetComponent<ToggleGroup>();

            _toggle.onValueChanged.AddListener(OnSelected);
            _toggle.group = _toggleGroup;
            _toggle.isOn = true;
        }

        private void OnSelected(bool active)
        {
            _onSelection?.Invoke(active);
            if(active == false) return;
            LobbyManager.CurrentRoomName = _roomInfo.Name;
        }

        private void OnDestroy()
        {
            if(_toggle.isOn) _onSelection?.Invoke(false);
        }
    }
}