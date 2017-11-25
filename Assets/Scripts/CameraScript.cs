using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public static CameraScript Instance;

    private void Awake()
    {
        Instance = this;
        ScreenDarken = new Material(ScreenDarken);
    }
    
    private const float FollowSpeed = 5f;
    private Vector3 _focus = Vector3.zero;

    public void FocusOn(Vector2 v)
    {
        _focus = v;
    }

    private void Update()
    {
        var dir = _focus - transform.position;
        dir.z = 0;
        transform.position += dir * FollowSpeed * Time.deltaTime;
    }

    public Material ScreenDarken;
    public float _darkenValue = 1f;

    public void SetScreenDarkness(float v)
    {
        _darkenValue = v;
    }
}
