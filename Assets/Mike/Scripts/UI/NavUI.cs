using Unity.VisualScripting;
using System;
using TMPro;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class NavUI : MonoBehaviour
{
    [Header("UI Screens:")]
    [SerializeField] GameObject StartScreen;
    [SerializeField] GameObject GameScreen;
    [SerializeField] GameObject DeathScreen;
    [SerializeField] GameObject PauseScreen;
    [SerializeField] TMP_Text Highscore;
    [SerializeField] GameObject SettingsScreen;
    [SerializeField] ScoreManager EnemySystems;

    [Header("Game:")]
    [SerializeField] GameObject Player;
    [SerializeField] GameObject A_Music;
    [SerializeField] GameObject M_Music;
    [SerializeField] GameObject D_Music;
    [SerializeField] WaveSystem Spawner;
    [SerializeField] Slider HealthSlider;
    [SerializeField] bool test;

    [Header("Audio:")]
    [SerializeField] List<AudioSource> AudioList;

    private bool isPaused = false;
    private bool isDead = false;

    void Start()
    {
        if (PlayerPrefs.HasKey("HighestScore"))
        {
            Highscore.text = "Highscore: " + PlayerPrefs.GetFloat("HighestScore").ToString();
        }
    }


    void Update()
    {
        if (Player.IsDestroyed() && !isDead)
        {
            EnemySystems.UpdateHighScore();
            Spawner.enabled = false;
            DeathScreen.SetActive(true);
            A_Music.SetActive(false);
            D_Music.SetActive(true);
            PlaySound(2);
            isDead = true;
            Time.timeScale = 0.0f;
        }
        else
        {
            HealthSlider.value = Player.GetComponent<Health>().health / (float)100;
        }
        if (test) DeathScreen.SetActive(true);
    }

    public void OnStartBtnPressed()
    {
        StartScreen.SetActive(false);
        GameScreen.SetActive(true);
        Player.SetActive(true);
        Spawner.enabled = true;
        A_Music.SetActive(true);
        M_Music.SetActive(false);
        PlaySound(0);
        Time.timeScale = 1.0f;
    }

    public void OnSettingsBtnPressed()
    {
        SettingsScreen.SetActive(true);
        PlaySound(0);
    }

    public void OnSaveBtnPressed()
    {
        SettingsScreen.GetComponent<SettingsUI>().OnSave();
        PlaySound(1);
    }

    public void OnBackBtnPressed()
    {
        SettingsScreen.SetActive(false);
        PlaySound(0);
    }

    public void OnRestartBtnPressed()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        PlaySound(0);
    }

    public void OnPauseBtnPressed()
    {
        isPaused = !isPaused;
        Spawner.enabled = !isPaused;
        Time.timeScale = (isPaused) ? 0.0f : 1.0f;
        PauseScreen.SetActive(isPaused);

        PlaySound(0);
    }

    public void OnQuitBtnPressed()
    {
        PlaySound(0);

        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                    Application.Quit();
        #endif
    }

    private void PlaySound(int ind)
    {
        AudioList[ind].Play();
    }
}
