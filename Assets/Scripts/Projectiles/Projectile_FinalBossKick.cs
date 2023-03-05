using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_FinalBossKick : MonoBehaviour
{
    [SerializeField] GameObject DKick_Holder;

    private void Start()
    {
        StartCoroutine(Boss_Kick_Projectile());
    }

    IEnumerator Boss_Kick_Projectile()
    {
        while (true)

        {
            yield return new WaitForSeconds(0.25f);
            Instantiate(DKick_Holder, new Vector3(transform.position.x, -3, transform.position.z), Quaternion.Euler(new Vector3(-90, 0, 0)));
            yield return new WaitForSeconds(0.65f);
        }
    }
}
