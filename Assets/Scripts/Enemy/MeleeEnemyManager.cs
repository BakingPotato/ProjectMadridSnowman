using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleeEnemyManager : EnemyManager
{
    public Animator anim;
    public override void AttackTarget()
    {
        anim.SetTrigger("Attack");


        //if (target)
        //{
        //    distanceToTarget = Vector3.Distance(target.position, transform.position);

        //    if(distanceToTarget < attackRange)
        //    {

        //    }

        //}
    }
}