using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] enemyClass type = enemyClass.Melee;

    [Header("Movimiento")]
    [SerializeField] float chaseRange = 15f;
    [SerializeField] float turnSpeed = 5f;

    [Header("Da�o")]
    [SerializeField] protected int enemyDamage = 2;

    //Si es provocado persiguira al jugador
    bool isProvoked = false;

    [Header("Distancia al jugador")]
    float distanceToTarget = Mathf.Infinity;
    Transform target;

    HealthManager health;
    protected NavMeshAgent navMeshAgent;

    public enemyClass getEnemyClass()
    {
        return type;
    }

    // Start is called before the first frame update
    public virtual void Start()
    {
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        health = GetComponent<HealthManager>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if (target)
        {
            distanceToTarget = Vector3.Distance(target.position, transform.position);

            //Si es provocado o el jugador entra en su rango, persigue al jugador
            if (isProvoked)
            {
                EngageTarget();
            }
            else if (distanceToTarget <= chaseRange)
            {
                isProvoked = true;
            }
        }
    }

    public void OnDamageTaken()
    {
        isProvoked = true;
    }

    private void EngageTarget()
    {
        FaceTarget();
        if (distanceToTarget >= navMeshAgent.stoppingDistance)
        {
            ChaseTarget();
        }

        if (distanceToTarget <= navMeshAgent.stoppingDistance)
        {
            AttackTarget();
        }
        else
        {
            //Cancelar animaci�n de ataque
        }

    }

    private void ChaseTarget()
    {
        if (navMeshAgent.isActiveAndEnabled)
        {
            navMeshAgent.SetDestination(target.position);
        }

    }

    public virtual void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
    }

    public virtual void AttackTarget() {}

    protected virtual void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            HealthManager playerHealth = collision.gameObject.GetComponent<HealthManager>();
            playerHealth.takeDamage(enemyDamage);
        }
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            HealthManager playerHealth = collision.gameObject.GetComponent<HealthManager>();
            playerHealth.takeDamage(enemyDamage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }

    public void healthDepleted()
    {
        enabled = false;
        navMeshAgent.enabled = false;
        Destroy(gameObject);
    }
}
