using UnityEngine;

public class Player {
	private GameObject _gameObject;
	
	public static PlayerState State;
	
	public Player(GameObject gameObject)
	{
		_gameObject = gameObject;

		if (State == null)
		{
			State = new PlayerState();
			State.LoadFromPrefs();
		}
	}

	public void Tickle()
	{
		State.Tickle();
	}
}
