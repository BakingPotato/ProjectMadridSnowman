using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    Rigidbody rb;

    Vector3 lookPos;

    [Header("Variable de movimiento")]
    public float speed = 50;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        RotateCharacterToMouse();
    }

    private void FixedUpdate()
    {
        GetMovementInput();
    }

    private void RotateCharacterToMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100))
        {
            lookPos = hit.point;
        }

        Vector3 lookDir = lookPos - transform.position;
        lookDir.y = 0;

        transform.LookAt(transform.position + lookDir, Vector3.up);
    }

    private void GetMovementInput()
    {
        //Obtenemos los movimientos del jugador
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        //Aplicamos el normalized para que los diagonales no sean m�s r�pido
        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;

        //Si hay movimiento
        if (direction.magnitude >= 0.1f)
        {
            rb.velocity = direction * speed;
        }
        else
        {
            rb.velocity = Vector3.zero;

            //Evitamos que se mueva si ha chocado con otro objeto con colliders y rigidiBody
            rb.angularVelocity = Vector3.zero;
        }
    }
}
