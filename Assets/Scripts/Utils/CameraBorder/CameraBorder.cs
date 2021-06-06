using UnityEngine;

namespace Utils.CameraBorder
{
    public class CameraBorder : MonoBehaviour
    {
        public SideType SideType { get; private set; }

        public static CameraBorder Init(GameObject border, SideType sideType)
        {
            var currentBorder = border.AddComponent<CameraBorder>();
            currentBorder.SideType = sideType;
            return currentBorder;
        }
    }
}
