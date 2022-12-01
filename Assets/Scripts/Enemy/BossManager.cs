using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class BossManager : EnemyManager
{
    [Header("Variables de Vida")]
    [SerializeField] BossHealthManager BHM;
    [SerializeField] NavMeshAgent movement;

    [Header("Prefabs de PowerUps")]
    [SerializeField] GameObject prefab_Umbrella;
    [SerializeField] GameObject prefab_Ham;

    [SerializeField] ShootingProjectiles shootingProjectiles;
    [SerializeField] Image powerUpIMG;

    [Header("Variables de fases")]
    [SerializeField] List<Phase> phases;
    public int phase_id = -1;

    [Serializable]
    public class Phase
    {
        public List<string> powerUps;
        public float spawningTime;
        public float shootingSpeedBuff;

        public int health;

        public float speed;
        public float acceleration;
        public float stoppingDistance;

        public bool autoBraking;
    }

    private void LateUpdate()
    {
        AttackTarget();
    }

    public override void AttackTarget()
    {
        shootingProjectiles.Shoot(transform.forward, enemyDamage);
    }

    public void setBossHP()
    {
        BHM.Boss_HP = phases.Count - 1;
    }



    public void ChangePhase()
    {
        //Avanzamos el número de fase y la obtenermos de la lista, debe haber el mismo número de boss_hp que de fases
        phase_id++;
        Phase phase = phases[phase_id];

        //Definimos la vida máxima de la fase actual
        BHM.setHealth(phase.health);

        //Procesamos las variables de movimiento, si no estan definidas en la fase se dejan las viejas
        movement.speed = phase.speed == 0 ? movement.speed : phase.speed;
        movement.acceleration = phase.acceleration == 0 ? movement.acceleration : phase.acceleration;
        movement.stoppingDistance = phase.stoppingDistance == -1 ? movement.stoppingDistance : phase.stoppingDistance;
        movement.autoBraking = phase.autoBraking;

        StartCoroutine(powerUpBoss(phase));

    }

    public void ProcessDeath()
    {
        //Avanzamos el número de fase para eliminar todos los powerUps
        phase_id++;

        //Animación de morir

        //Destruir
        Destroy(gameObject);
    }



    IEnumerator powerUpBoss(Phase phase)
    {
        //recorremos la lista de powerUps y los aplicamos
        foreach (string pu in phase.powerUps)
        {
            processPowerUp(pu.ToLower(), phase);
            yield return new WaitForSeconds(phase.spawningTime);
        }
    }

    void processPowerUp(string powerUp_name, Phase phase)
    {
        switch (powerUp_name)
        {
            case "ham":
                instantiateHam(phase_id);
                break;
            case "umbrella":
                instantiateUmbrella(phase_id);
                break;
            case "shottingspeed":
                IncreaseShootingSpeed(phase_id, phase.shootingSpeedBuff);
                break;
            case "tripleshoot":
                SetTripleShot(phase_id);
                break;
        }
    }

    //PowerUps
    public void instantiateUmbrella(int actual_phase)
    {
        StartCoroutine(powerUpUmbrella(actual_phase));
    }
    public void instantiateHam(int actual_phase)
    {
        StartCoroutine(powerUpHam(actual_phase));
    }

    public void IncreaseShootingSpeed(int actual_phase, float multiplier)
    {
        StartCoroutine(powerUpShootingSpeed(actual_phase, multiplier));
    }

    public void SetTripleShot(int actual_phase)
    {
        StartCoroutine(powerUpTripleShoot(actual_phase));
    }

    IEnumerator powerUpUmbrella(int actual_phase)
    {
        //Lo instanciamos en el padre de 
        prefab_Umbrella.GetComponent<UmbrellaController>().player = this.gameObject;
        GameObject actualUmbrella = Instantiate(prefab_Umbrella, transform.parent);

        while(phase_id == actual_phase)
        {
            yield return new WaitForEndOfFrame();
        }

        //Destruimos el objeto
        Destroy(actualUmbrella);
    }
    IEnumerator powerUpHam(int actual_phase)
    {
        //Lo instanciamos en el padre de 
        prefab_Ham.GetComponent<HamController>().player = this.gameObject;
        GameObject actualHam = Instantiate(prefab_Ham, transform.parent);

        while (phase_id == actual_phase)
        {
            yield return new WaitForEndOfFrame();
        }

        //Destruimos el objeto
        Destroy(actualHam);
    }

    IEnumerator powerUpShootingSpeed(int actual_phase, float multiplier)
    {
        //Lo activamos
        shootingProjectiles.IncreaseShootingSpeed(multiplier);


        while (phase_id == actual_phase)
        {
            yield return new WaitForEndOfFrame();
        }

        shootingProjectiles.DecreaseShootingSpeed(multiplier);
    }

    IEnumerator powerUpTripleShoot(int actual_phase)
    {
        //Lo instanciamos en el padre de 
        shootingProjectiles.Activate_TripleShot();


        while (phase_id == actual_phase)
        {
            yield return new WaitForEndOfFrame();
        }

        //Destruimos el objeto
        shootingProjectiles.Deactivate_TripleShot();
    }

    IEnumerator powerUpShotShize(int actual_phase)
    {
        //Lo instanciamos en el padre de 
        prefab_Ham.GetComponent<HamController>().player = this.gameObject;
        GameObject actualHam = Instantiate(prefab_Ham, transform.parent);

        while (phase_id == actual_phase)
        {
            yield return new WaitForEndOfFrame();
        }

        //Destruimos el objeto
        Destroy(actualHam);
    }
}
