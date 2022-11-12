using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [Header("Vida")]
    [SerializeField] protected int maxHealth = 10;
    [SerializeField] protected int health;

    protected GameManager GM;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;

        GM = GameManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    takeDamage(2);
        //}
        //if (Input.GetKeyDown(KeyCode.A))
        //{
        //    gainHealth(1);
        //}
    }

    public int getHealth()
    {
        return health;
    }

    public virtual void takeDamage(int damage)
    {
        health -= damage;
        //UI.updateHealthBar(health);
        //GM.score.resetMultiplier();
        //GM.score.resetComboFromDamage();

        checkDeath();
    }

    public void gainHealth(int cure)
    {
        health += cure;
        //UI.updateHealthBar(health);
    }

    protected void checkDeath()
    {
        if (health <= 0)
        {
            //Aqui se debería llamar a un objeto padre que decida que hacer, empezar gameover si es jugador o solo destruir el objeto y sumar puntos si es Enemigo
            if (gameObject.tag == "Player")
            {

            }
            //GM.startGameOver();
            //Parar las corrutinas
            //Destroy(gameObject);
        }
    }
}
