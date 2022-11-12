using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : HealthManager
{
    public override void takeDamage(int damage)
    {
        health -= damage;
        if(gameObject.GetComponent<EnemyManager>().getEnemyClass() == enemyClass.Melee)
        {
            //Es provocado al ser da�ado
            gameObject.GetComponent<MeleeEnemyManager>().OnDamageTaken();
        }
        checkDeath();
    }
}
