public enum HandsType
{
    Down,
    Up
}

public enum PillowType
{
    Idle,
    Up
}

public enum EyeType
{
    
}

public class PlayerView {
    private static HandsType _handsType = HandsType.Down;
    public static HandsType HandsType
    {
        get { return _handsType; }
        set
        {
            _handsType = value;
        }
    }
    
    private static PillowType _pillowType = PillowType.Idle;
    public static PillowType PillowType
    {
        get { return _pillowType; }
        set
        {
            _pillowType = value;
        }
    }
}
