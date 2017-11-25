using UnityEngine;
using UnityEngine.UI;

public class ScrollBtn : MonoBehaviour
{

	public PlayerSmth PlayerSmth;
	public bool DirLeft;
	
	private void Start()
	{
		var btn = gameObject.GetComponent<Button>();
		btn.onClick.AddListener(Clicked);
	}
	
	private void Clicked()
	{
		ShopGameLogic.Instance.ChangeSmth(PlayerSmth, DirLeft);
	}
}
