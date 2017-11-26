using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PawsType
{
    Down,
    Up
}

public class Paws {
    private static Dictionary<PawsType, Sprite> _sprites;

    public static void Load()
    {
        _sprites = new Dictionary<PawsType, Sprite>()
        {
            {PawsType.Down, Resources.Load<Sprite>("Pet/paws_down")},
            {PawsType.Up, Resources.Load<Sprite>("Pet/paws_up")},
        };
    }

    public static Sprite Get(PawsType type)
    {
        return _sprites[type];
    }

    public static int Count()
    {
        return _sprites.Count;
    }
}
