using UnityEngine;
using UnityEngine.UI;

public class ScrollBtn : MonoBehaviour
{
	public GameObject View;
	public bool DirectionNext;
	
	private void Start()
	{
		var btn = gameObject.GetComponent<Button>();
		btn.onClick.AddListener(Clicked);
	}
	
	private void Clicked()
	{
		var incrIdx = DirectionNext ? 1 : -1;
		var newIdx = PlayerState.ClothIndex + incrIdx;
		if (newIdx < 0)
		{
			newIdx = Cloth.Sprites.Count - 1;
		} else if (newIdx >= Cloth.Sprites.Count)
		{
			newIdx = 0;
		}
		PlayerState.ClothIndex = newIdx;

		var sprite = Cloth.Sprites[newIdx];

		var image = View.GetComponent<Image>();

		image.sprite = sprite;
	}
}
