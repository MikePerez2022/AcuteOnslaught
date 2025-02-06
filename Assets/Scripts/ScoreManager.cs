using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public float currentScore;
    public TMP_Text currentScoreText; // Can be removed if we'll only display the final highscore
    public TMP_Text finalScoreText;
    public TMP_Text highestScoreText;

    // Call whenever we'll calculate/update score: Wave Ended, Time Intervals, etc.
    public void ChangeScore(int points)
    {
        currentScore += points;
        currentScoreText.text = currentScore.ToString();
    }

    // Call at end of the game
    public void UpdateHighScore()
    {
        if (PlayerPrefs.HasKey("HighestScore"))
        {
            if (currentScore > PlayerPrefs.GetFloat("HighestScore"))
            {
                PlayerPrefs.SetFloat("HighestScore", currentScore);
            }
        }
        else
        {
            PlayerPrefs.SetFloat("HighestScore", currentScore);
        }

        finalScoreText.text = currentScore.ToString();
        highestScoreText.text = PlayerPrefs.GetFloat("HighestScore").ToString();
    }
}
