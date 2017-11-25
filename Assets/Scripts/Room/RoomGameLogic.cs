﻿using UnityEngine;

public class RoomGameLogic : MonoBehaviour
{
    private GameObject _playerView;
    private GameObject _playerObj;

    [SerializeField] private Camera _camera;
    
    private Vector2 _moveForce;

    private Rect _bounds;
    
    private void Awake()
    {
        if (_camera == null)
        {
            _camera = Camera.main;
        }

        _playerView = (GameObject) Instantiate(Resources.Load("Player", typeof(GameObject)));

        _playerObj = (GameObject) Instantiate(Resources.Load("RoomPlayerIO", typeof(GameObject)));
        _playerView.transform.parent = _playerObj.transform;
        
        _playerObj.GetComponent<RoomPlayerIO>().RoomGameLogic = this;
        
        SetupBounds();
        
        _playerObj.transform.position = new Vector3(0, _bounds.yMin, -2f);
        
        PlayerState.LoadFromPrefs();
    }

    private void SetupBounds()
    {
        Vector3 topRightPoint = _camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

        var size = _playerView.GetComponent<SpriteRenderer>().sprite.bounds.size;
        var viewScale = _playerView.transform.localScale;

        var halfWidth = size.x / 2 * viewScale.x;
        
        _bounds.xMin = -topRightPoint.x + halfWidth;
        _bounds.xMax = topRightPoint.x - halfWidth;
        _bounds.yMin = -1.5f;
    }

    private void FixedUpdate()
    {
        Physics();
    }

    private void Physics()
    {
        if (Mathf.Abs(_moveForce.magnitude) <= Mathf.Epsilon)
        {
            return;
        }
        _moveForce.y -= 100 * Time.deltaTime;

        var deltaForce = _moveForce * Time.deltaTime;
        
        var pos = _playerObj.transform.position;
        pos.x += deltaForce.x;
        pos.y += deltaForce.y;
        if (pos.y <= _bounds.yMin)
        {
            _moveForce.Set(0, 0);
            pos.y = _bounds.yMin;
        }
        
        if (pos.x <= _bounds.xMin)
        {
            pos.x = _bounds.xMin;
            _moveForce.x = 0;
        } else if (pos.x >= _bounds.xMax)
        {
            pos.x = _bounds.xMax;
            _moveForce.x = 0;
        }
        
        _playerObj.transform.position = pos;        
    }

    public void ClickOnPlayer()
    {
        if (Mathf.Abs(_moveForce.magnitude) <= Mathf.Epsilon)
        {
            PlayerState.Tickle();

            float xForce = (Random.value - 0.5f) * 6f;
            _moveForce = new Vector2(xForce, 20f);
        }        
    }
}
