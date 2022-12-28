using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private EnemyAI enemyAI;
    [SerializeField] private GameObject ship;
    [SerializeField] private float radius;
    [SerializeField] private int enemyCount;
    
    private void Start() //todo enemy counter
    {
        enemyPrefab.GetComponent<Enemy>().ship = ship;
        for (var i = 0; i < enemyCount; i++)
        {
            Instantiate(enemyPrefab, GetPointOnCircle(), new Quaternion());
        }

        enemyAI.enabled = true;
    }

    private Vector3 GetPointOnCircle()
    {
        return (Vector3)Random.insideUnitCircle.normalized * radius + ship.transform.position;
    }
}
