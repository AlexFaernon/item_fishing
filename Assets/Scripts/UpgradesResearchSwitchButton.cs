using UnityEngine;
using UnityEngine.UI;

public class UpgradesResearchSwitchButton : MonoBehaviour
{
    [SerializeField] private GameObject setActive;

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        transform.parent.gameObject.SetActive(false);
        setActive.SetActive(true);
    }
}
