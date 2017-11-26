public enum HandsType
{
    Down,
    Up
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
   
    private static EyeType _eyeType = EyeType.Center3;
    public static EyeType EyeType
    {
        get { return _eyeType; }
        set
        {
            _eyeType = value;
            ShopGameLogic.Instance._PlayerParts.Eyes.sprite = Eyes.GetActive(_eyeType);
        }
    }
    
    private static NoseType _noseType = NoseType.Nose1;
    public static NoseType NoseType
    {
        get { return _noseType; }
        set
        {
            _noseType = value;
            ShopGameLogic.Instance._PlayerParts.Nose.sprite = Noses.GetActive(_noseType);
        }
    }
}
