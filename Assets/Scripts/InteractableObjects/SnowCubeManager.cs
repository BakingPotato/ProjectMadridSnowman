using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowCubeManager : MonoBehaviour
{

    public int health = 2;

    public List<GameObject> spawnablePickUps;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {
            Destroy(gameObject);
        }

        //if (Input.GetKeyDown(KeyCode.Mouse0))
        //{
        //    takeDamage(1);
        //}
        
    }

    public void takeDamage(int amount)
    {
        health -= amount;
    }

    private void OnDestroy()
    {
        int i = Random.Range(0, spawnablePickUps.Count);
        Instantiate(spawnablePickUps[i], transform.position, transform.rotation);
    }
}
