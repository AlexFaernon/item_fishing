using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnClickMainMenu : MonoBehaviour
{
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
