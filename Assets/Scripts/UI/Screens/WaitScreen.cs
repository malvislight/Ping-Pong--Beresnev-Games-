using Photon.Pun;
using ScreenSystem;

namespace UI.Screens
{
    public class WaitScreen : Screen
    {
        protected override void OnShow()
        {
        }

        protected override void OnHide()
        {
        }

        protected override ScreenType ScreenType => ScreenType.Wait;

        private void Update()
        {
            if(PhotonNetwork.CurrentRoom.PlayerCount != 2) return;
            
            ScreenSwitcher.Instance.ShowScreen(ScreenType.Game);
        }
    }
}