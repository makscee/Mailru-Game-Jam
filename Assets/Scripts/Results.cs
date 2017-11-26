using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class Results : MonoBehaviour
{
    public static bool NewSuit = false;
    public Text VersusCombo, SoloCombo;

    public void Run()
    {
        VersusCombo.text = "x0";
        SoloCombo.text = "x0";
        Utils.InvokeDelayed(() =>
        {
            gameObject.SetActive(true);
            StartCoroutine(ComboCount());
        }, 0.5f);
    }

    private IEnumerator ComboCount()
    {
        var vScore = 0;
        var sScore = 0;
        const int fontIncrease = 10;
        do
        {
            yield return new WaitForSeconds(0.3f);
            if (vScore < Scores.ScorePlayer && vScore < Scores.ScoreEnemy)
            {
                vScore++;
                Effects.ExplosionEffect(VersusCombo.transform.position, 25);
//                VersusCombo.fontSize = Math.Min(105, VersusCombo.fontSize + fontIncrease);
            }
            if (sScore < Scores.ScoreEnemy || sScore < Scores.ScorePlayer)
            {
                sScore++;
                Effects.ExplosionEffect(SoloCombo.transform.position, 25);
//                SoloCombo.fontSize = Math.Min(105, SoloCombo.fontSize + fontIncrease);
            }
            VersusCombo.text = "x" + vScore;
            SoloCombo.text = "x" + sScore;
        } while (sScore < Scores.ScoreEnemy || sScore < Scores.ScorePlayer);
        yield return new WaitForSeconds(0.5f);
        while (true)
        {
            if (ScaleController.TapDown())
            {
                SpriteDarken.Reset();
                Scores.Combo = 0;
                Scores.ScorePlayer = 0;
                Scores.ScoreEnemy = 0;
                NewSuit = true;
                SceneManager.LoadScene("Room");
            }
            yield return null;
        }
    }
}