using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityRandom = UnityEngine.Random;

public class SpawnerGeneric : MonoBehaviour
{
    [Tooltip("Use Collider to spawn objects or empty GameObjects")]
    public bool useCollider = false;

    [SerializeField]
    public List<GameObject> spawnPositions;

    public bool contactPlayerDespawn = true;
    public bool lifeTimeDespawn;
    [Tooltip("Make Object has lifeTime")]
    [Range(0.1f, 999.0f)]
    public float objectsLifeTime;

    [Space]

    [Tooltip("Model to spawn")]
    public GameObject model;
    [Range(0.1f, 100.0f)]
    public float TimeBetweenSpawn;
    [Range(1, 999)]
    public int elementsEachSpawn = 1;
    [Range(1, 1000)]
    public int maxElems;

    [Space]
    public UnityEvent OnDespawnCallback;

    private Queue<Spawned> pool;
    private Dictionary<int, Spawned> spawnedObjects;
    [HideInInspector] public bool keepSpawning;

    private void Awake()
    {
        pool = new Queue<Spawned>(maxElems);
        spawnedObjects = new Dictionary<int, Spawned>(maxElems);
        //Aqui iria un switch case con los tipos de elementos spawneables
        if(contactPlayerDespawn)
        {
            model.AddComponent<SpanwedOnPlayerCollision>();
        }
        else
        {

        }
        model.GetComponent<Spawned>().mySpawner = this;

        for (int i = 0; i< maxElems; i++)
        {
            GameObject obj = Instantiate(model);
            obj.SetActive(false);
            obj.transform.position = this.transform.position;
            pool.Enqueue(obj.GetComponent<Spawned>());
        }
    }

    void Start()
    {
        Init();
    }
    void Init()
    {
        keepSpawning = true;
        StartCoroutine(StartSpawning());
    }

    public IEnumerator StartSpawning()
    {
        float timer = 0.0f;
        while(keepSpawning)
        {
            timer += Time.deltaTime;
            if(timer >= TimeBetweenSpawn)
            {
                timer -= TimeBetweenSpawn;
                for(int i = 0; i< elementsEachSpawn; i++)
                {
                    bool hasSpawned = SpawnObject();
                }
            }

            yield return null;
        }
    }

    public bool DespawnObject(Spawned target)
    {
        bool objectExists = spawnedObjects.Remove(target.GetInstanceID());
        if (objectExists)
        {
            target.gameObject.SetActive(false);
            target.gameObject.transform.position = Vector3.zero;
            OnDespawnCallback.Invoke();
        }
        return objectExists;
    }

    bool SpawnObject()
    {
        try
        {
            Vector3 pos = SelectSpawnPos();
            Spawned spawned = pool.Dequeue();
            spawned.transform.parent = null;
            spawned.transform.position = pos;
            spawnedObjects.Add(spawned.GetInstanceID(), spawned);

            //init object
            spawned.gameObject.SetActive(true);
            if (lifeTimeDespawn) {
                InitLifeTime(spawned as SpanwedLifeTimeDespawn);
            }
            return true;
        }
        catch (Exception e)
        {
            Debug.LogWarning($"{e.Message} in SpawnerGeneric, trying to spawn objects");
        }
        return false;
    }

    Vector3 SelectSpawnPos()
    {
        int index = UnityRandom.Range(0, spawnPositions.Count-1);
        return spawnPositions[index].transform.position;
    }

    #region ObjectsLifeTime
    public void InitLifeTime(SpanwedLifeTimeDespawn spawnedObj)
    {
        StartCoroutine(AliveCoroutine(spawnedObj));
    }

    private IEnumerator AliveCoroutine(SpanwedLifeTimeDespawn spawnedObj)
    {
        float timer = 0.0f;
        while (timer < objectsLifeTime)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        DespawnObject(spawnedObj);
    }

    #endregion ObjectsLifeTime

}

public abstract class Spawned : MonoBehaviour
{
    public SpawnerGeneric mySpawner = null;

}
public class SpanwedLifeTimeDespawn : Spawned
{

}
public class SpanwedOnPlayerCollision: Spawned
{
    public void Awake()
    {
 
    }

    public void Start()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //pickUpEffect.Apply(collision.gameObject);
            mySpawner.DespawnObject(this);
        }
    }
 

}