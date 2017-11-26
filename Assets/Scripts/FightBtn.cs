using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FightBtn : MonoBehaviour {
	private void Start()
	{
		var btn = gameObject.GetComponent<Button>();
		btn.onClick.AddListener(() =>
		{
			SceneManager.LoadScene("Battle");
		});
	}
}
