using System.Collections.Generic;
using UnityEngine;

public enum TailState
{
	Normal,
}

public class Tails {
	private static Dictionary<TailState, Sprite> _sprites;

	public static void Load()
	{
		_sprites = new Dictionary<TailState, Sprite>()
		{
			{TailState.Normal, Resources.Load<Sprite>("Pet/tail")},
		};
	}

	public static Sprite Get(TailState type)
	{
		return _sprites[type];
	}

	public static int Count()
	{
		return _sprites.Count;
	}
}
