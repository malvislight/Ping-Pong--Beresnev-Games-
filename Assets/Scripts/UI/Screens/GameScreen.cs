using ScreenSystem;
using UnityEngine;
using Screen = ScreenSystem.Screen;

namespace UI.Screens
{
    public class GameScreen : Screen
    {
        protected override void OnShow()
        {
            Time.timeScale = 1;
        }

        protected override void OnHide()
        {
        }

        protected override ScreenType ScreenType => ScreenType.Game;
        
    }
}