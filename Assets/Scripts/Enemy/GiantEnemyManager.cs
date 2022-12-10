using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantEnemyManager : EnemyManager
{

    Rigidbody rb;
    public float speed;
    int phase;

    SpecialEnemyHealthManager boss_health;

    [Header("Variables de cambio de fase")]
    [SerializeField] GameObject spawner1;
    [SerializeField] GameObject spawner2;
    [SerializeField] GameObject shooter;
    [SerializeField] GameObject health_UI;


    // Start is called before the first frame update
    public new void Start()
    {
        boss_health = GetComponent<SpecialEnemyHealthManager>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody>();
        phase = 1;
    }

    // Update is called once per frame
    public new void Update()
    {
        //Solo se mueve durante la fase 1
        if(phase == 1)
            rb.MovePosition(transform.position + new Vector3(0, 0, -1) * Time.deltaTime * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            HealthManager playerHealth = other.gameObject.GetComponent<HealthManager>();
            playerHealth.takeDamage(enemyDamage);
        }else if(other.gameObject.name == "Phase2_Trigger")
        {
            changePhase();
        }
    }

    public void changePhase()
    {
        phase++;
        boss_health.invincible = false;
        spawner1.SetActive(true);
        spawner2.SetActive(true);
        shooter.SetActive(true);

        //Preparamos y mostramos la barra de vida
        BossHealthBar BHB = health_UI.GetComponent<BossHealthBar>();
        BHB.SetMaxHealth(boss_health.getMaxHealth());
        BHB.SetHealth(boss_health.getThisHealth()); 
        health_UI.SetActive(true);
    }

}