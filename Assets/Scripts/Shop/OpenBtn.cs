using UnityEngine;
using UnityEngine.UI;

public class OpenBtn : MonoBehaviour
{
	public GameObject ShopUI;
	
	private void Start()
	{
		ShopUI.SetActive(false);
		var btn = gameObject.GetComponent<Button>();
		btn.onClick.AddListener(Clicked);
}
	
	private void Clicked()
	{
		ShopUI.SetActive(true);
		gameObject.SetActive(false);
	}
}
