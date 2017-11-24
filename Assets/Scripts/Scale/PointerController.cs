using UnityEngine;

public class PointerController : MonoBehaviour
{
    private float _min, _max, _width, _current, _speed = 1f;
    private RectTransform _rt;

    private void Start()
    {
        _width = ScaleController.Rt.rect.width;
        _min = -_width / 2;
        _max = -_min;
        _rt = GetComponent<RectTransform>();
        _current = 0;
        SetPosition(0f);
    }

    private bool _moving = false;
    private void Update()
    {
        if (!_moving) return;
        _current += _speed * Time.deltaTime;
        SetPosition(_current);
        if (_current > 1f)
        {
            ScaleController.Instance.Stop();
        }
    }

    public void StartMoving()
    {
        _moving = true;
    }

    public float StopMoving()
    {
        _moving = false;
        return 1;
    }

    private void SetPosition(float v)
    {
        _rt.anchoredPosition = new Vector2(_min + v * _width, _rt.anchoredPosition.y);
    }
}