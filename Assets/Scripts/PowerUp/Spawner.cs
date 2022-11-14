using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject powerUp;
    [SerializeField] float waitTime;
    [SerializeField] GameObject _actualPowerUp;

    public bool isSpawning { get { return spawning != null; } }
    Coroutine spawning = null;


    // Start is called before the first frame update
    void Start()
    {
        //_actualPowerUp = Instantiate(powerUp, transform.position, transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        if(_actualPowerUp == null && !isSpawning)
        {
            //Paramos la corrutina por si entro uno antes y empezamos
            spawning = StartCoroutine(spawnPowerUp());
        }
    }

    IEnumerator spawnPowerUp()
    {
        yield return new WaitForSeconds(waitTime);
        _actualPowerUp = Instantiate(powerUp, transform.position, transform.rotation);
        spawning = null;
    }
}
