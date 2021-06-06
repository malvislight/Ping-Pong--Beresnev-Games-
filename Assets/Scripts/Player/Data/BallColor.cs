using System;
using Data;
using UnityEngine;

namespace Player.Data
{
    public static class BallColor
    {
        public static event Action OnChanged;
        private static string _ballHueValueKey => SavingKeyProvider.GetKey(SavesType.BallHueValue);
        public static Color Color { get; private set; } = Color.HSVToRGB(PlayerPrefs.GetFloat(_ballHueValueKey, 1), 1, 1);
        public static void Save(float hue)
        {
            PlayerPrefs.SetFloat(_ballHueValueKey, hue);
            Color = Color.HSVToRGB(hue, 1, 1);
            OnChanged?.Invoke();
        }
    }
}