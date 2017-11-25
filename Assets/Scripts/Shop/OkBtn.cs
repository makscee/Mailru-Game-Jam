using UnityEngine;
using UnityEngine.UI;

public class OkBtn : MonoBehaviour {
    public GameObject ShopUI;
    public GameObject ShopOpenBtn;
    
    private void Start()
    {
        var btn = gameObject.GetComponent<Button>();
        btn.onClick.AddListener(Clicked);
    }
	
    private void Clicked()
    {
        ShopUI.SetActive(false);
        ShopOpenBtn.SetActive(true);
    }
}
