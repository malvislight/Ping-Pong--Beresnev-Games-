using System;

namespace Data
{
    public static class SavingKeyProvider
    {
        public static string GetKey(SavesType type)
        {
            return Enum.GetName(typeof(SavesType), type);
        }
    }
    
    public enum SavesType
    {
        SinglePlayScore,
        MultiPlayScore,
        BallHueValue
    }
}