using System;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private int currentScore = 0;
    public TMP_Text currentScoreText; // Can be removed if we'll only display the final highscore
    public TMP_Text finalScoreText;
    public TMP_Text highestScoreText;

    // Call whenever we'll calculate/update score: Wave Ended, Enemy Death, etc.
    // Need to discuss how/where multiplier will be stored/accessed
    public void ChangeScore(int points, float multiplier)
    {
        currentScore += (int)(points * multiplier); // Truncates after calculating score
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
