using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ScreenSystem
{
    public class ScreenSwitcher : MonoBehaviour
    {
        public static ScreenSwitcher Instance { get; private set; }

        public static event Action OnInit;

        [SerializeField] private ScreenType _startScreen;
        
        [SerializeField] private GameObject _screensParent;
        [SerializeField] private GameObject _popupsParent;
        [SerializeField] private Image _background;

        private readonly Dictionary<ScreenType, Screen> _screens = new Dictionary<ScreenType, Screen>();
        private readonly Dictionary<PopupType, Popup> _popups = new Dictionary<PopupType, Popup>();
        
        public T GetScreen<T>(ScreenType type)
        {
            return !_screens.ContainsKey(type) ? default : _screens[type].GetComponent<T>();
        }
        
        public T GetPopup<T>(PopupType type)
        {
            return !_popups.ContainsKey(type) ? default : _popups[type].GetComponent<T>();
        }

        private Screen _currentScreen;
        private Popup _currentPopup;

        private void Awake()
        {
            Instance = this;
            
            RegisterChildren(_screensParent.transform);
            RegisterChildren(_popupsParent.transform);

            OnInit?.Invoke();
        }

        private void Start()
        {
            ShowScreen(_startScreen);
        }

        private void RegisterChildren(Transform parent)
        {
            for (var i = 0; i < parent.childCount; i++)
            {
                var child = parent.GetChild(i);
                var screenBase = child.GetComponent<ScreenBase>();
                if (screenBase != null)
                {
                    screenBase.Register();
                }
                child.gameObject.SetActive(false);
            }
        }

        public void RegisterScreen(ScreenType type, Screen screen)
        {
            _screens.Add(type, screen);
        }
        
        public void RegisterPopup(PopupType type, Popup popup)
        {
            _popups.Add(type, popup);
        }

        public void ShowScreen(ScreenType type, bool pause = false)
        {
            if (_currentScreen != null)
            {
                _currentScreen.Hide();
                _currentScreen = null;
            }

            _currentScreen = _screens[type];
            _currentScreen.Show();
            
            Time.timeScale = 1;
        }
        
        public void ShowPopup(PopupType type)
        {
            HidePopup();

            _currentPopup = _popups[type];
            _currentPopup.Show();

            _background.gameObject.SetActive(true);
        }
        
        public void HidePopup()
        {
            if (_currentPopup == null) return;

            _currentPopup.Hide();
            _currentPopup = null;

            _background.gameObject.SetActive(false);

        }
    }
}