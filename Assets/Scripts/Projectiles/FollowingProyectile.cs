using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingProyectile : MonoBehaviour
{

    Transform target;

    [Header("Variables de movimiento")]
    public Rigidbody rb;
    float speed;
    float rotateSpeed;


    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player)
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
        else
            target = null;

        speed = Random.Range(4, 6);
        rotateSpeed = Random.Range(2, 6);

        StartCoroutine(pursuePlayer());

    }

    public IEnumerator pursuePlayer()
    {
        yield return new WaitForSeconds(1f);
        while (true)
        {
            yield return StartCoroutine(pursuePlayerFunc());
        }
    }

    private IEnumerator pursuePlayerFunc()
    {
        //La distancia del objeto al objetivo
        Vector3 direction = target.position - rb.position;

        //Normaliza para que la direcci�n este en 1
        direction.Normalize();

        //Multiplicamos ambos vectores para saber cuanto girar para mirar al objetivo
        Vector3 rotateAmount = Vector3.Cross(direction, transform.up);

        //El - es para que el misil mire para el jugador
        rb.angularVelocity = -rotateAmount * rotateSpeed;

        rb.velocity = transform.up * speed;

        yield return new WaitForEndOfFrame();
    }

}