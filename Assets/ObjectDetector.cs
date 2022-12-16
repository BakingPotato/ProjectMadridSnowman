using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDetector : MonoBehaviour
{
    [SerializeField] EnemyManager enemyManager;
    // Start is called before the first frame update
    void Start()
    {
        enemyManager.changeTarget(null);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "gift")
        {
            enemyManager.changeTarget(other.gameObject);
        }
    }
}
