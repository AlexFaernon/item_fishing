using UnityEngine;
using Random = UnityEngine.Random;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private GameObject item;
    private static System.Random _random = new();
    private void Awake() //todo item counter
    {
        var coord = Random.insideUnitCircle;
        var rect = GetComponent<RectTransform>().rect;
        var itemInstantiate = Instantiate(item, new Vector3(coord.x * rect.width / 2, coord.y * rect.height / 2, 0) + transform.position,
            new Quaternion());
        if (_random.Next(10) < 8)
        {
            itemInstantiate.tag = "Metal";
        }
        else
        {
            itemInstantiate.tag = "Electronics";
            itemInstantiate.GetComponent<SpriteRenderer>().color = Color.green;
        }
    }
}
