using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HamController : UmbrellaController
{
    public int damage = 1;

    void FixedUpdate()
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
                //else
                //{
                //    Debug.Log("enemigo es null pero colisiona :)");
                //}

                break;

            default:
                break;
        }
    }
  
}
