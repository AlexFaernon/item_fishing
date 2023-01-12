using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private GameObject item;
    [SerializeField] private List<Sprite> metalSprites;
    [SerializeField] private List<Sprite> electronicsSprites;
    private static readonly System.Random Random = new();
    private Sprite GetRandomMetalSprite => metalSprites[Random.Next(metalSprites.Count)];
    private Sprite GetRandomElectronicsSprite => electronicsSprites[Random.Next(electronicsSprites.Count)];
    private void Awake()
    {
        for (var i = 0; i < LoadedData.MetalCount; i++)
        {
            var obj = SpawnItem();
            obj.tag = "Metal";
            var spriteRenderer = obj.GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = GetRandomMetalSprite;
            obj.GetComponent<BoxCollider2D>().size = spriteRenderer.sprite.bounds.size;
        }

        for (var i = 0; i < LoadedData.ElectronicsCount; i++)
        {
            var obj = SpawnItem();
            obj.tag = "Electronics";
            var spriteRenderer = obj.GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = GetRandomElectronicsSprite;
            obj.GetComponent<BoxCollider2D>().size = spriteRenderer.sprite.bounds.size;
            obj.transform.GetChild(0).gameObject.GetComponent<Light>().color = Color.green;
        }
    }

    private GameObject SpawnItem()
    {
        var rect = GetComponent<RectTransform>().rect;
        float x = 0;
        float y = 0;
        while (x is > -20 and < 20 && y is > -15 and < 15)
        {
            x = UnityEngine.Random.Range(-rect.width / 2, rect.width / 2);
            y = UnityEngine.Random.Range(-rect.height / 2, rect.height / 2);
        }
        return Instantiate(item, new Vector3(x, y, 0) + transform.position, new Quaternion());
    }
}
