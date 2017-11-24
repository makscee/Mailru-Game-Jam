public class Scores
{
    public static float ScorePlayer, ScoreEnemy;

    public static void AddScorePlayer(float score)
    {
        Utils.Animate(0, score, 1f, (v) => ScorePlayer += v);
    }
    
    public static void AddScoreEnemy(float score)
    {
        Utils.Animate(0, score, 1f, (v) => ScoreEnemy += v);
    }
}