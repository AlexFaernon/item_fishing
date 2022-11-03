using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

public class EnemyAI : MonoBehaviour
{
    private static readonly Dictionary<GameObject, Enemy> Enemies = new();
    private static readonly Random _random = new();
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
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G) && _canAttack)
        {
            Attack();
            _canAttack = false;
            StartCoroutine(WaitToAttack());
        }
    }

    private static IEnumerator WaitToAttack()
    {
        yield return new WaitForSeconds(3);

        _canAttack = true;
        Debug.Log("attack now");
    }

    private static void Attack()
    {
        var groupBySector = Enemies.Values.GroupBy(enemy => enemy.CurrentSector).ToList();
        var skipTo = _random.Next(groupBySector.Count);
        var sector = groupBySector.Skip(skipTo).First();
        Debug.Log(sector.Key.ToString());
        var enemyList = sector.ToList();
        enemyList[_random.Next(enemyList.Count)].Attack();
    }
}