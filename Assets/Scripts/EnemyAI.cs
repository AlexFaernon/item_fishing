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
    public static int EnemyCount => Enemies.Count;
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
        yield return new WaitForSeconds(4);

        _canAttack = true;
    }

    private void Update()
    {
        if (!_canAttack || !Enemies.Any()) return;
        
        PrepareAndAttack();
    }

    private IEnumerator Attack(Enemy enemy1, Enemy enemy2)
    {
        var target1 = Ship.GetTarget(enemy1.CurrentSide);
        target1.SendMessage(nameof(TurretBody.WarningLight), true, SendMessageOptions.DontRequireReceiver);
        enemy1.Attack(target1);
        
        yield return new WaitUntil(() => enemy1.IsReadyToAttack || enemy1.Health <= 0);
        target1.SendMessage(nameof(TurretBody.WarningLight), false, SendMessageOptions.DontRequireReceiver);
        
        if (enemy2 is not null)
        {
            yield return new WaitForSeconds(5);
            
            var target2 = Ship.GetTarget(enemy2.CurrentSide);
            target2.SendMessage(nameof(TurretBody.WarningLight), true, SendMessageOptions.DontRequireReceiver);
            enemy2.Attack(target2);
            
            yield return new WaitUntil(() => enemy2.IsReadyToAttack || enemy2.Health <= 0);
            target2.SendMessage(nameof(TurretBody.WarningLight), false, SendMessageOptions.DontRequireReceiver);
        }
        
        yield return new WaitForSeconds(7f);
        _canAttack = true;
    }

    private IEnumerator RandomAttack()
    {
        yield return new WaitForSeconds(14);
        if (Random.Next(10) >= 6)
        {
            var enemy = GetRandomEnemy();
            var target = Ship.GetTarget(enemy.CurrentSide);
            target.SendMessage(nameof(TurretBody.WarningLight), true, SendMessageOptions.DontRequireReceiver);
            enemy.Attack(target);
            yield return new WaitUntil(() => enemy.IsReadyToAttack || enemy.Health <= 0);
            target.SendMessage(nameof(TurretBody.WarningLight), false, SendMessageOptions.DontRequireReceiver);
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
        return list.Count == 0 ? null : list[Random.Next(list.Count)];
    }
}