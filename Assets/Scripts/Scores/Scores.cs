using UnityEngine;

public class Scores
{
    public static float ScorePlayer, ScoreEnemy;

    public static void AddScorePlayer(float score)
    {
        Utils.Animate(0, score, 0.5f, (v) => ScorePlayer += v);
    }
    
    public static void AddScoreEnemy(float score)
    {
        Utils.Animate(0, score, 0.5f, (v) => ScoreEnemy += v);
    }
}