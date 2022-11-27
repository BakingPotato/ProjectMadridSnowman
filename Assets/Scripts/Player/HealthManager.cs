using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    protected GameManager GM;

    [Header("Vida")]
    [SerializeField] protected int maxHealth = 10;
    [SerializeField] protected int health;
    [SerializeField] GameObject blinkingObject;

    bool invencible = false;

    // Start is called before the first frame update
    void Start()
    {
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
        return GM.CurrentLevelManager.HP;
    }

    public virtual void takeDamage(int damage)
    {
        if(!invencible)
        {
            //AudioManager.Instance.PlaySFXRandomPitch("PlayerHurt");
            FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/2D/Player/player_is_hurt");
            StartBlinking();
            GM.CurrentLevelManager.HP -= damage;
            GM.CurrentLevelManager.TotalDamage += damage;
            //GM.score.resetMultiplier();
            //GM.score.resetComboFromDamage();
            checkDeath();
        }
    }

    protected void StartBlinking()
	{
        float invencibilityTime = 1.0f;
        StartCoroutine(SetInvencibility(invencibilityTime));
        CancelInvoke();
        Invoke("StopBlinking", invencibilityTime);
        InvokeRepeating("Blink", 0, 0.1f);
	}

    IEnumerator SetInvencibility(float time)
    {
        invencible = true;
        yield return new WaitForSeconds(time);
        invencible = false;
    }

    void Blink()
	{
        blinkingObject.SetActive(!blinkingObject.activeSelf);
    }

    void StopBlinking()
    {
        CancelInvoke("Blink");
        blinkingObject.SetActive(true);
    }

    public void gainHealth(int cure)
    {
        GM.CurrentLevelManager.HP += cure;
    }

    public void resultsScreen()
    {

    }

    protected virtual void checkDeath()
    {
        //if (health <= 0)
        //{
        //    //Aqui se deber�a llamar a un objeto padre que decida que hacer, empezar gameover si es jugador o solo destruir el objeto y sumar puntos si es Enemigo
        //    if (gameObject.tag == "Player")
        //    {

        //    }
        //    //GM.startGameOver();
        //    //Parar las corrutinas
        //    //Destroy(gameObject);
        //}
    }
}
