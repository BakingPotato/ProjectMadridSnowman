using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossController : EnemyManager
{

    public enum AttackType
    {
        DT, DP, DL, SD
    }

    [System.Serializable]
    public struct Attack
    {
        public AttackType type;
        public int repetitions;
        public float duration;
        public float cooldown;
    }

    [System.Serializable]
    public struct Phase
    {
        public Attack[] attacks;
        public float cooldown;
        public int HPLimit;
        public bool random;
    }

    [Header("Fases")]
    [SerializeField] Phase[] Phases;
    int attack_numbers = 0;

    [Header("Contenedores")]
    public GameObject DL_object;
    public GameObject SD_object;
    public GameObject DT_object;




    public override void Start()
    {
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(MainControl());
    }


    // Update is called once per frame
    public override void Update()
    {
        distanceToTarget = Vector3.Distance(target.position, transform.position);

        //Si es provocado o el jugador entra en su rango, persigue al jugador
        EngageTarget();
    }

    IEnumerator MainControl()
    {
        Coroutine actualPhase = null;
        for(int i = 0; i < Phases.Length; i++)
        {
            actualPhase = StartCoroutine(ProccessPhase(Phases[0]));
            //while (HP > Phases[i].HPlimit)
            //{
            //    yield return new WaitForSeconds(1);
            //}
            //StopCoroutine(actualPhase);
            //actualPhase = null;

            ////Esperamos a que no haya más ataques
            //while(attack_numbers > 0)
            //{
            //    yield return new WaitForEndOfFrame();
            //}

        }
        yield return new WaitForEndOfFrame();
    }

    IEnumerator ProccessPhase(Phase phase)
    {
        int j = 0;
        if (!phase.random)
        {
            while (true)
            {
                if (j == phase.attacks.Length) j = 0;
                startAttack(phase.attacks[j]);
                j++;
                yield return new WaitForSeconds(phase.cooldown);
            }
        }
    }

    public void startAttack(Attack attack)
    {
        switch (attack.type)
        {
            case AttackType.DT:
                StartCoroutine(ActivateShooter(DT_object, attack.duration, attack.cooldown));
                break;
            case AttackType.DL:
                StartCoroutine(InvokeShooters(DL_object, attack.repetitions, attack.cooldown));
                break;
            case AttackType.DP:
                break;
            case AttackType.SD:
                StartCoroutine(ActivateShooter(SD_object, attack.repetitions, attack.duration, attack.cooldown));
                break;
        }
    }

    public IEnumerator ActivateShooter(GameObject shooter, float duration, float cooldown)
    {
        attack_numbers++;
        shooter.SetActive(true);
        ShootingProjectiles sp = shooter.GetComponent<ShootingProjectiles>();

        sp.ShootCooldown = cooldown;
        yield return new WaitForSeconds(duration);

        shooter.SetActive(false);
        attack_numbers--;
    }

    public IEnumerator ActivateShooter(GameObject shooter, int repetitions, float duration, float cooldown)
    {
        attack_numbers++;
        for(int j = 0; j < repetitions; j++)
        {
            shooter.SetActive(true);
            yield return new WaitForSeconds(duration);
            shooter.SetActive(false);
            yield return new WaitForSeconds(cooldown);
        }

        attack_numbers--;
    }

    public IEnumerator InvokeShooter(GameObject shooter, Vector3 position, int repetitions, float cooldown)
    {
        attack_numbers++;
        for(int i = 0; i < repetitions; i++)
        {
            Instantiate(shooter, position, Quaternion.identity);
            yield return new WaitForSeconds(cooldown);
        }
        attack_numbers--;
    }

    public IEnumerator InvokeShooters(GameObject shooter, int repetitions, float cooldown)
    {
        attack_numbers++;
        for (int i = 0; i < repetitions; i++)
        {
            for(int j = 0; j < Random.Range(5, 8); j++)
            {
                Instantiate(shooter, getRandomPosAbovePlayer(), Quaternion.Euler(new Vector3(90, 0, 0)));
                yield return new WaitForSeconds(1f);
            }
            yield return new WaitForSeconds(cooldown);
        }
        attack_numbers--;
    }

    public Vector3 getRandomPosAbovePlayer()
    {
        return new Vector3(Random.Range(-3.0f, 3.0f), 15, Random.Range(-3.0f, 3.0f)) + target.transform.position;
    }

}
