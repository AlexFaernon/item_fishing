using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnClickMonetization : MonoBehaviour
{
    public void OnClickBack()
    {
        SceneManager.LoadScene("Levels");
    }
    
    public void OnClickViewAd()
    {
        // todo запустить рекламку начислить ресурсы;
    } 
    
    public void OnClickStart()
    {
        // todo в бой
    }
}
