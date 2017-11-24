using System;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour
{
    public Text Text;
    public bool IsPlayer;

    private void Update()
    {
        var score = IsPlayer ? Scores.ScorePlayer : Scores.ScoreEnemy;
        float scale;
        scale = score < 1 ? 0f : Math.Max(0f, 1f - 1f / (score + 100) * 100);
        Text.fontSize = 50 + (int) Math.Round(scale * 50, 0);
        Text.text = Math.Round(score, 0).ToString(CultureInfo.InvariantCulture);
    }
}