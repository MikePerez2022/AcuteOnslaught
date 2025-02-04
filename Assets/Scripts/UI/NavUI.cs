using Unity.VisualScripting;
using UnityEngine;
using UnityEditor;

// Texture2D StartScreen;

public class NavUI : MonoBehaviour
{
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

        print("Start Btn worked");
    }

    public void OnQuitBtnPressed()
    {
        Application.Quit();
        print("Quit Btn worked");
    }
}
