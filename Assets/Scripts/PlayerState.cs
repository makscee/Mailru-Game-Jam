using UnityEngine;

public class PlayerState
{
	private const int TickleMoodIncr = 10;

	public static int FoodCount;
	private static int _candiesCount;
    public static int Mood;

    public static int HatIndex;
    public static int GlassesIndex;

	public static int CandiesCount
	{
		get { return _candiesCount; }
		set
		{
			_candiesCount = value;
			ScaleController.Instance.CandyCount.text = value.ToString();
		}
	}

	public static void SaveToPrefs()
    {
		PlayerPrefs.SetInt("FoodCount", FoodCount);
		PlayerPrefs.SetInt("CandiesCount", _candiesCount);
		PlayerPrefs.SetInt("Mood", Mood);
		PlayerPrefs.SetInt("HatIndex", HatIndex);
		PlayerPrefs.SetInt("GlassesIndex", GlassesIndex);
    }

    public static void LoadFromPrefs()
    {
	    FoodCount = PlayerPrefs.GetInt("FoodCount", 5);
	    CandiesCount = PlayerPrefs.GetInt("CandiesCount", 50);
	    Mood = PlayerPrefs.GetInt("Mood", 50);

	    HatIndex = PlayerPrefs.GetInt("HatIndex", 0);
	    GlassesIndex = PlayerPrefs.GetInt("GlassesIndex", 0);
    }

	public static void Tickle()
	{
		Debug.Log("Tickle");
		
		Mood += TickleMoodIncr;
		PlayerPrefs.SetInt("Mood", Mood);
	}
}
