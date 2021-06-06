using System;
using Photon.Pun;
using UnityEngine;

namespace Ball
{
    public class Ball : MonoBehaviour
    {
        private Action _onLeftRoom;
        public static Ball Init(GameObject ball)
        {
            var currentBall = ball.AddComponent<Ball>();
            return currentBall ;
        }

        private void OnEnable()
        {
            _onLeftRoom = () =>
            {
                PhotonNetwork.Destroy(gameObject);
            };
            PhotonCallbacksManager.OnAnyLeftRoomAction += _onLeftRoom;
        }

        private void OnDisable()
        {
            PhotonCallbacksManager.OnAnyLeftRoomAction -= _onLeftRoom;
        }
    }
}