using UnityEngine;
using Utils.CameraBorder;

namespace Player
{
    public class Player : MonoBehaviour
    {
        public SideType SideType { get; private set; }

        public static Player Init(GameObject player, SideType sideType)
        {
            var currentPlayer = player.AddComponent<Player>();
            currentPlayer.SideType = sideType;
            return currentPlayer ;
        }
    }
}