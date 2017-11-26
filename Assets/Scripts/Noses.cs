using System;
using System.Collections.Generic;
using UnityEngine;

public enum NoseType
{
	Nose0,
	Nose1,
	Nose2,
	Nose3,
	Nose4,
	Nose5
}

public class Noses {
	private static Dictionary<NoseType, Sprite> _sprites;

	public static void Load()
	{
		_sprites = new Dictionary<NoseType, Sprite>();
		
		foreach (var type in Enum.GetValues(typeof(NoseType)))
		{
			var sprite = Resources.Load<Sprite>("Pet/nose" + (int)type);
			_sprites.Add((NoseType)type, sprite);
		}
	}

	public static Sprite GetActive(NoseType type)
	{
		return _sprites[type];
	}

	public static int Count()
	{
		return _sprites.Count;
	}
}
