using UnityEngine;
using Utils.CameraBorder;

namespace StartUps
{
    public class GameStartUp : MonoBehaviour
    {
        [SerializeField] private Player.Player _topPlayer;
        [SerializeField] private Player.Player _buttomPlayer;

        private void Awake()
        {
            Player.Player.Init(_topPlayer.gameObject, SideType.Top);
            Player.Player.Init(_buttomPlayer.gameObject, SideType.Buttom);
        }
    }
}