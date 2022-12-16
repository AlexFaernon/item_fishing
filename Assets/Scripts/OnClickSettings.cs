using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnClickSettings : MonoBehaviour
{
    public void OnClickBack()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
