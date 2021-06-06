using GameModeSystem;
using Player.Data;
using ScreenSystem;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Screen = ScreenSystem.Screen;

namespace UI.Screens
{
    public class SettingsPopup : Popup
    {
        [SerializeField] private Slider _hsvSlider;
        [SerializeField] private Image _ballImage;
        [SerializeField] private Button _backButton;
        [SerializeField] private Button _backToMenu;
        protected override PopupType PopupType => PopupType.Settings;

        protected override void OnShow()
        {
            Time.timeScale = 0;
        }

        protected override void OnHide()
        {
            Time.timeScale = 1;
        }

        private void Start()
        { 
            Color.RGBToHSV(BallColor.Color, out var hue, out _, out _);
            _hsvSlider.value = hue;
            _ballImage.color = Color.HSVToRGB(hue, 1, 1);
            
            _hsvSlider.onValueChanged.AddListener(SetBallColor);

            if (GameMode.Mode == GameModeType.SinglePlay)
            {
                _backButton.onClick.AddListener(() => ScreenSwitcher.Instance.HidePopup());
                _backToMenu.onClick.AddListener(() => SceneManager.LoadScene("MainMenu"));
            }
            else
            {
                _backButton.onClick.AddListener(() => { ScreenSwitcher.Instance.HidePopup(); });
                _backToMenu.onClick.AddListener(() =>
                {
                    ScreenSwitcher.Instance.HidePopup();
                    PhotonCallbacksManager.Leave();
                });
            }
        }

        private void SetBallColor(float value)
        {
            _ballImage.color = Color.HSVToRGB(value, 1, 1);
            BallColor.Save(value);
        }
    }
}