using UnityEngine;

public class ShopPlayerIO : MonoBehaviour
{
	public ShopGameLogic ShopGameLogic;
	
	private void OnMouseDown()
	{
		ShopGameLogic.ClickOnPlayer();
	}
}
