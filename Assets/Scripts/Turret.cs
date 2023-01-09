using System;
using System.Collections;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] private GameObject turret;
    private TurretBody turretBody;
    private Enemy currentTarget;
    private LineRenderer lineRenderer;
    private bool canShoot = true;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        turretBody = turret.GetComponent<TurretBody>();
    }

    private void Update()
    {
        if (currentTarget == null) return;
        
        var enemyPosition = currentTarget.gameObject.transform.position;
        var position = turret.transform.position;
        var angle = Vector2.Angle(Vector2.right, enemyPosition - position);
        turret.transform.eulerAngles = new Vector3(0f, 0f, position.y < enemyPosition.y ? angle : -angle);

        if (!canShoot) return;
        
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, currentTarget.transform.position);
        currentTarget.TakeDamage(turretBody.TurretClass.Damage);
        canShoot = false;
        StartCoroutine(DestroyLineAfterTime());
        StartCoroutine(WaitToShoot());
    }
    
    private IEnumerator WaitToShoot()
    {
        yield return new WaitForSeconds(PowerUps.TurretsBoost ? 1.5f : 3f);

        canShoot = true;
        //Debug.Log("shoot now");
    }

    private IEnumerator DestroyLineAfterTime()
    {
        yield return new WaitForSeconds(0.5f);
        
        lineRenderer.SetPosition(1, transform.position);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Enemy")) return;

        var enemy = EnemyAI.GetEnemy(other.gameObject);
        if (currentTarget == null)
        {
            currentTarget = enemy;
            //Debug.Log("First enemy");
        }
        else if (currentTarget.Health > enemy.Health)
        {
            currentTarget = enemy;
            //Debug.Log("New enemy");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (currentTarget != null && other.gameObject == currentTarget.gameObject)
        {
            currentTarget = null;
        }
    }
}
