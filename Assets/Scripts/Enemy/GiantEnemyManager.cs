using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GiantEnemyManager : EnemyManager
{
    Rigidbody rb;
    public float speed;

    // Start is called before the first frame update
    public new void Start()
    {
        health = GetComponent<HealthManager>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    public new void Update()
    {
        rb.MovePosition(transform.position + new Vector3(0, 0, -1) * Time.deltaTime * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            HealthManager playerHealth = other.gameObject.GetComponent<HealthManager>();
            playerHealth.takeDamage(enemyDamage);
        }
    }
}

