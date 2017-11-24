using System;
using UnityEngine;

public enum BattleState
{
    Hidden,
    PreStart,
    Started,
    Stopped
}

public class ScaleController : MonoBehaviour
{
    public RectTransform SubScale1, SubScale2, Pointer;
    public float Size1, Size2;
    private PointerController _pc;
    private float _offScreen;
    public static RectTransform Rt;
    public static ScaleController Instance;

    private void Awake()
    {
        Instance = this;
        _pc = Pointer.GetComponent<PointerController>();
        Rt = GetComponent<RectTransform>();
        _offScreen = -Screen.height / 2f - Rt.rect.height;
        Rt.anchoredPosition = new Vector2(0, _offScreen);
        var width = Rt.rect.width;
        SubScale1.anchoredPosition = new Vector2(-width / 4, 0);
        SubScale2.anchoredPosition = new Vector2(width / 4, 0);
    }

    public BattleState BattleState;

    private void Update()
    {
        if (!TapDown()) return;
        switch (BattleState)
        {
            case BattleState.Hidden:
                Run();
                break;
            case BattleState.PreStart:
                break;
            case BattleState.Started:
                Stop();
                break;
            case BattleState.Stopped:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public void Stop()
    {
        _pc.StopMoving();
        BattleState = BattleState.Stopped;
        Utils.InvokeDelayed(() =>
        {
            Utils.Animate(Rt.anchoredPosition, new Vector2(0f, _offScreen), 0.3f, (v) => { Rt.anchoredPosition = v; }, null, true);
        }, 0.5f);
    }

    public void Run()
    {
        Utils.Animate(Rt.anchoredPosition, new Vector2(0f, 0f), 0.3f, (v) => { Rt.anchoredPosition = v; }, null, true);
        BattleState = BattleState.PreStart;
        Utils.InvokeDelayed(() =>
        {
            BattleState = BattleState.Started;
            _pc.StartMoving();
        }, 0.7f);
    }

    public static bool TapDown()
    {
        return Input.GetKeyDown(KeyCode.Space);
    }

    public void SetScale(float size, RectTransform scale)
    {
        if (scale == SubScale1)
        {
            Size1 = size;
        }
        else
        {
            Size2 = size;
        }
        scale.sizeDelta = new Vector2(Rt.rect.width * size, scale.rect.height);
    }
}