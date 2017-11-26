using System.Collections.Generic;
using UnityEngine;

public enum SuitType
{
    None,
    Croc,
    Pig
}

public class SuitInfo
{
    public Sprite Body;
    public Sprite PawsUp;
    public Sprite PawsDown;
    public Sprite Tail;
}

public class Suits
{
    private static Dictionary<SuitType, SuitInfo> _suits;

    public static void Load()
    {
        var croc = new SuitInfo
        {
            Body = Resources.Load<Sprite>("Croc/body"),
            PawsDown = Resources.Load<Sprite>("Croc/paws_down"),
            PawsUp = Resources.Load<Sprite>("Croc/paws_up"),
            Tail = Resources.Load<Sprite>("Croc/tail"),
        };
        
        var pig = new SuitInfo
        {
            Body = Resources.Load<Sprite>("Pig/body"),
            PawsDown = Resources.Load<Sprite>("Pig/paws_down"),
            PawsUp = Resources.Load<Sprite>("Pig/paws_up"),
            Tail = Resources.Load<Sprite>("Pig/tail"),
        };

        _suits = new Dictionary<SuitType, SuitInfo>()
        {
            {SuitType.None, null},
            {SuitType.Croc, croc},
            {SuitType.Pig, pig},
        };
    }

    public static SuitInfo Get(SuitType type)
    {
        return _suits[type];
    }

    public static int Count()
    {
        return _suits.Count;
    }
}
