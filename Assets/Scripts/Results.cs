using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Results : MonoBehaviour
{
    public Text PlayerCombo, EnemyCombo;
    public GameObject Continue;

    public void Run()
    {
        PlayerCombo.text = "x0";
        EnemyCombo.text = "x0";
        Utils.InvokeDelayed(() =>
        {
            gameObject.SetActive(true);
            StartCoroutine(ComboCount());
        }, 0.5f);
    }

    private IEnumerator ComboCount()
    {
        var pScore = 0;
        var eScore = 0;
        const int fontIncrease = 10;
        do
        {
            yield return new WaitForSeconds(0.2f);
            if (pScore < Scores.ScorePlayer)
            {
                pScore++;
                Effects.ExplosionEffect(PlayerCombo.transform.position, PlayerCombo.color, 25);
                PlayerCombo.fontSize = Math.Min(150, PlayerCombo.fontSize + fontIncrease);
            }
            if (eScore < Scores.ScoreEnemy)
            {
                eScore++;
                Effects.ExplosionEffect(EnemyCombo.transform.position, EnemyCombo.color, 25);
                EnemyCombo.fontSize = Math.Min(150, EnemyCombo.fontSize + fontIncrease);
            }
            PlayerCombo.text = "x" + pScore;
            EnemyCombo.text = "x" + eScore;
        } while (pScore < Scores.ScorePlayer || eScore < Scores.ScoreEnemy);
        yield return new WaitForSeconds(0.5f);
        Continue.SetActive(true);
        while (true)
        {
            if (ScaleController.TapDown())
            {
                SpriteDarken.Reset();
                SceneManager.LoadScene("Battle");
            }
            yield return null;
        }
    }
}