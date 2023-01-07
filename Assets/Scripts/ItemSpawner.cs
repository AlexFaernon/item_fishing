using UnityEngine;
using Random = UnityEngine.Random;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private GameObject item;
    [SerializeField] private int metalCount;
    [SerializeField] private int electronicsCount;
    private void Awake() //todo item counter
    {
        for (var i = 0; i < LoadedData.MetalCount; i++)
        {
            SpawnItem().tag = "Metal";
        }

        for (var i = 0; i < LoadedData.ElectronicsCount; i++)
        {
            var obj = SpawnItem();
            obj.tag = "Electronics";
            obj.GetComponent<SpriteRenderer>().color = Color.green;
        }
    }

    private GameObject SpawnItem()
    {
        var rect = GetComponent<RectTransform>().rect;
        var x = Random.Range(-rect.width / 2, rect.width / 2);
        var y = Random.Range(-rect.height / 2, rect.height / 2);
        return Instantiate(item, new Vector3(x, y, 0) + transform.position, new Quaternion());
    }
}
