using Photon.Pun;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(PhotonView))]
    public class MultiPlayPlayerMovement : PlayerMovement
    {
        private void Awake()
        {
            enabled = GetComponent<PhotonView>().IsMine;
        }
    }
}