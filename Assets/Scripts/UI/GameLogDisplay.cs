using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameLogDisplay : MonoBehaviour
{
    [SerializeField] private GameObject[] diamonds;
    [SerializeField] private TMP_Text title;

    [SerializeField] private TMP_Text enemyCount;
    [SerializeField] private TMP_Text appleCount;
    [SerializeField] private TMP_Text bananaCount;
    [SerializeField] private TMP_Text melonCount;

    [SerializeField] private TMP_Text bonus;
    [SerializeField] private TMP_Text score;

    [SerializeField] private TMP_Text timeRemain;
    [SerializeField] private TMP_Text finalScore;
    [SerializeField] private Button buttonNext;
    private void SetEnemyCountText(int value)
    {
        enemyCount.text = value.ToString();
    }
    private void SetAppleCountText(int value)
    {
        appleCount.text = value.ToString();
    }
    private void SetBananaCountText(int value)
    {
        bananaCount.text = value.ToString();
    }
    private void SetMelonCountText(int value)
    {
        melonCount.text = value.ToString();
    }
    private void SetBonusText(int value)
    {
        bonus.text = "Bonus: " + value.ToString();
    }
    private void SetTimeRemainText(int value)
    {
        timeRemain.text = "Time Remain: " + value.ToString();
    }
    private void SetScoreText(int value)
    {
        score.text = "Score: " + value.ToString();
    }
    private void SetFinalScoreText(int value)
    {
        finalScore.text = "Final Score: " + value.ToString();
    }
    private void SetDiamondDisplay(int value)
    {
        while (value > 0)
        {
            diamonds[value - 1]?.SetActive(true);
            value--;
        }
    }
    public void DisplayAchivement(Achivement achivement, bool isFinish)
    {
        if (SceneManager.GetActiveScene().name == "Level3")
        {
            buttonNext.interactable = false;
        }
        if (isFinish)
        {
            title.text = "LEVEL COMPLETE!";
        }
        else
        {
            title.text = "GAME OVER";
            buttonNext.interactable = false;
        }

        SetDiamondDisplay(achivement.diamonds);
        SetEnemyCountText(achivement.kills);
        SetAppleCountText(achivement.appleCount);
        SetBananaCountText(achivement.bananaCount);
        SetMelonCountText(achivement.melonCount);
        SetTimeRemainText(achivement.timeRemain);
        SetScoreText(achivement.score);
        SetBonusText(achivement.GetBonusScore());
        SetFinalScoreText(achivement.GetFinalScore());
    }
}
