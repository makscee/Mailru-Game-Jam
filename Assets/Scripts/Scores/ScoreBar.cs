using System;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBar : MonoBehaviour
{
    public bool IsPlayer;
    public RectTransform Rt;
    public RawImage Image;
    private const float MoodBarMax = 3.392f;

    private void Update()
    {
        var score = IsPlayer ? Scores.ScorePlayer : Scores.ScoreEnemy;
        float scale;
        scale = score < 1 ? 0f : Math.Max(0f, 1f - 1f / (score / 2 + 100) * 100);
        Rt.sizeDelta = new Vector2(Rt.rect.width, MoodBarMax * scale);
        Image.color = Color.Lerp(Color.yellow, Color.green, scale);
    }
}