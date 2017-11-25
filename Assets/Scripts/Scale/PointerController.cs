using System;
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

    private int _moving = 0;

    private bool _clicked = false;
    private void Update()
    {
        if (_moving == 0) return;
        _current += _speed * Time.deltaTime * _moving;
        SetPosition(_current);
        if ((_current > 1f || _current < 0f) && !_clicked)
        {
            ScaleController.Instance.Check();
            return;
        }
        if (_current > 1f)
        {
            _clicked = false;
            SetPosition(1);
            _moving = -1;
        } else if (_current < 0)
        {
            _clicked = false;
            SetPosition(0);
            _moving = 1;
        }
    }

    public void Reset()
    {
        _current = 0;
        SetPosition(_current);
    }

    public void StartMoving()
    {
        _moving = 1;
    }

    public int CheckPointer()
    {
        _clicked = true;
        var size = ScaleController.Instance.SubScale.rect.width / 2;
        var pos = ScaleController.Instance.SubScale.position.x;
        var curPos = transform.position.x;
        if (curPos > pos - size && curPos < pos + size)
        {
            return 1;
        }
        return 0;
    }

    public void StopMoving()
    {
        _moving = 0;
    }

    private void SetPosition(float v)
    {
        _rt.anchoredPosition = new Vector2(_min + v * _width, _rt.anchoredPosition.y);
    }
}