using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeEnemy : EnemyManager
{
    [SerializeField] ShootingProjectiles shootingProjectiles;
    [SerializeField] ShootingProjectiles shootingProjectiles2;
    [SerializeField] bool isTurret;
    [SerializeField] Animator RangeEnemyAnimator;

	public override void AttackTarget()
    {
        shootingProjectiles.Shoot(transform.forward, enemyDamage);
        if (shootingProjectiles2)
            shootingProjectiles2.Shoot(transform.forward, enemyDamage);
    }

	public override void FaceTarget()
	{
        if(!isTurret)
		    base.FaceTarget();
	}

    protected override void EngageTarget()
    {
        if (!isTurret)
        {
            FaceTarget();
            if (distanceToTarget >= navMeshAgent.stoppingDistance)
            {
                ChaseTarget();
                if(RangeEnemyAnimator) RangeEnemyAnimator.SetTrigger("Move");
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
        else {

            FaceTarget();

            if (type != enemyClass.Boss && distanceToTarget <= chaseRange)
            {
                AttackTarget();
            }
            else
            {
                //Cancelar animaci�n de ataque
            }
        }

    }
}