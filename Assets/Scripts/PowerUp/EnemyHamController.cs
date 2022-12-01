using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHamController : HamController
{

    private void OnTriggerEnter(Collider collider)
    {
       switch (LayerMask.LayerToName(collider.gameObject.layer))
        {
            case "Player":
                HealthManager phm = collider.transform.GetComponent<HealthManager>();
                if (phm != null)
                {
                    phm.takeDamage(damage); 
                }
                //else
                //{
                //    Debug.Log("enemigo es null pero colisiona :)");
                //}
                break;

            case "Box":
                SnowCubeManager scm = collider.transform.GetComponent<SnowCubeManager>();
                if (scm != null)
                {
                    scm.takeDamage(damage);
                }
                break;
            default:
                break;
        }
    }

}
