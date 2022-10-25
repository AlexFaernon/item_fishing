using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] private Transform target;

    private void Update()
    {
        transform.position = (Vector2)target.position;
    }
}
