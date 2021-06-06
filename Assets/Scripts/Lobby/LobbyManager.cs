using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using Room = Lobby.UI.Room;

namespace Lobby
{
    public class LobbyManager : MonoBehaviourPunCallbacks
    {
        private Room _roomUIPrefab;
        [SerializeField] private Transform _roomsContainer;
        [SerializeField] private Button _createRoomButton;
        [SerializeField] private Button _joinRoomButton;

        private Dictionary<RoomInfo,GameObject> _roomListEntries = new Dictionary<RoomInfo, GameObject>();
        
        public static string CurrentRoomName { get; set; }

        private void Start()
        {
            _roomUIPrefab = Resources.Load<Room>("Prefabs/UI/Lobby/Room");
            _createRoomButton.interactable = false;
            _joinRoomButton.interactable = false;
            
            _createRoomButton.onClick.AddListener(CreateRoom);
            _joinRoomButton.onClick.AddListener(JoinRoom);

            PhotonNetwork.NickName = $"Player {Random.Range(1000, 9999)}";

            PhotonNetwork.AutomaticallySyncScene = true;
            PhotonNetwork.GameVersion = "1";
            PhotonNetwork.ConnectUsingSettings();
        }

        public override void OnConnectedToMaster()
        {
            _createRoomButton.interactable = true;
            PhotonNetwork.JoinLobby();
        }

        public override void OnCreatedRoom()
        {
            _createRoomButton.interactable = false;
            _joinRoomButton.interactable = false;
        }
        
        public override void OnRoomListUpdate(List<RoomInfo> roomList)
        {
            
            foreach (var roomInfo in roomList)
            {
                if (roomInfo.RemovedFromList)
                {
                    Destroy(_roomListEntries[roomInfo]);
                }

                if (_roomListEntries.ContainsKey(roomInfo)) continue;
                var room = Room.Create(_roomUIPrefab, _roomsContainer, roomInfo,
                    active => { _joinRoomButton.interactable = active; });
                _roomListEntries.Add(roomInfo, room.gameObject);
            }
        }

        public override void OnJoinedRoom()
        {
            PhotonNetwork.LoadLevel("Multi Player Game");
        }

        private void CreateRoom()
        {
            CurrentRoomName = $"Room {Random.Range(1000, 9999)}";
            PhotonNetwork.CreateRoom(CurrentRoomName, new RoomOptions {MaxPlayers = 2});
        }

        private void JoinRoom()
        {
            PhotonNetwork.JoinRoom(CurrentRoomName);
        }

    }
}