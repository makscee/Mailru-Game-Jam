using System;
using System.Collections;
using UnityEngine;

public enum BattleState
{
    Hidden,
    PreStart,
    Started,
    Stopped,
    Candy,
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
        SetScale(0.3f, SubScale1);
        SetScale(0.05f, SubScale2);
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
            case BattleState.Candy:
                break;
            case BattleState.Stopped:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public void Stop()
    {
        var res = _pc.StopMoving();
        BattleState = BattleState.Stopped;
        Utils.InvokeDelayed(() => { Scores.AddScorePlayer(res * 100 * (0.9f + 0.2f * UnityEngine.Random.value)); },
            0.5f);
        Utils.InvokeDelayed(() =>
        {
            _pc.Reset();
            SetScale(Size1 / 1.5f, SubScale1);
            SetScale(Size2 / 1.5f, SubScale2);
            StartCoroutine(CandyCoroutine());
        }, 1f);
    }

    public RectTransform CandyBar;

    private IEnumerator CandyCoroutine()
    {
        var t = 1f;
        BattleState = BattleState.Candy;
        Utils.Animate(Rt.anchoredPosition, new Vector2(0f, _offScreen / 2), 0.3f, v => Rt.anchoredPosition = v,
            null, true);
        yield return new WaitForSeconds(0.3f);
        while (t > 0)
        {
            t -= Time.deltaTime;
            CandyBar.sizeDelta = new Vector2(300f * t, CandyBar.rect.height);
            if (TapDown())
            {
                SetScale(Size1 * 1.5f, SubScale1);
                SetScale(Size2 * 1.5f, SubScale2);
                t = 1f;
                if (Size1 > 0.44f)
                {
                    SetScale(0.45f, SubScale1);
                    SetScale(0.45f / 6, SubScale2);
                    t = 0f;
                }
            }
            yield return null;
        }
        CandyBar.sizeDelta = new Vector2(0f, CandyBar.rect.height);
        BattleState = BattleState.Hidden;
        Utils.Animate(Rt.anchoredPosition, new Vector2(0f, _offScreen), 0.3f, v => Rt.anchoredPosition = v,
            null, true);
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