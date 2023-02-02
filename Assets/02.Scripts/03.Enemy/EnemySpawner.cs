using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    static private EnemySpawner instance;
    static public EnemySpawner Instance { get { return instance; } }
    public List<LinkedList<GameObject>> EnemyPools { get; private set; }
    public GameObject[] prefabs;
    
    private void Awake()
    {
        instance = this;

        EnemyPools = new List<LinkedList<GameObject>>((int)NormalEnemyClasses.TotalCount);
        for(int i = 0; i < EnemyPools.Capacity; ++i)
        {
            EnemyPools.Add(new LinkedList<GameObject>());
        }
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.G))
        {
            SpawnBison();
        }
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

}
