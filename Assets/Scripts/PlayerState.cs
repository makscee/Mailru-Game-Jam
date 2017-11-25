using UnityEngine;

public class PlayerState
{
	private static int TickleMoodIncr = 10;
	
    public static int FoodCount;
    public static int CandiesCount;
    public static int Mood;

    public static int HatIndex;
    public static int GlassesIndex;
    
    public void SaveToPrefs()
    {
		PlayerPrefs.SetInt("FoodCount", FoodCount);
		PlayerPrefs.SetInt("CandiesCount", CandiesCount);
		PlayerPrefs.SetInt("Mood", Mood);
		PlayerPrefs.SetInt("HatIndex", HatIndex);
		PlayerPrefs.SetInt("GlassesIndex", GlassesIndex);
    }

    public void LoadFromPrefs()
    {
	    FoodCount = PlayerPrefs.GetInt("FoodCount", 0);
	    CandiesCount = PlayerPrefs.GetInt("CandiesCount", 0);
	    Mood = PlayerPrefs.GetInt("Mood", 0);

	    HatIndex = PlayerPrefs.GetInt("HatIndex", 0);
	    GlassesIndex = PlayerPrefs.GetInt("GlassesIndex", 0);
    }

	public void Tickle()
	{
		Debug.Log("Tickle");
		
		Mood += TickleMoodIncr;
		PlayerPrefs.SetInt("Mood", Mood);
	}
}
