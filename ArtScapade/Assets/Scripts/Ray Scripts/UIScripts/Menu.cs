using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public string FirstLevel;
    public string SettingsMenu;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        SceneManager.LoadScene(FirstLevel);         //loads scene you designate in string
    }

    public void QuitGame()
    {
        Application.Quit();                         //Exits game
    }
    
    public void Settings()
    {
        SceneManager.LoadScene(SettingsMenu);         //loads scene you designate in string
    }
}
