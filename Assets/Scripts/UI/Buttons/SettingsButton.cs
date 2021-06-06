using ScreenSystem;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Buttons
{
    public class SettingsButton : MonoBehaviour
    {
        private void Start()
        {
            GetComponent<Button>().onClick.AddListener(() => ScreenSwitcher.Instance.ShowPopup(PopupType.Settings));
        }
    }
}