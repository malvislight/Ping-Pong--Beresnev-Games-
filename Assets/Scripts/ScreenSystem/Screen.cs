namespace ScreenSystem
{
    public abstract class Screen : ScreenBase
    {
        protected abstract ScreenType ScreenType { get; }

        internal override void Register()
        {
            ScreenSwitcher.Instance.RegisterScreen(ScreenType, this);
        }
    }
}