using System;
using System.Collections;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] private GameObject turret;
    private Enemy _currentTarget;
    private LineRenderer _lineRenderer;
    private bool _canShoot = true;

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        if (_currentTarget == null) return;
        
        var enemyPosition = _currentTarget.gameObject.transform.position;
        var position = turret.transform.position;
        var angle = Vector2.Angle(Vector2.right, enemyPosition - position);
        turret.transform.eulerAngles = new Vector3(0f, 0f, position.y < enemyPosition.y ? angle : -angle);

        if (!_canShoot) return;
        
        _lineRenderer.SetPosition(0, transform.position);
        _lineRenderer.SetPosition(1, _currentTarget.transform.position);
        _currentTarget.TakeDamage(1);
        _canShoot = false;
        StartCoroutine(DestroyLineAfterTime());
        StartCoroutine(WaitToShoot());
    }
    
    private IEnumerator WaitToShoot()
    {
        yield return new WaitForSeconds(1.5f);

        _canShoot = true;
        //Debug.Log("shoot now");
    }

    private IEnumerator DestroyLineAfterTime()
    {
        yield return new WaitForSeconds(0.5f);
        
        _lineRenderer.SetPosition(1, transform.position);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Enemy")) return;

        var enemy = EnemyAI.GetEnemy(other.gameObject);
        if (_currentTarget == null)
        {
            _currentTarget = enemy;
            //Debug.Log("First enemy");
        }
        else if (_currentTarget.Health > enemy.Health)
        {
            _currentTarget = enemy;
            //Debug.Log("New enemy");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == _currentTarget.gameObject)
        {
            _currentTarget = null;
        }
    }
}
