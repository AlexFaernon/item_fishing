using System;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private GameObject hook;
    private LineRenderer line;

    private void Awake()
    {
        EventAggregator.ModeSwitched.Subscribe(SetActiveOnMod);
    }

    private void Start()
    {
        line = GetComponent<LineRenderer>();
    }

    void Update()
    {
        line.SetPosition(0, transform.position);
        line.SetPosition(1, hook.transform.position);
        if (Hook.IsLaunched || Hook.IsRetracting) return;
        TurnToMouse();
    }

    private void TurnToMouse()
    {
        var mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition); //положение мыши из экранных в мировые координаты
        var angle = Vector2.Angle(Vector2.right, mousePosition - transform.position);//угол между вектором от объекта к мыше и осью х
        transform.eulerAngles = new Vector3(0f, 0f, transform.position.y < mousePosition.y ? angle : -angle);//немного магии на последок
    }

    private void SetActiveOnMod(Mode mode)
    {
        switch (mode)
        {
            case Mode.Fishing:
                transform.parent.gameObject.SetActive(true);
                break;
            case Mode.Player:
                transform.parent.gameObject.SetActive(false);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(mode), mode, null);
        }
    }

    private void OnDestroy()
    {
        EventAggregator.ModeSwitched.Unsubscribe(SetActiveOnMod);
    }
}
