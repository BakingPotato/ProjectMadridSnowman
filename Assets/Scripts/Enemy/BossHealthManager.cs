using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealthManager : HealthManager
{
	[SerializeField] BossHealthBar healthBar;

    [Header("Variables de jefe")]
    [SerializeField] BossManager BM;
    public int Boss_HP;

    // Start is called before the first frame update
    void Start()
    {
        BM = GetComponent<BossManager>();
        BM.setBossHP();
        BM.ChangePhase();

        GM = GameManager.Instance;
    }


    public void setMaxHealth(int new_health)
    {
        health = new_health;
        healthBar.SetMaxHealth(health);
    }

    public override void takeDamage(int damage)
    {
        if (!invencible)
        {
            health -= damage;
            healthBar.SetHealth(health);
            //AudioManager.Instance.PlaySFX3DRandomPitch("EnemyHurt", transform.position);
            FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/3D/Enemies/enemy_is_hurt", transform.position);
            StartBlinking();
            if (gameObject.GetComponent<EnemyManager>().getEnemyClass() == enemyClass.Boss)
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
            if(Boss_HP == 0)
            {
                //Parar las corrutinas
                //Contamos la muerte
                //AudioManager.Instance.PlaySFX3DRandomPitch("EnemyDeath", transform.position);
                FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/3D/Enemies/enemy_is_dead", transform.position);
                GM.CurrentLevelManager.KillCount++;
                BM.ProcessDeath();
            }
            else
            {
                Boss_HP--;
                BM.ChangePhase();
            }

        }
    }

}
