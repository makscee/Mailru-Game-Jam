using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

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
    public Text CandyCount, CandyUseText;
    public float Size1, Size2;
    private PointerController _pc;
    private float _offScreen, _onScreen;
    public static RectTransform Rt;
    public static ScaleController Instance;

    private void Awake()
    {
        Rt = GetComponent<RectTransform>();
        Rt.sizeDelta = new Vector2(Screen.width, 30);
        Instance = this;
        _pc = Pointer.GetComponent<PointerController>();
        _offScreen = Screen.height / 2f + Rt.rect.height;
        _onScreen = _offScreen / 2;
        Rt.anchoredPosition = new Vector2(0, _offScreen);
        var width = Rt.rect.width;
        SubScale1.anchoredPosition = new Vector2(-width / 4, 0);
        SubScale2.anchoredPosition = new Vector2(width / 4, 0);
        SetScale(0.3f, SubScale1);
        SetScale(0.05f, SubScale2);
        PlayerState.LoadFromPrefs();
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
        var c = Color.white;
        switch (res)
        {
            case 0:
                StartCoroutine(ShakeBar());
                break;
            case 1:
                c = Color.yellow;
                break;
            case 4:
                c = Color.red;
                break;
        }
        var pos = Camera.main.ScreenToWorldPoint(_pc.transform.position);
        Effects.ExplosionEffect(pos, c);
        Utils.InvokeDelayed(() =>
        {
            _pc.Reset();
            SetScale(Size1 / 1.5f, SubScale1);
            SetScale(Size2 / 1.5f, SubScale2);

            Hide();
            Scores.AddScorePlayer(res * 100 * (0.9f + 0.2f * UnityEngine.Random.value));
            Utils.InvokeDelayed(() =>
            {
                CameraScript.Instance.FocusOn(BattleLevel.Instance.Enemy.transform.position);
                Utils.InvokeDelayed(() => Scores.AddScoreEnemy(100), 1f);
            }, 1f);
            Utils.InvokeDelayed(() =>
            {
                CameraScript.Instance.FocusOn(BattleLevel.Instance.Player.transform.position);
                StartCoroutine(CandyCoroutine());
            }, 3f);
        }, 1f);
    }

    public RectTransform CandyBar;

    private int _candyUse = 5;

    private IEnumerator ShakeBar()
    {
        var t = 0.3f;
        while (t > 0)
        {
            t -= Time.deltaTime;
            var v1 = UnityEngine.Random.value - 0.5f;
            var v2 = UnityEngine.Random.value - 0.5f;
            const float mult = 30f;
            Rt.anchoredPosition = new Vector2(v1 * mult, _onScreen + v2 * mult);
            yield return null;
        }
        Rt.anchoredPosition = new Vector2(0, _onScreen);
    }

    private void Hide()
    {
        Utils.Animate(Rt.anchoredPosition, new Vector2(0f, _offScreen), 0.3f, v => Rt.anchoredPosition = v,
            null, true);
        Utils.Animate(0.5f, 1f, 0.3f, v => CameraScript.Instance.SetScreenDarkness(v),
            null, true);
    }

    private IEnumerator CandyCoroutine()
    {
        var t = 1f;
        BattleState = BattleState.Candy;
        Utils.Animate(1f, 0.5f, 0.3f, v => CameraScript.Instance.SetScreenDarkness(v),
            null, true);
        Utils.Animate(Rt.anchoredPosition, new Vector2(0f, _offScreen / 2), 0.3f, v => Rt.anchoredPosition = v,
            null, true);
        yield return new WaitForSeconds(1f);
        CandyUseText.text = "x" + _candyUse;
        CandyUseText.gameObject.SetActive(true);
        CandyUseText.fontSize = 45;
        while (t > 0)
        {
            t -= Time.deltaTime / 2;
            CandyBar.sizeDelta = new Vector2(300f * t, CandyBar.rect.height);
            if (TapDown() && PlayerState.CandiesCount - _candyUse >= 0)
            {
                SetScale(Size1 * 1.5f, SubScale1);
                SetScale(Size2 * 1.5f, SubScale2);
                t = 1f;
                PlayerState.CandiesCount -= _candyUse;
                _candyUse *= 2;
                CandyUseText.text = "x" + _candyUse;
                if (CandyUseText.fontSize < 100)
                {
                    CandyUseText.fontSize += 10;
                }
                if (Size1 > 0.44f)
                {
                    SetScale(0.45f, SubScale1);
                    SetScale(0.45f / 6, SubScale2);
                    t = 0f;
                }
            }
            yield return null;
        }
        yield return new WaitForSeconds(0.4f);
        CandyBar.sizeDelta = new Vector2(0f, CandyBar.rect.height);
        Hide();
        BattleState = BattleState.Hidden;
        CandyUseText.gameObject.SetActive(false);
    }

    public void Run()
    {
        Utils.Animate(1f, 0.5f, 0.3f, v => CameraScript.Instance.SetScreenDarkness(v),
            null, true);
        Utils.Animate(Rt.anchoredPosition, new Vector2(0f, _onScreen), 0.3f, v => Rt.anchoredPosition = v,
            null, true);
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