﻿using System;
using System.Collections;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState
{
    Hidden,
    PreStart,
    Started,
    Stopped,
}

public class ScaleController : MonoBehaviour
{
    public RectTransform SubScale, Pointer;
    public Results Results;
    public Text PlayerComboText, EnemyComboText;
    public float Size1, Size2;
    private PointerController _pc;
    private float _offScreen, _onScreen, _ctOffScreen;
    public static RectTransform Rt;
    public static ScaleController Instance;

    private void Awake()
    {
        Rt = GetComponent<RectTransform>();
        Rt.sizeDelta = new Vector2(Screen.width, 30);
        Instance = this;
        _pc = Pointer.GetComponent<PointerController>();
        _offScreen = Screen.height / 2f + Rt.rect.height;
        _ctOffScreen = Screen.width / 2f + Rt.rect.width;
        _onScreen = _offScreen / 2;
        Rt.anchoredPosition = new Vector2(0, _offScreen);
        var width = Rt.rect.width;
        SubScale.anchoredPosition = new Vector2(UnityEngine.Random.value * width / 3 - width / 6, 0);
//        SubScale2.anchoredPosition = new Vector2(width / 4, 0);
        SetScale(0.3f, SubScale);
//        SetScale(0.05f, SubScale2);
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
                Check();
                break;
            case BattleState.Stopped:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void AddCombo()
    {
        Scores.ScorePlayer++;
        PlayerComboText.text = "x" + Scores.ScorePlayer;
        PlayerComboText.fontSize = Math.Min(400, PlayerComboText.fontSize + 30);
        PlayerComboText.transform.rotation = Quaternion.AngleAxis(UnityEngine.Random.Range(-25, 25), Vector3.forward);
    }

    public void Check()
    {
        var res = _pc.CheckPointer();
        var c = Color.white;
        switch (res)
        {
            case 0:
                StartCoroutine(ShakeBar());
                BattleState = BattleState.Stopped;
                _pc.StopMoving();
                Scores.ScoreEnemy = UnityEngine.Random.Range(4, 8);
                EnemyComboText.text = "x" + Scores.ScoreEnemy;
                EnemyComboText.transform.rotation = Quaternion.AngleAxis(UnityEngine.Random.Range(-25, 25), Vector3.forward);
                
                Utils.InvokeDelayed(() =>
                {
                    _pc.Reset();
                    Hide();
                    HideComboText(PlayerComboText);
                    Utils.InvokeDelayed(() =>
                    {
                        CameraScript.Instance.FocusOn(BattleLevel.Instance.Enemy.transform.position);
                        ShowComboText(EnemyComboText);
                    }, 1f);
                    Utils.InvokeDelayed(
                        () =>
                        {
                            CameraScript.Instance.FocusOn(BattleLevel.Instance.Player.transform.position);
                            HideComboText(EnemyComboText);
                            ShowComboText(PlayerComboText);
                            Utils.InvokeDelayed(() =>
                            {
                                HideComboText(PlayerComboText);
//                                Results.Run();
                            }, 1f);
                        }, 3f);
                }, 1f);
                break;
            case 1:
                c = Color.yellow;
                AddCombo();
                break;
            case 4:
                c = Color.red;
                break;
        }
        var pos = Camera.main.ScreenToWorldPoint(_pc.transform.position);
        CutSubscale(_pc.transform.position.x);
        Effects.ExplosionEffect(pos, c);
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

    private void ShowComboText(Graphic t)
    {
        Utils.Animate(t.rectTransform.anchoredPosition, new Vector2(0f, 0f), 0.3f, 
            v => t.rectTransform.anchoredPosition = v, null, true);
    }

    private void HideComboText(Graphic t)
    {
        float os;
        if (t == PlayerComboText) os = -_ctOffScreen;
        else os = _ctOffScreen;
        Utils.Animate(t.rectTransform.anchoredPosition, new Vector2(os * 2, 0), 0.3f, 
            v => t.rectTransform.anchoredPosition = v, null, true);
    }

    private void Hide()
    {
        Utils.Animate(Rt.anchoredPosition, new Vector2(0f, _offScreen), 0.3f, v => Rt.anchoredPosition = v,
            null, true);
        Utils.Animate(0.5f, 1f, 0.3f, v => SpriteDarken.DarkValue = v,
            null, true);
    }

//    private IEnumerator CandyCoroutine()
//    {
//        var t = 1f;
//        BattleState = BattleState.Candy;
//        Utils.Animate(1f, 0.5f, 0.3f, v => CameraScript.Instance.SetScreenDarkness(v),
//            null, true);
//        Utils.Animate(Rt.anchoredPosition, new Vector2(0f, _offScreen / 2), 0.3f, v => Rt.anchoredPosition = v,
//            null, true);
//        yield return new WaitForSeconds(1f);
//        CandyUseText.text = "x" + _candyUse;
//        CandyUseText.gameObject.SetActive(true);
//        CandyUseText.fontSize = 45;
//        while (t > 0)
//        {
//            t -= Time.deltaTime / 2;
//            CandyBar.sizeDelta = new Vector2(300f * t, CandyBar.rect.height);
//            if (TapDown() && PlayerState.CandiesCount - _candyUse >= 0)
//            {
//                SetScale(Size1 * 1.5f, SubScale1);
//                SetScale(Size2 * 1.5f, SubScale2);
//                t = 1f;
//                PlayerState.CandiesCount -= _candyUse;
//                _candyUse *= 2;
//                CandyUseText.text = "x" + _candyUse;
//                if (CandyUseText.fontSize < 100)
//                {
//                    CandyUseText.fontSize += 10;
//                }
//                if (Size1 > 0.44f)
//                {
//                    SetScale(0.45f, SubScale1);
//                    SetScale(0.45f / 6, SubScale2);
//                    t = 0f;
//                }
//            }
//            yield return null;
//        }
//        yield return new WaitForSeconds(0.4f);
//        CandyBar.sizeDelta = new Vector2(0f, CandyBar.rect.height);
//        Hide();
//        BattleState = BattleState.Hidden;
//        CandyUseText.gameObject.SetActive(false);
//    }

    public void Run()
    {
        Utils.Animate(1f, 0.5f, 0.3f, v => SpriteDarken.DarkValue = v,
            null, true);
        Utils.Animate(Rt.anchoredPosition, new Vector2(0f, _onScreen), 0.3f, v => Rt.anchoredPosition = v,
            null, true);
        PlayerComboText.rectTransform.anchoredPosition = Vector2.zero;
        BattleState = BattleState.PreStart;
        Utils.InvokeDelayed(() =>
        {
            BattleState = BattleState.Started;
            _pc.StartMoving();
        }, 0.7f);
    }

    public static bool TapDown()
    {
        if (Input.GetKeyDown(KeyCode.Space)) return true;
        if (Input.touchCount > 0)
        {
            for (var i = 0; i < Input.touchCount; ++i)
            {
                if (Input.GetTouch(i).phase != TouchPhase.Began) continue;
                return true;
            }
        }
        return false;
    }

    public void SetScale(float size, RectTransform scale)
    {
        if (scale == SubScale)
        {
            Size1 = size;
        }
        else
        {
            Size2 = size;
        }
        scale.sizeDelta = new Vector2(Rt.rect.width * size, scale.rect.height);
    }

    public void CutSubscale(float pos)
    {
        if (pos > SubScale.position.x + SubScale.rect.width / 2 || pos < SubScale.position.x - SubScale.rect.width / 2)
        {
            return;
        }
        float newSize, newPos;
        if (pos > SubScale.position.x)
        {
            newSize = SubScale.rect.width / 2 + pos - SubScale.position.x;
            var sizeDelta = SubScale.rect.width - newSize;
            newPos = SubScale.anchoredPosition.x - sizeDelta / 2;
        }
        else
        {
            newSize = SubScale.rect.width / 2 + SubScale.position.x - pos;
            var sizeDelta = SubScale.rect.width - newSize;
            newPos = SubScale.anchoredPosition.x + sizeDelta / 2;
        }
        SubScale.sizeDelta = new Vector2(newSize, SubScale.rect.height);
        SubScale.anchoredPosition = new Vector2(newPos, SubScale.anchoredPosition.y);
    }
}