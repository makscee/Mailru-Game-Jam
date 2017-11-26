using UnityEngine;

public class PlayerView {
    private static PawsType _pawsType = PawsType.Down;
    public static PawsType PawsType
    {
        get { return _pawsType; }
        set
        {
            _pawsType = value;
            ShopGameLogic.Instance.PetAnim.PlayerParts.Paws.sprite = Paws.Get(_pawsType);
        }
    }
   
    private static EyeType _eyeType = EyeType.Center3;
    public static EyeType EyeType
    {
        get { return _eyeType; }
        set
        {
            _eyeType = value;
            ShopGameLogic.Instance.PetAnim.PlayerParts.Eyes.sprite = Eyes.Get(_eyeType);
        }
    }
    
    private static NoseType _noseType = NoseType.Nose1;
    public static NoseType NoseType
    {
        get { return _noseType; }
        set
        {
            _noseType = value;
            ShopGameLogic.Instance.PetAnim.PlayerParts.Nose.sprite = Noses.Get(_noseType);
        }
    }
    
    private static TailState _tailType = TailState.Normal;
    public static TailState TailState
    {
        get { return _tailType; }
        set
        {
            _tailType = value;
            ShopGameLogic.Instance.PetAnim.PlayerParts.Tail.sprite = Tails.Get(_tailType);
        }
    }
    
    private static SuitType _suitType = SuitType.None;
    public static SuitType SuitType
    {
        get { return _suitType; }
        set
        {
            _suitType = value;

            var parts = ShopGameLogic.Instance.PetAnim.PlayerParts;
            
            var suitInfo = Suits.Get(_suitType);

            if (null == suitInfo)
            {
                parts.Paws.sprite = Paws.Get(_pawsType);
                parts.Suit.gameObject.SetActive(false);
                parts.Tail.sprite = Tails.Get(_tailType);

                return;
            }

            parts.Paws.sprite = suitInfo.PawsDown;
            parts.Tail.sprite = suitInfo.Tail;
            parts.Suit.sprite = suitInfo.Body;
            parts.Suit.gameObject.SetActive(true);
        }
    }

    public static void SetPaws(bool up)
    {
        var suitInfo = Suits.Get(_suitType);
        var parts = ShopGameLogic.Instance.PetAnim.PlayerParts;

        if (null == suitInfo)
        {
            parts.Paws.sprite = Paws.Get(up ? PawsType.Up : PawsType.Down);
        }
        else
        {
            parts.Paws.sprite = up ? suitInfo.PawsUp : suitInfo.PawsDown;
        }
    }
}
