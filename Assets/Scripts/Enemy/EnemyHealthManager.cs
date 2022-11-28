using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : HealthManager
{
	[SerializeField] EnemyHealthBar healthBar;
	// Start is called before the first frame update
	void Start()
    {
        health = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        GM = GameManager.Instance;
    }

    public override void takeDamage(int damage)
    {
        health -= damage;
        healthBar.SetHealth(health);
		//AudioManager.Instance.PlaySFX3DRandomPitch("EnemyHurt", transform.position);
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/3D/Enemies/enemy_is_hurt");
        StartBlinking();
		if (gameObject.GetComponent<EnemyManager>().getEnemyClass() == enemyClass.Melee)
        {
            //Es provocado al ser da�ado
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
            //AudioManager.Instance.PlaySFX3DRandomPitch("EnemyDeath", transform.position);
            FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/3D/Enemies/enemy_is_dead");
            GM.CurrentLevelManager.KillCount++;
            this.GetComponent<EnemiesLootManager>().InstanceRandomLoot();
            Destroy(gameObject);
        }
    }

}
