using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Results : MonoBehaviour
{
    public Text PlayerCombo, EnemyCombo;

    public void Run()
    {
        PlayerCombo.text = "x0";
        EnemyCombo.text = "x0";
        gameObject.SetActive(true);
        StartCoroutine(ComboCount());
    }

    private IEnumerator ComboCount()
    {
        var pScore = 0;
        var eScore = 0;
        yield return new WaitForSeconds(0.2f);
        do
        {
            yield return new WaitForSeconds(0.2f);
            if (pScore < Scores.ScorePlayer)
            {
                pScore++;
                Effects.ExplosionEffect(PlayerCombo.transform.position, PlayerCombo.color, 25);
            }
            if (eScore < Scores.ScoreEnemy)
            {
                eScore++;
                Effects.ExplosionEffect(EnemyCombo.transform.position, EnemyCombo.color, 25);
            }
            PlayerCombo.text = "x" + pScore;
            EnemyCombo.text = "x" + eScore;
        } while (pScore < Scores.ScorePlayer || eScore < Scores.ScoreEnemy);
    }
}