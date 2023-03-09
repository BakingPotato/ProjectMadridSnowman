using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HamController : UmbrellaController
{
    public int damage = 1;

    public void FixedUpdate()
    {
        transform.position = player.transform.position;
        transform.Rotate(Vector3.down * speed);
    }

    private void OnTriggerEnter(Collider collider)
    {
       switch (LayerMask.LayerToName(collider.gameObject.layer))
        {
            case "Enemy":
                EnemyHealthManager ehm = collider.transform.GetComponent<EnemyHealthManager>();
                if (ehm != null)
                {
                    ehm.takeDamage(damage); 
                }
                else
                {
                    SpecialEnemyHealthManager sehm = collider.transform.GetComponent<SpecialEnemyHealthManager>();
                    if (sehm != null)
                    {
                        sehm.takeDamage(damage);
                    }
                }
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
