namespace Utils.CameraBorder
{
    public enum SideType
    {
        None = 0,
        Top,
        Buttom = Top << 1,
        Left = Buttom << 1,
        Right = Left << 1,
        All = (Right << 1) - 1
    }
}