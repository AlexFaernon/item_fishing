using UnityEngine;

public class EscManager : MonoBehaviour
{
    [SerializeField] private GameObject research;
    [SerializeField] private GameObject upgrades;
    [SerializeField] private GameObject pause;

    private void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Escape)) return;
        
        if (research.activeSelf || upgrades.activeSelf)
        {
            research.SetActive(false);
            upgrades.SetActive(false);
        }
        else
        {
            pause.SetActive(!pause.activeSelf);
        }
    }
}
