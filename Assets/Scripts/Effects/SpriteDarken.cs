using System;
using UnityEngine;

public class SpriteDarken : MonoBehaviour
{
    private static float _darkValue = 1f;

    private static Action _update;

    public static float DarkValue
    {
        get { return _darkValue; }
        set
        {
            _darkValue = value;
            _update();
        }
    }

    private void Awake()
    {
        var sr = GetComponent<SpriteRenderer>();
        _update += () => sr.color = new Color(_darkValue, _darkValue, _darkValue);
    }
}