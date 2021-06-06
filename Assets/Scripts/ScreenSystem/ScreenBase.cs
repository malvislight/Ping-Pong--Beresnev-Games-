using UnityEngine;

namespace ScreenSystem
{
    public abstract class ScreenBase : MonoBehaviour
    {
        internal abstract void Register();
        
        public void Show()
        {
            gameObject.SetActive(true);
            OnShow();
        }
        public void Hide()
        {
            gameObject.SetActive(false);
            OnHide();
        }

        protected abstract void OnShow();
        protected abstract void OnHide();
    }
}