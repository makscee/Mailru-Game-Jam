using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EyeType
{
	Blink0 = 0,
	Blink1 = 1,
    
	DownLeft0 = 3,
	DownLeft1 = 4,
	DownLeft2 = 5,
    
	Down0 = 6,
	Down1 = 11,
    
	Right0 = 7,
	Right1 = 13,
    
	TopLeft0 = 8,
    
	Center0 = 9,
	Center2 = 12,
	Center5 = 14,
	Center6 = 15,
	Center3 = 16,
    
	Left0 = 10,
    
	Top0 = 2,
}

public class Eyes {
	private static Dictionary<EyeType, Sprite> _sprites;

	public static void Load()
	{
		_sprites = new Dictionary<EyeType, Sprite>();
		
		foreach (var type in Enum.GetValues(typeof(EyeType)))
		{
			var sprite = Resources.Load<Sprite>("Pet/eyes" + (int)type);
			_sprites.Add((EyeType)type, sprite);
		}
	}

	public static Sprite GetActive(EyeType type)
	{
		return _sprites[type];
	}

	public static int Count()
	{
		return _sprites.Count;
	}
}
