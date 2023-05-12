using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    protected GameManager GM;

    [Header("Vida")]
    [SerializeField] protected int maxHealth = 10;
    [SerializeField] protected int health;
    [SerializeField] protected bool hasInvencibilityFrames = false;
    [SerializeField] protected float invincibilityTime = 1.0f;
    [SerializeField] protected GameObject blinkingObject;

    public bool invencible = false;
    public bool noMoreBlinking = false;

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

    public int getThisHealth()
    {
        return health;
    }
    public int getMaxHealth()
    {
        return maxHealth;
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

    public virtual void takeDamageIgnoreInvencibility(int damage)
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

    protected void StartBlinking()
	{
        if(hasInvencibilityFrames)
            StartCoroutine(SetInvencibility(invincibilityTime));
        CancelInvoke();
        Invoke("StopBlinking", invincibilityTime);
        InvokeRepeating("Blink", 0, 0.1f);
 
        if(blinkingObject != null)
        {
            CancelInvoke();
            Invoke("StopBlinking", invincibilityTime);
            InvokeRepeating("Blink", 0, 0.1f);
        }
 
	}

    IEnumerator SetInvencibility(float time)
    {
        invencible = true;
        yield return new WaitForSeconds(time);
        invencible = false;
    }

    void Blink()
	{
        if(!noMoreBlinking)
            blinkingObject.SetActive(!blinkingObject.activeSelf);
        else
            blinkingObject.SetActive(false);
    }

    protected void StopBlinking()
    {
        CancelInvoke("Blink");
        if(!noMoreBlinking)
            blinkingObject.SetActive(true);
    }
    
    protected void StopBlinkingForever()
    {
        noMoreBlinking = true;
        CancelInvoke("Blink");
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
