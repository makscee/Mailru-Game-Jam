public class PlayerView {
    private static PawsType _pawsType = PawsType.Down;
    public static PawsType PawsType
    {
        get { return _pawsType; }
        set
        {
            _pawsType = value;
            ShopGameLogic.Instance._PlayerParts.Paws.sprite = Paws.GetActive(_pawsType);
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
