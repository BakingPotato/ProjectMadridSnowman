using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossController : EnemyManager
{

    public enum AttackType
    {
        DT, DP, DL
    }

    [IEnnumerable]
    public struct Attack
    {
        public AttackType type;
        public int repetitions;
        public float duration;
        public float cooldown;
        public GameObject Shooter;
    }

    public List<Attack> phase1;

    public override void Start()
    {
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }


    // Update is called once per frame
    public override void Update()
    {
        distanceToTarget = Vector3.Distance(target.position, transform.position);

        //Si es provocado o el jugador entra en su rango, persigue al jugador
        EngageTarget();
    }

    public IEnumerator InvokeShooter(GameObject shooter, Vector3 position, int repetitions, float cooldown)
    {
        for(int i = 0; i < repetitions; i++)
        {
            Instantiate(shooter, position, Quaternion.identity);
            yield return new WaitForSeconds(cooldown);
        }
        //Instantiate(shooter, transform.position + new Vector3(0, 14, 0), Quaternion.identity);
    }

    public void startAttack(Attack attack)
    {
        switch (attack.type)
        {
            case AttackType.DT:
                break;
            case AttackType.DL:
                break;
            case AttackType.DP:
                break;
        }
    }
}
