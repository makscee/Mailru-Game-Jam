using UnityEngine;

public class PlayerState
{
	private const int TickleMoodIncr = 10;

	public static int FoodCount;
	private static int _candiesCount;
    public static int Mood;

	private static int _clothIndex;
	public static int ClothIndex
	{
		get { return _clothIndex; }
		set
		{
			_clothIndex = value;
			PlayerPrefs.SetInt("ClothIndex", ClothIndex);
			Debug.Log("change cloth to " + ClothIndex);
		}
	}
	
	private static int _pillowIndex;
	public static int PillowIndex
	{
		get { return _pillowIndex; }
		set
		{
			_pillowIndex = value;
			PlayerPrefs.SetInt("PillowIndex", _pillowIndex);
			Debug.Log("change pillow to " + _pillowIndex);
		}
	}

	public static int CandiesCount
	{
		get { return _candiesCount; }
		set
		{
			_candiesCount = value;
			if (ScaleController.Instance != null)
			{
				ScaleController.Instance.CandyCount.text = value.ToString();
			}
		}
	}

	public static void SaveToPrefs()
    {
		PlayerPrefs.SetInt("FoodCount", FoodCount);
		PlayerPrefs.SetInt("CandiesCount", _candiesCount);
		PlayerPrefs.SetInt("Mood", Mood);

	    PlayerPrefs.SetInt("ClothIndex", ClothIndex);
		PlayerPrefs.SetInt("PillowIndex", PillowIndex);
    }

    public static void LoadFromPrefs()
    {
	    FoodCount = PlayerPrefs.GetInt("FoodCount", 5);
	    CandiesCount = PlayerPrefs.GetInt("CandiesCount", 50);
	    Mood = PlayerPrefs.GetInt("Mood", 50);

	    ClothIndex = PlayerPrefs.GetInt("ClothIndex", 0);
	    PillowIndex = PlayerPrefs.GetInt("PillowIndex", 0);
    }

	public static void Tickle()
	{
		Mood += TickleMoodIncr;
		PlayerPrefs.SetInt("Mood", Mood);
		
		Debug.Log("change mood to " + Mood);
	}
}
