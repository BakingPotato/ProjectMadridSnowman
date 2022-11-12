using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeEnemy : EnemyManager
{
    [SerializeField] ShootingProjectiles shootingProjectiles;
    [SerializeField] bool isTurret;

	public override void AttackTarget()
    {
        shootingProjectiles.Shoot(transform.forward, enemyDamage);
    }

	public override void FaceTarget()
	{
        if(!isTurret)
		    base.FaceTarget();
	}
}