using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossManager : EnemyManager
{
    [SerializeField] ShootingProjectiles shootingProjectiles;
    [SerializeField] ShootingProjectiles shootingProjectiles2;

    [Serializable]
    public class Phase
    {
        public int phase_id;
        public List<PickUp> phasePower;
        public int phase_Health;

        public int phase_number;
        public int phase_speed;
        public int phase_acceleration;
        public int phase_stoppingDistance;

    }



    public override void AttackTarget()
    {
        shootingProjectiles.Shoot(transform.forward, enemyDamage);
        if (shootingProjectiles2)
            shootingProjectiles2.Shoot(transform.forward, enemyDamage);
    }

    private void LateUpdate()
    {
        AttackTarget();
    }
}
