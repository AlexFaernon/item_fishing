using UnityEngine;
using Random = UnityEngine.Random;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private GameObject item;
    private void Awake()
    {
        var coord = Random.insideUnitCircle;
        var rect = GetComponent<RectTransform>().rect;
        Instantiate(item, new Vector3(coord.x * rect.width / 2, coord.y * rect.height / 2, 0) + transform.position, new Quaternion());
    }
}
