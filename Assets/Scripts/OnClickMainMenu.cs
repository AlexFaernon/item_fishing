using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnClickMainMenu : MonoBehaviour
{
    private void Awake()
    {
        TutorialManager.TutorialEnabled =
            Convert.ToBoolean(PlayerPrefs.GetInt(nameof(TutorialManager.TutorialEnabled), 1));
    }

    public void OnClickStart()
    {
        SceneManager.LoadScene("Levels");
    }
    
    public void OnClickSettings()
    {
        SceneManager.LoadScene("Settings");
    } 
    
    public void OnClickExit()
    {
        Application.Quit();
    }
}
