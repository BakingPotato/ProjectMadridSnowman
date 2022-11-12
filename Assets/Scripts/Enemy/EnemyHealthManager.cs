using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : HealthManager
{
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;

        GM = GameManager.Instance;
    }

    public override void takeDamage(int damage)
    {
        health -= damage;
        if(gameObject.GetComponent<EnemyManager>().getEnemyClass() == enemyClass.Melee)
        {
            //Es provocado al ser dañado
            gameObject.GetComponent<EnemyManager>().OnDamageTaken();
        }
        checkDeath();
    }

    protected override void checkDeath()
    {
        if (health <= 0)
        {
            //Parar las corrutinas
            //Contamos la muerte
            GM.CurrentLevelManager.KillCount++;
            Destroy(gameObject);
        }
    }

}
