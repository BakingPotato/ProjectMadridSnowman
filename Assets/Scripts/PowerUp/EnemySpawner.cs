using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<GameObject> ranged;
    [SerializeField] List<GameObject> melee;
    [SerializeField] int maxEnemyCount;

    public enum EnemyType
    {
        Melee, Ranged
    }

    [System.Serializable]
    public struct Enemy
    {
        public EnemyType type;
        public float weight;
    }

    [SerializeField] protected Enemy[] enemyArray;

    public bool isSpawning { get { return spawning != null; } }
    Coroutine spawning = null;

    [SerializeField] float initial_waitTime;
    [SerializeField] float waitTime;

    // Start is called before the first frame update
    void Start()
    {
        spawning = StartCoroutine(spawnEnemy(initial_waitTime));
    }

    // Update is called once per frame
    void Update()
    {
        if(!isSpawning)
        {
            //Paramos la corrutina por si entro uno antes y empezamos
            spawning = StartCoroutine(spawnEnemy(waitTime));
        }
    }

    IEnumerator spawnEnemy(float time)
    {
        yield return new WaitForSeconds(time);

        SpawnRandomEnemy();
        spawning = null;
    }

    private void SpawnRandomEnemy()
    {
        if (transform.childCount >= maxEnemyCount)
            return;
        EnemyType randomType = GetRandomEnemyType();
        GameObject enemy = null;
        switch (randomType)
        {
            case EnemyType.Ranged:
                enemy = Instantiate(ranged[Random.Range(0, ranged.Count)], transform.position, Quaternion.identity);
                break;
            case EnemyType.Melee:
                enemy = Instantiate(melee[Random.Range(0, melee.Count)], transform.position, Quaternion.identity);
                break;
        }
        if (enemy)
            enemy.transform.parent = transform;
    }

    protected EnemyType GetRandomEnemyType()
    {
        float totalWeight = 0;
        foreach (Enemy loot in enemyArray)
        {
            totalWeight += loot.weight;
        }

        float p = Random.Range(0, totalWeight);
        float runningTotal = 0;

        foreach (Enemy enemy in enemyArray.Reverse())
        {
            runningTotal += enemy.weight;
            if (p < runningTotal) return enemy.type;
        }

        return EnemyType.Melee;
    }
}
