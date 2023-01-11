using UnityEngine;
using UnityEngine.SceneManagement;

public class OnClickSettings : MonoBehaviour
{
    public void OnClickBack()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void CloseSettingsInGame()
    {
        transform.parent.parent.gameObject.SetActive(false);
    }
}
