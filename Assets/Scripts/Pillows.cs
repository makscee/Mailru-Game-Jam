using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public enum PillowState
{
	Idle,
	Compress,
	Up,
	Down
}

public class Pillows {
	public static List<Dictionary<PillowState, Sprite>> Sprites;

	public static void Load()
	{
		SpriteAtlas atlasPurple = Resources.Load<SpriteAtlas>("Pillows/pillowPurple");
		
		Sprites = new List<Dictionary<PillowState, Sprite>>
		{
			new Dictionary<PillowState, Sprite>
			{
				{PillowState.Idle, atlasPurple.GetSprite("pillowIdle_purple")},				
				{PillowState.Compress, atlasPurple.GetSprite("pillowCompress_purple")},				
				{PillowState.Up, atlasPurple.GetSprite("pillowUp_purple")},				
				{PillowState.Down, atlasPurple.GetSprite("pillowDown_purple")},				
			}
		};
	}

	public static Sprite GetActive(PillowState state)
	{
		return Sprites[PlayerState.PillowIndex][state];
	} 
}
