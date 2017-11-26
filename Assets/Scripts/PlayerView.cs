using UnityEngine;

public class PlayerView
{

    private PetAnim anim;

    public PlayerView(PetAnim pa)
    {
        anim = pa;
    }
    
    private PawsType _pawsType = PawsType.Down;
    public PawsType PawsType
    {
        get { return _pawsType; }
        set
        {
            _pawsType = value;
            anim.PlayerParts.Paws.sprite = Paws.Get(_pawsType);
        }
    }
   
    private EyeType _eyeType = EyeType.Center3;
    public EyeType EyeType
    {
        get { return _eyeType; }
        set
        {
            _eyeType = value;
            anim.PlayerParts.Eyes.sprite = Eyes.Get(_eyeType);
        }
    }
    
    private NoseType _noseType = NoseType.Nose1;
    public NoseType NoseType
    {
        get { return _noseType; }
        set
        {
            _noseType = value;
            anim.PlayerParts.Nose.sprite = Noses.Get(_noseType);
        }
    }
    
    private TailState _tailType = TailState.Normal;
    public TailState TailState
    {
        get { return _tailType; }
        set
        {
            _tailType = value;
            anim.PlayerParts.Tail.sprite = Tails.Get(_tailType);
        }
    }
    
    private SuitType _suitType = SuitType.None;
    public SuitType SuitType
    {
        get { return _suitType; }
        set
        {
            _suitType = value;

            var parts = anim.PlayerParts;
            
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

    public void SetPaws(bool up)
    {
        var suitInfo = Suits.Get(_suitType);
        var parts = anim.PlayerParts;

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
