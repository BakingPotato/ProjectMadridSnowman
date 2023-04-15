using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : HealthManager
{
	[SerializeField] EnemyHealthBar healthBar;

    public bool invincible = false;
    public bool finalBoss = false;

    public ParticleSystem snowExplosion;
	// Start is called before the first frame update
	void Start()
    {
        if (!finalBoss)
        {
            health = maxHealth;
            healthBar.SetMaxHealth(maxHealth);
        }
        else
        {
            healthBar.SetMaxHealth(maxHealth);
            healthBar.SetHealth(health);
        }

        GM = GameManager.Instance;
    }

    public override void takeDamage(int damage)
    {
        if (!invincible) {
            health -= damage;
            healthBar.SetHealth(health);
            //AudioManager.Instance.PlaySFX3DRandomPitch("EnemyHurt", transform.position);
            //if (snowExplosion)
            //{
            //    ParticleSystem SnowExplode = Instantiate(snowExplosion);
            //    SnowExplode.transform.position = transform.position;
            //    SnowExplode.Play();
            //}
            FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/3D/Enemies/enemy_is_hurt", transform.position);
            StartBlinking();
            if (gameObject.GetComponent<EnemyManager>().getEnemyClass() == enemyClass.Melee)
            {
                //Es provocado al ser da�ado
                gameObject.GetComponent<EnemyManager>().OnDamageTaken();
            }
            checkDeath();
        }
    }

    protected override void checkDeath()
    {
        if (health <= 0)
        {
            //Parar las corrutinas
            //Contamos la muerte
            //AudioManager.Instance.PlaySFX3DRandomPitch("EnemyDeath", transform.position);
            FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/3D/Enemies/enemy_is_dead", transform.position);
            GM.CurrentLevelManager.KillCount++;
            if (!finalBoss)
            {
                this.GetComponent<EnemiesLootManager>().InstanceRandomLoot();
            }
            else
            {
                //Animación muerte
                GM.ShowResults("Creditos");
            }
            Destroy(gameObject);
        }
    }

}
