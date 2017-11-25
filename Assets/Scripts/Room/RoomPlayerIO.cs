using UnityEngine;

public class RoomPlayerIO : MonoBehaviour
{
	public RoomGameLogic RoomGameLogic;
	
	private void OnMouseDown()
	{
		RoomGameLogic.ClickOnPlayer();
	}
}
