namespace ScreenSystem
{
    public abstract class Popup : ScreenBase
    {
        protected abstract PopupType PopupType { get; } 
        
        internal override void Register()
        {
            ScreenSwitcher.Instance.RegisterPopup(PopupType, this);
        }
    }
}