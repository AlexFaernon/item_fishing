using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ExitConfirmWindow : MonoBehaviour
{
    [SerializeField] private Button yes;
    [SerializeField] private Button no;

    private void Awake()
    {
        yes.onClick.AddListener(() => SceneManager.LoadScene("Levels"));
        no.onClick.AddListener(() => gameObject.SetActive(false));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameObject.SetActive(false);
        }
    }
}
