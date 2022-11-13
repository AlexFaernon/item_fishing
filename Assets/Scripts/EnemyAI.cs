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
    private static bool _canAttack = true;

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
    
    private void Update()
    {
        if (!_canAttack || !Enemies.Any()) return;
        
        Attack();
        _canAttack = false;
        StartCoroutine(WaitToAttack());
    }

    private static IEnumerator WaitToAttack()
    {
        yield return new WaitForSeconds(5);

        _canAttack = true;
        Debug.Log("attack now");
    }

    private static void Attack()
    {
        // var groupBySide = Enemies.Values.GroupBy(enemy => enemy.CurrentSide).ToList();
        // var skipTo = _random.Next(groupBySide.Count);
        // var side = groupBySide.Skip(skipTo).First();
        //
        // Debug.Log(side.Key.ToString());
        //
        // var enemyList = side.ToList();
        var enemy = Enemies.Values.ToList()[Random.Next(Enemies.Count)];
        var target = Ship.GetTarget(enemy.CurrentSide);
        enemy.Attack(target);

    }
}