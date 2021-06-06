using System;
using ScreenSystem;
using UnityEngine;
using UnityEngine.EventSystems;
using Screen = ScreenSystem.Screen;

namespace UI.Screens
{
    public class StartScreen : Screen, IPointerDownHandler
    {
        public static event Action OnStart;
        protected override ScreenType ScreenType => ScreenType.Start;

        protected override void OnShow()
        {
            Time.timeScale = 1;
        }

        protected override void OnHide()
        {
            
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            ScreenSwitcher.Instance.ShowScreen(ScreenType.Game);
            OnStart?.Invoke();
        }
    }
}