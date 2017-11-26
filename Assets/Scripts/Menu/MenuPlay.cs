using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPlay : MonoBehaviour
{
	public SpriteRenderer SecondSprite;
	
	private void OnMouseDown()
	{
		gameObject.SetActive(false);
		SecondSprite.gameObject.SetActive(true);
		
		SceneManager.LoadScene("Room");
	}
}
