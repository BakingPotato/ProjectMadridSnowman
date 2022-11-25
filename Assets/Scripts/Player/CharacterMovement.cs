using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private static GameManager GM;

    Rigidbody rb;

    Vector3 lookPos;

    [Header("Variable de movimiento")]
    Animator anim;

    public float speed = 50;
    [SerializeField] float MAX_SPEED = 8f;
    [SerializeField] float NORMAL_SPEED = 4.5f;
    [SerializeField] LayerMask groundLayer;

	public Vector3 LookPos { get => lookPos; set => lookPos = value; }

    private Vector3 inputDirection;

	// Start is called before the first frame update
	void Start()
    {
        GM = GameManager.Instance;

        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();

        speed = NORMAL_SPEED;
    }

    private void Update()
    {
        transform.position = new Vector3(transform.position.x, 1.13f, transform.position.z);
        //El jugador puede moverse mientras no acabe la partida
        if (GM.CurrentLevelManager.getGameOver() == false)
        {
            RotateCharacterToMouse();
        }
        else
        {
            StopMovement();
        }
    }

    private void FixedUpdate()
    {
        //El jugador puede moverse mientras no acabe la partida
        if(GM.CurrentLevelManager.getGameOver() == false)
        {
            GetMovementInput();
        }
    }

    private void RotateCharacterToMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        //Layer 11 es la del suelo
        if (Physics.Raycast(ray, out hit, 100, groundLayer))
        {
            LookPos = hit.point;
        }

        Vector3 lookDir = LookPos - transform.position;
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
            //AudioManager.Instance.PlaySFX("SnowWalk", transform.position);
            FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/2D/Player/player_moves");
            MovePlayer(direction);
        }
        else
        {
            StopMovement();
        }
    }

    private void MovePlayer(Vector3 direction)
    {
        rb.angularVelocity = Vector3.zero;
        rb.velocity = direction * speed;
        anim.SetBool("Move", true);
    }

    //private void GetMovementInput()
    //{
    //    inputDirection = Vector3.zero;

    //    if (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.A))
    //    {
    //        inputDirection += new Vector3(-1, 0, 0);
    //    }
    //    if (Input.GetKey(KeyCode.D))
    //    {
    //        inputDirection += new Vector3(1, 0, 0);
    //    }
    //    if (Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.W))
    //    {
    //        inputDirection += new Vector3(0, 0, 1);
    //    }
    //    if (Input.GetKey(KeyCode.S))
    //    {
    //        inputDirection += new Vector3(0, 0, -1);
    //    }

    //    MovePlayer(inputDirection);
    //}


    //private void MovePlayer(Vector3 direction)
    //{
    //    Vector3 moveDirection = new Vector3(Mathf.Clamp(direction.x * 2, -1, 1), 0, Mathf.Clamp(direction.z * 2, -1, 1));
    //    rb.AddTorque(moveDirection * 5);
    //    rb.AddForce(moveDirection * 5, ForceMode.Force);
    //}



    private void StopMovement()
    {
        rb.velocity = Vector3.zero;

        //Evitamos que se mueva si ha chocado con otro objeto con colliders y rigidiBody
        rb.angularVelocity = Vector3.zero;
        anim.SetBool("Move", false);
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
