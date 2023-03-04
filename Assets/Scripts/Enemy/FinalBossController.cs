﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossController : EnemyManager
{

    public enum AttackType
    {
        DT, DF, DP, DL, SD, SD2, SD3, WD, DG, DASH, JUMP, KICK, JAIL
    }

    [System.Serializable]
    public struct Attack
    {
        public AttackType type;
        public int repetitions;
        public float duration;
        public float cooldown;
        public float timeBeforeNextAttack;
    }

    [System.Serializable]
    public struct Phase
    {
        public Attack[] attacks;
        public int HPLimit;
        public bool random;
        public bool transition;
        public bool walking;
    }

    [Header("Fases")]
    [SerializeField] Phase[] Phases;
    [SerializeField] bool walk = false;
    [SerializeField] float walk_speed = 1.5f;
    int attack_numbers = 0;
    Coroutine actual_attack;
    bool actual_Phase_Walk;
    bool goingUp = false;
    bool goingDown = false;

    [Header("Contenedores")]
    public GameObject DL_object;
    public GameObject DP_object;
    public GameObject SD_object;
    public GameObject SD2_object;
    public GameObject DT_object;
    public GameObject WD_object;
    public GameObject DG_object;
    public GameObject DF_object;
    public GameObject JAIL_object;
    public GameObject DJump_object;
    public GameObject DKIck_object;
    public GameObject Godness_object;
    Rigidbody rb;

    public override void Start()
    {
        health = GetComponent<HealthManager>();
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody>();

        StartCoroutine(MainControl());

        actual_attack = null;
    }


    // Update is called once per frame
    public override void Update()
    {
        distanceToTarget = Vector3.Distance(target.position, transform.position);
        //Si es provocado o el jugador entra en su rango, persigue al jugador
        EngageTarget();

        if (goingUp)
        {
            transform.Translate(Vector3.up * Time.deltaTime * 20, Space.World);
        }

        if (goingDown)
        {
            transform.Translate(Vector3.down * Time.deltaTime * 25, Space.World);
        }
    }

    protected virtual void EngageTarget()
    {
        FaceTarget();

        if (distanceToTarget >= navMeshAgent.stoppingDistance)
        {
            ChaseTarget();
            if (anime)
                anime.SetTrigger("Move");
        }

        if (type != enemyClass.Boss && distanceToTarget <= navMeshAgent.stoppingDistance)
        {
            AttackTarget();
        }
        else
        {
            //Cancelar animaci�n de ataque
        }

    }

    protected void ChaseTarget()
    {
        if (navMeshAgent)
        {
            if (walk & navMeshAgent.isActiveAndEnabled)
            {
                navMeshAgent.SetDestination(target.position);
            }
            else if (!walk & navMeshAgent.isActiveAndEnabled)
            {
                navMeshAgent.ResetPath();
            }
        }

    }


    IEnumerator MainControl()
    {
        Coroutine actualPhase = null;
        for(int i = 0; i < Phases.Length; i++)
        {
            Debug.Log("actual phase is " + i );
            if (Phases[i].transition)
            {
                yield return StartCoroutine(ProccessPhase(Phases[i]));
                Debug.Log("transition end");
            }
            else
            {
                actualPhase = StartCoroutine(ProccessPhase(Phases[i]));

                while (health.getThisHealth() > Phases[i].HPLimit)
                {
                    yield return new WaitForEndOfFrame();
                }
                Debug.Log(health.getThisHealth() + " > " + Phases[i].HPLimit);


                if (actualPhase != null)
                {
                    StopCoroutine(actualPhase);
                    actualPhase = null;
                }

                Debug.Log("attack_numbers - comprobacion");

                while (attack_numbers > 0)
                {
                    yield return new WaitForEndOfFrame();
                }
                Debug.Log("attack_numbers superado");
            }
        }
        Debug.Log("battle end");
        yield return new WaitForEndOfFrame();
    }

    IEnumerator ProccessPhase(Phase phase)
    {
        int j = 0;
        actual_Phase_Walk = phase.walking;
        if (!phase.random)
        {
            while (true)
            {
                actual_attack = startAttack(phase.attacks[j]);

                walk = false;
                //Si es un tipo de ataque que se puede solapar con otros, esperamos el tiempo que diga el jugador, en caso contrario, esperamos a que el ataque acabe
                if(phase.attacks[j].type == AttackType.DP || phase.attacks[j].type == AttackType.DL || phase.attacks[j].type == AttackType.JAIL)
                {
                    yield return new WaitForSeconds(phase.attacks[j].timeBeforeNextAttack);
                }
                else
                    yield return actual_attack = startAttack(phase.attacks[j]);
                j++;

                //Si estamos en una transición pasamos a la siguiente fase en cuanto acaben los ataques
                if (phase.transition && j == phase.attacks.Length) break;
                //En caso contrario, repetimos hasta que se cumpla el limite de vida
                else if (j == phase.attacks.Length) j = 0;
            }
        }
    }

    public Coroutine startAttack(Attack attack)
    {
        Coroutine c = null;
        switch (attack.type)
        {
            case AttackType.DT:
                c = StartCoroutine(ActivateShooter(DT_object, attack.duration, attack.cooldown, attack.timeBeforeNextAttack));
                break;
            case AttackType.DF:
                c = StartCoroutine(ActivateShooter(DF_object, attack.duration, attack.timeBeforeNextAttack));
                break;
            case AttackType.DL:
                c = StartCoroutine(InvokeSnowRain(DL_object, attack.repetitions, attack.cooldown, attack.timeBeforeNextAttack));
                break;
            case AttackType.DP:
                c = StartCoroutine(ActivateShooter_DP(DP_object, attack.cooldown, attack.timeBeforeNextAttack));
                break;
            case AttackType.SD:
                c = StartCoroutine(ActivateShooter(SD_object, attack.repetitions, attack.duration, attack.cooldown, attack.timeBeforeNextAttack));
                break;
            case AttackType.SD2:
                c = StartCoroutine(ActivateShooter(SD2_object, attack.repetitions, attack.duration, attack.cooldown, attack.timeBeforeNextAttack));
                break;
            case AttackType.SD3:
                c = StartCoroutine(ActivateShooter_3DS(SD_object, SD2_object, attack.repetitions, attack.duration, attack.cooldown, attack.timeBeforeNextAttack));
                break;
            case AttackType.WD:
                c = StartCoroutine(ActivateShooter(WD_object, attack.duration, attack.timeBeforeNextAttack));
                break;
            case AttackType.JUMP:
                c = StartCoroutine(JumpAttack(DJump_object, attack.repetitions, attack.duration, attack.cooldown, attack.timeBeforeNextAttack));
                break;
            case AttackType.KICK:
                c = StartCoroutine(KickAttack(DKIck_object, attack.repetitions, attack.cooldown, attack.timeBeforeNextAttack));
                break;
            case AttackType.DG:
                c = StartCoroutine(DGAttack(DG_object, attack.duration, attack.cooldown, attack.timeBeforeNextAttack));
                break;
            case AttackType.JAIL:
                c = StartCoroutine(InstantiateJail(JAIL_object, attack.timeBeforeNextAttack));
                break;
        }
        return c;
    }

    public IEnumerator ActivateShooter(GameObject shooter, float duration, float cooldown, float waitTime)
    {
        attack_numbers++;
        shooter.SetActive(true);
        ShootingProjectiles sp = shooter.GetComponent<ShootingProjectiles>();

        sp.ShootCooldown = cooldown;
        yield return new WaitForSeconds(duration);

        shooter.SetActive(false);

        walk = actual_Phase_Walk;
        yield return new WaitForSeconds(waitTime);
        attack_numbers--;
    }

    public IEnumerator ActivateShooter(GameObject shooter, float duration, float waitTime)
    {
        attack_numbers++;
        shooter.SetActive(true);

        yield return new WaitForSeconds(duration);

        shooter.SetActive(false);

        walk = actual_Phase_Walk;
        yield return new WaitForSeconds(waitTime);
        attack_numbers--;
    }


    public IEnumerator ActivateShooter(GameObject shooter, int repetitions, float duration, float cooldown, float waitTime)
    {
        attack_numbers++;
        for(int j = 0; j < repetitions; j++)
        {
            shooter.SetActive(true);
            yield return new WaitForSeconds(duration);
            shooter.SetActive(false);
            yield return new WaitForSeconds(cooldown);
        }
        walk = actual_Phase_Walk;
        yield return new WaitForSeconds(waitTime);
        attack_numbers--;
    }

    public IEnumerator ActivateShooter_DP(GameObject shooter, float cooldown, float waitTime)
    {
        attack_numbers++;
        shooter.SetActive(true);
        attack_numbers--;

        foreach (ShootingProjectiles sp in shooter.GetComponentsInChildren<ShootingProjectiles>())
        {
            sp.ShootCooldown = cooldown;
        }

        yield return new WaitForSeconds(cooldown);

        foreach (ShootingProjectiles sp in shooter.GetComponentsInChildren<ShootingProjectiles>())
        {
            sp.ShootCooldown = 99;
        }

        shooter.SetActive(false);
        walk = actual_Phase_Walk;
        yield return new WaitForSeconds(waitTime);
    }

    public IEnumerator ActivateShooter_3DS(GameObject shooter, GameObject shooter2, int repetitions, float duration, float cooldown, float waitTime)
    {
        attack_numbers++;
        for (int j = 0; j < repetitions; j++)
        {
            shooter.SetActive(true);
            yield return new WaitForSeconds(duration);
            shooter.SetActive(false);

            shooter2.SetActive(true);
            yield return new WaitForSeconds(duration);
            shooter2.SetActive(false);

            yield return new WaitForSeconds(cooldown);
        }
        walk = actual_Phase_Walk;
        yield return new WaitForSeconds(waitTime);
        attack_numbers--;
    }


    public IEnumerator InvokeShooter(GameObject shooter, Vector3 position, int repetitions, float cooldown, float waitTime)
    {
        attack_numbers++;
        for(int i = 0; i < repetitions; i++)
        {
            Instantiate(shooter, position, Quaternion.identity);
            yield return new WaitForSeconds(cooldown);
        }
        yield return new WaitForSeconds(waitTime);
        attack_numbers--;
    }

    public IEnumerator InvokeSnowRain(GameObject shooter, int repetitions, float cooldown, float waitTime)
    { 
        attack_numbers++;
        yield return new WaitForSeconds(1);
        attack_numbers--;

        walk = actual_Phase_Walk;

        for (int i = 0; i < repetitions; i++)
        {
            int limit = Random.Range(5, 8);
            for (int j = 0; j < limit; j++)
            {
                Instantiate(shooter, getRandomPosAbovePlayer(), Quaternion.Euler(new Vector3(90, 0, 0)));
                yield return new WaitForSeconds(0.8f);
            }
            yield return new WaitForSeconds(cooldown);
        }
        yield return new WaitForSeconds(waitTime);
    }

    public IEnumerator JumpAttack(GameObject shooter, int repetitions, float duration, float cooldown, float waitTime)
    {
        navMeshAgent.enabled = false;

        for(int i = 0; i < repetitions; i++)
        {
            goingUp = true;
            while (transform.position.y < 25)
            {
                yield return new WaitForEndOfFrame();
            }
            goingUp = false;

            Godness_object.GetComponent<MeshRenderer>().enabled = false;

            yield return new WaitForSeconds(duration);

            Vector3 targetFall = target.position;

            Godness_object.GetComponent<MeshRenderer>().enabled = true;

            transform.position = new Vector3(targetFall.x, 30.0f, targetFall.z);

            goingDown = true;
            while (transform.position.y > 1)
            {
                yield return new WaitForEndOfFrame();
            }
            goingDown = false;

            Instantiate(shooter, new Vector3(transform.position.x, 1, transform.position.z), Quaternion.identity);

            yield return new WaitForSeconds(cooldown);
        }
        //Animación
        attack_numbers--;
        navMeshAgent.enabled = true;

        walk = actual_Phase_Walk;

        yield return new WaitForSeconds(waitTime);
    }

    public IEnumerator DGAttack(GameObject shooter, float duration, float cooldown, float waitTime)
    {
        navMeshAgent.enabled = false;

        goingUp = true;
        while (transform.position.y < 25)
        {
            yield return new WaitForEndOfFrame();
        }
        goingUp = false;

        Godness_object.GetComponent<MeshRenderer>().enabled = false;

        yield return new WaitForSeconds(0.5f);

        Godness_object.GetComponent<MeshRenderer>().enabled = true;

        transform.position = new Vector3(-1.77f, 30.0f, -19.66f);

        goingDown = true;
        while (transform.position.y > 1)
        {
            yield return new WaitForEndOfFrame();
        }
        goingDown = false;

        shooter.SetActive(true);

        yield return new WaitForSeconds(duration);

        Instantiate(JAIL_object, new Vector3(target.position.x, 1, target.position.z - 8), Quaternion.identity);

        foreach (ShootingProjectiles sp in shooter.GetComponentsInChildren<ShootingProjectiles>())
        {
            sp.ShootCooldown = 0.4F;
        }

        yield return new WaitForSeconds(cooldown);

        shooter.SetActive(false);

        foreach (ShootingProjectiles sp in shooter.GetComponentsInChildren<ShootingProjectiles>())
        {
            sp.ShootCooldown = 0.7F;
        }

        //Animación
        attack_numbers--;
        navMeshAgent.enabled = true;

        walk = actual_Phase_Walk;

        yield return new WaitForSeconds(waitTime);
    }

    public IEnumerator KickAttack(GameObject shooter, int repetitions, float cooldown, float waitTime)
    {
        attack_numbers++;

        for (int j = 0; j < repetitions; j++)
        {
            yield return new WaitForSeconds(1);
            shooter.SetActive(true);
            yield return new WaitForSeconds(0.7f);
            shooter.SetActive(false);
            yield return new WaitForSeconds(cooldown);
        }
        walk = actual_Phase_Walk;
        yield return new WaitForSeconds(waitTime);
        attack_numbers--;
    }

    public IEnumerator InstantiateJail(GameObject shooter, float waitTime)
    {
        attack_numbers++;
        //Animación
        yield return new WaitForSeconds(1);
        attack_numbers--;

        walk = actual_Phase_Walk;

        Instantiate(shooter, new Vector3(target.position.x, 1, target.position.z - 8), Quaternion.identity);
       yield return new WaitForSeconds(waitTime);
    }

    public Vector3 getRandomPosAbovePlayer()
    {
        return new Vector3(Random.Range(-3.0f, 3.0f), 15, Random.Range(-3.0f, 3.0f)) + target.transform.position;
    }

}
