using System.Collections.Generic;
using UnityEngine;

public class Clothes
{
    public static List<Sprite> Sprites;

    public static void Load()
    {
        Sprites = new List<Sprite>
        {
            Resources.Load<Sprite>("clothCroco"), 
            Resources.Load<Sprite>("clothCrocoBlue"),
            Resources.Load<Sprite>("clothCrocoRed")
        };
    }
}
