using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterController : MonoBehaviour
{
    [Range(0.5f, 10.0f)]
    public float shotSpeed;

    [Range(1.0f, 10.0f)]
    public float reloadTime;

    [Range(0.5f, 5.0f)]
    public float proyectileTimeAlive;

    [Tooltip("Instance of the owner")]
    public GameObject owner;

    [Tooltip("Snow model prefab")]
    public GameObject snowballPrefab;

    [Range(1, 10)]
    public int poolSize;
    private Queue<GameObject> pool;

    void Start()
    {
        InitPool();
        StartCoroutine(StartInputDetection());
    }

    private void InitPool()
    {
        pool = new Queue<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject instance = Instantiate(snowballPrefab);
            instance.SetActive(false);
            pool.Enqueue(instance);
        }
    }

    private IEnumerator StartInputDetection()
    {
        while(true)
        {
            if(Input.GetMouseButtonDown(0))
            {
                LaunchProyectile();
            }
            yield return null;
        }
  
    }


    public bool LaunchProyectile()
    {
        try
        {
            GameObject proyectile = pool.Dequeue();
            proyectile.transform.rotation = Quaternion.identity;
            proyectile.SetActive(true);
            float eulerYRot = owner.transform.localRotation.eulerAngles.y;
           
            //if (eulerYRot < 0.0f) eulerYRot += 360.0f;
            Debug.Log("local euler" + owner.transform.localRotation.eulerAngles);
            Vector3 directorVector = new Vector3(Mathf.Cos(eulerYRot), 0, Mathf.Sin(eulerYRot));
            //position
            //Vector3 boundsSize = proyectile.GetComponent<Collider>().bounds.size;
            //Vector3 positionSpawnOffset = directorVector + new Vector3(boundsSize.x, boundsSize.y, boundsSize.z);
            //proyectile.transform.position = owner.transform.position + positionSpawnOffset;
            //Direction
            proyectile.GetComponent<Rigidbody>().velocity = directorVector.normalized * shotSpeed;


            StartCoroutine(AliveProyectile(proyectile));
            return true;

        }catch(InvalidOperationException e)
        {
            Debug.Log("there is no proyectile left");
            return false;
        }
    }

    private IEnumerator AliveProyectile(GameObject proyectile)
    {
        yield return new WaitForSeconds(proyectileTimeAlive);
        DespawnProyectile(proyectile);
    }

    private void DespawnProyectile(GameObject proyectile)
    {
        proyectile.SetActive(false);
        proyectile.GetComponent<Rigidbody>().velocity = Vector3.zero;
        proyectile.transform.position = owner.transform.position;
        pool.Enqueue(proyectile);
    }

}
