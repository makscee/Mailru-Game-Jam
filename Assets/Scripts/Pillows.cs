using System.Collections.Generic;
using UnityEngine;

public enum PillowState
{
	Idle,
	Up,
	//Compress,
	//Down
}

public class Pillows {
	public static List<Dictionary<PillowState, Sprite>> Sprites;

	public static void Load()
	{
		var pillowIdle = Resources.Load<Sprite>("Pet/pillow");
		var pillowUp = Resources.Load<Sprite>("Pet/pillow_up");
		
		//SpriteAtlas atlasPurple = Resources.Load<SpriteAtlas>("Pillows/pillowPurple");
		
		Sprites = new List<Dictionary<PillowState, Sprite>>
		{
			new Dictionary<PillowState, Sprite>
			{
				{PillowState.Idle, pillowIdle},				
				{PillowState.Up, pillowUp},
				//{PillowState.Compress, atlasPurple.GetSprite("pillowCompress_purple")},				
				//{PillowState.Down, atlasPurple.GetSprite("pillowDown_purple")},				
			}
		};
	}

	public static Sprite GetActive(PillowState state)
	{
		return Sprites[PlayerState.PillowIndex][state];
	} 
}
