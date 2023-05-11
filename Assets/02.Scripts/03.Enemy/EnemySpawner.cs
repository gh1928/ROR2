using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    static private EnemySpawner instance;
    static public EnemySpawner Instance { get { return instance; } }
    public List<LinkedList<GameObject>> EnemyPools { get; private set; }
    public Transform[] spawnPotins;
    private int pointsCount;
    public GameObject[] prefabs;

    private float spawnTimer;
    public float spawnCycle = 10f;

    private void Awake()
    {
        instance = this;
        pointsCount = spawnPotins.Length;
        spawnTimer = spawnCycle / 2f;
        EnemyPools = new List<LinkedList<GameObject>>((int)NormalEnemyClasses.TotalCount);
        for(int i = 0; i < EnemyPools.Capacity; ++i)
        {
            EnemyPools.Add(new LinkedList<GameObject>());
        }
    }
    private void Update()
    {
        SpawnRandomEnemy();

        if (Input.GetKeyDown(KeyCode.G))
        {
            SpawnBison();
        }
        if(Input.GetKeyDown(KeyCode.H))
        {
            SpawnLemurian();
        }
    }
    private void SpawnRandomEnemy()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer < spawnCycle)
            return;

        spawnTimer = 0f;

        int index = Random.Range(0, (int)NormalEnemyClasses.TotalCount);
        var pool = EnemyPools[index];
        if (pool.Count > 0)
        {
            var enemy = pool.First.Value;
            enemy.SetActive(true);
            pool.RemoveFirst();
        }
        else
            Instantiate(prefabs[index], spawnPotins[Random.Range(0, pointsCount)].position, Quaternion.identity);
    }

    private void SpawnBison()
    {
        int index = (int)NormalEnemyClasses.Bison;
        var pool = EnemyPools[index];
        if (pool.Count > 0)
        {
            var enemy = pool.First.Value;
            enemy.SetActive(true);
            pool.RemoveFirst();
        }
        else
            Instantiate(prefabs[index]);
    }
    private void SpawnLemurian()
    {
        int index = (int)NormalEnemyClasses.Lemurian;
        var pool = EnemyPools[index];
        if (pool.Count > 0)
        {
            var enemy = pool.First.Value;
            enemy.SetActive(true);
            pool.RemoveFirst();
        }
        else
            Instantiate(prefabs[index]);
    }

}
