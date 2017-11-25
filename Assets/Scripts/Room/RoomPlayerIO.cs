using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RoomPlayerIO : MonoBehaviour
{
	public RoomGameLogic RoomGameLogic;
	
	private void OnMouseDown()
	{
		RoomGameLogic.ClickOnPlayer();
	}
	
	/*private const int CriticalLowMood = 30;
	
	[SerializeField] private int FoodCount;
	[SerializeField] private int CandyCount;
	[SerializeField] private int Mood;
	
	[SerializeField]
	private Vector2 _jumpForce = new Vector2(250f, 200f);
	
	public Slider HealthSlider;
	public Image HealthSliderFill;
	
	private void Start ()
	{
		_rigidbody2D = GetComponent<Rigidbody2D>();
		UpdateSlider(HealthSlider, HealthSliderFill, _health/MaxHealth);
	}

	private void OnMouseDown()
	{
		if (Mathf.Abs(_rigidbody2D.velocity.magnitude) <= Mathf.Epsilon)
		{
			Jump();
		}
	}

	private void Jump()
	{
		_health = Mathf.Clamp(_health - 10, 0, MaxHealth);
		if (_health < Mathf.Epsilon)
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
			return;
		}
		UpdateSlider(HealthSlider, HealthSliderFill, _health/MaxHealth);
		
		float xForce = (Random.value - 0.5f) * _jumpForce[0];
		_rigidbody2D.AddForce(new Vector2(xForce, _jumpForce[1]));		
	}

	private static void UpdateSlider(Slider slider, Image sliderImage, float percent)
	{
		var color = Color.green;
		if (percent < 0.3f)
		{
			color = Color.red;
		}
		slider.value = percent;
		sliderImage.color = color;		
	}*/
}
