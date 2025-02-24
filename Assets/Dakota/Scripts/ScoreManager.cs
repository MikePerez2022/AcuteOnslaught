using System;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    private int currentScore = 0;
    public float multiplier = 1f;
    [SerializeField] TMP_Text currentScoreText; // Can be removed if we'll only display the final highscore
    [SerializeField] TMP_Text finalScoreText;
    [SerializeField] TMP_Text highestScoreText;

    // Call whenever we'll calculate/update score: Wave Ended, Enemy Death, etc.
    // Need to discuss how/where multiplier will be stored/accessed
    public void ChangeScore(int points)
    {
        currentScore += (int)(points * multiplier); // Truncates after calculating score
        currentScoreText.text = "Score: " + currentScore.ToString();
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
        highestScoreText.text = "Highscore: " + PlayerPrefs.GetFloat("HighestScore").ToString();
    }
}
