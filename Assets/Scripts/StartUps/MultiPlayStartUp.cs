using Photon.Pun;
using UnityEngine;
using Utils.CameraBorder;

namespace StartUps
{
    public class MultiPlayStartUp : MonoBehaviour
    {
        [SerializeField] private CameraBordersBuilder _cameraBordersBuilder;
        private void Awake()
        {
            _cameraBordersBuilder.gameObject.SetActive(false);
            new GameObject(nameof(PhotonEventHandler), typeof(PhotonEventHandler));
            if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
            {
                _cameraBordersBuilder.gameObject.SetActive(true);
            }
        }
    }
}
