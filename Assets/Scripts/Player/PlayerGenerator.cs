using System;
using Photon.Pun;
using UnityEngine;
using Utils.CameraBorder;

namespace Player
{
    public class PlayerGenerator : MonoBehaviourPun
    {
        private Action _onAnyPlayerLeftRoom;
        public static Player CurrentPlayer { get; private set; }
        private SideType PlayerSideType { get;} = SideType.Buttom;
        private SideType EnemySideType { get;} = SideType.Top;

        private void Start()
        {
            var position = Vector3.down * 4.6f;
            var sideType = PlayerSideType;

            _onAnyPlayerLeftRoom += () =>
            {
                CurrentPlayer.transform.position = Vector3.down * 4.6f;
                Camera.main.transform.rotation = Quaternion.identity;
            };

            PhotonCallbacksManager.OnAnyLeftRoomAction += _onAnyPlayerLeftRoom;

            if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
            {
                position = Vector3.up * 4.6f;
                Camera.main.transform.rotation = Quaternion.Euler(Vector3.forward * 180);
                sideType = EnemySideType;
            }
            
            var player = PhotonNetwork.Instantiate("Prefabs/Game/Player", position, Quaternion.identity);
            CurrentPlayer = Player.Init(player, sideType);
        }

        private void OnDisable()
        {
            PhotonCallbacksManager.OnAnyLeftRoomAction -= _onAnyPlayerLeftRoom;
        }
    }
}