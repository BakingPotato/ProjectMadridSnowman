using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeEnemy : EnemyManager
{
    [SerializeField] ShootingProjectiles shootingProjectiles;
    [SerializeField] ShootingProjectiles shootingProjectiles2;
    [SerializeField] bool isTurret;

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
}