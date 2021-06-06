using Photon.Pun;
using UnityEngine;

namespace Ball
{
    public class BallGenerator : MonoBehaviourPunCallbacks
    {
        private void Start()
        {
            if (PhotonNetwork.CurrentRoom.PlayerCount != 2) return;

            var ball = PhotonNetwork.Instantiate("Prefabs/Game/Ball", Vector3.zero, Quaternion.identity);
            Ball.Init(ball);
        }
    }
}