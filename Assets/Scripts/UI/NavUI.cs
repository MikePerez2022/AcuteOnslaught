using Unity.VisualScripting;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class NavUI : MonoBehaviour
{
    [Header("UI Screens:")]
    [SerializeField] GameObject StartScreen;
    [SerializeField] GameObject GameScreen;
    [SerializeField] GameObject DeathScreen;
    [SerializeField] GameObject SettingsScreen;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnStartBtnPressed()
    {
        StartScreen.SetActive(false);
        GameScreen.SetActive(true);
        print("Start Btn worked");
    }

    public void OnSettingsBtnPressed()
    {
        SettingsScreen.SetActive(true);
        print("Settings Btn worked");
    }

    public void OnSaveBtnPressed()
    {
        print("Save Btn worked");
    }

    public void OnBackBtnPressed()
    {
        SettingsScreen.SetActive(false);
        print("Back Btn worked");
    }

    public void OnQuitBtnPressed()
    {
        print("Quit Btn worked");

        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
