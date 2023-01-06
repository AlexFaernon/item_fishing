using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

public class EnemyAI : MonoBehaviour
{
    private static readonly Dictionary<GameObject, Enemy> Enemies = new();
    private static readonly Random Random = new();
    private static bool _canAttack;

    public static Enemy GetEnemy(GameObject gameObject) => Enemies[gameObject];
    public static void AddEnemy(Enemy enemy)
    {
        if (!Enemies.ContainsValue(enemy))
        {
            Enemies[enemy.gameObject] = enemy;
        }
        else
        {
            throw new ArgumentException("Enemy already presenting in dictionary");
        }
    }

    public static void RemoveEnemy(Enemy enemy)
    {
        Enemies.Remove(enemy.gameObject);
    }

    private void Start()
    {
        StartCoroutine(RandomAttack());
        StartCoroutine(WaitBeforeFirstAttack());
    }

    private IEnumerator WaitBeforeFirstAttack()
    {
        yield return new WaitForSeconds(5);

        _canAttack = true;
    }

    private void Update()
    {
        if (!_canAttack || !Enemies.Any()) return;
        
        PrepareAndAttack();
    }

    private IEnumerator Attack(Enemy enemy1, Enemy enemy2)
    {
        enemy1.Attack(Ship.GetTarget(enemy1.CurrentSide));
        Debug.Log("enemy1 attack");
        yield return new WaitUntil(() => enemy1.IsReadyToAttack || enemy1.Health == 0);
        
        if (enemy2 is not null)
        {
            yield return new WaitForSeconds(8);
            enemy2.Attack(Ship.GetTarget(enemy2.CurrentSide));
            Debug.Log("enemy2 attack");
            yield return new WaitUntil(() => enemy2.IsReadyToAttack || enemy2.Health == 0);
        }
        
        yield return new WaitForSeconds(10);
        _canAttack = true;
    }

    private IEnumerator RandomAttack()
    {
        yield return new WaitForSeconds(30);
        if (Random.Next(10) >= 7)
        {
            var enemy = GetRandomEnemy();
            enemy.Attack(Ship.GetTarget(enemy.CurrentSide));
            Debug.Log("random enemy attack");
        }

        if (Enemies.Any())
        {
            StartCoroutine(RandomAttack());
        }
    }

    private void PrepareAndAttack()
    {
        var enemy1 = GetRandomEnemy();
        Enemy enemy2 = null;
        if (Enemies.Count > 1)
        {
            enemy2 = GetRandomEnemy(enemy1);
        }
        _canAttack = false;
        StartCoroutine(Attack(enemy1, enemy2));
    }

    private Enemy GetRandomEnemy(Enemy other = null)
    {
        var list = Enemies.Values.Where(enemy => enemy.IsReadyToAttack && enemy != other).ToList();
        return list[Random.Next(list.Count)];
    }
}