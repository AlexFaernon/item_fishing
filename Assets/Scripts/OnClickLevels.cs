using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnClickLevels : MonoBehaviour
{
    public void OnClickBack()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
