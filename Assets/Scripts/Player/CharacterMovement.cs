using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    Rigidbody rb;

    Vector3 lookPos;

    [Header("Variable de movimiento")]
    Animator anim;

    public float speed = 50;
    [SerializeField] const float MAX_SPEED = 20;
    [SerializeField] const float NORMAL_SPEED = 4;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();

        speed = NORMAL_SPEED;
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

        //Aplicamos el normalized para que los diagonales no sean más rápido
        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;

        //Si hay movimiento
        if (direction.magnitude >= 0.1f)
        {
            rb.velocity = direction * speed;
            anim.SetBool("Move", true);
        }
        else
        {
            rb.velocity = Vector3.zero;

            //Evitamos que se mueva si ha chocado con otro objeto con colliders y rigidiBody
            rb.angularVelocity = Vector3.zero;
            anim.SetBool("Move", false);
        }
    }

    public void increaseSpeed(float amount)
    {
        speed += amount;

        if(speed > MAX_SPEED)
        {
            speed = MAX_SPEED;
        }
    }

    public void decreaseSpeed(float amount)
    {
        speed -= amount;

        if (speed < NORMAL_SPEED)
        {
            speed = NORMAL_SPEED;
        }
    }

    public void increaseSpeed_temp(float amount, float time)
    {
        StartCoroutine(startBoost(amount, time));
    }

    IEnumerator startBoost(float amount, float time)
    {
        increaseSpeed(amount);
        yield return new WaitForSeconds(time);
        decreaseSpeed(amount);
    }

}
