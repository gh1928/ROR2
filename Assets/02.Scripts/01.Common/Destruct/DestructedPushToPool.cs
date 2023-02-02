using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructedPushToPool : MonoBehaviour, IDestructible
{
    public NormalEnemyClasses enemyClass;
    private EnemySpawner enemySpawner;
    private LinkedList<GameObject> enemyPool;
    private void Start()
    {
        enemySpawner = EnemySpawner.Instance;        
        enemyPool = enemySpawner.EnemyPools[(int)enemyClass];
    }
    public void OnDestruction()
    {
        enemyPool.AddLast(gameObject);
    }
}
