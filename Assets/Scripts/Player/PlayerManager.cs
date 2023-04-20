using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    HealthManager _health;
    CharacterMovement _movement;

    bool _autoShoot = false;

    [Header("Prefabs de PowerUps")]
    [SerializeField] GameObject prefab_Umbrella;
    [SerializeField] ShootingProjectiles shootingProjectiles;
    [SerializeField] GameObject prefabHam;
    [SerializeField] Image powerUpIMG;
    [SerializeField] Animation powerUpAnim;


    // Start is called before the first frame update
    void Start()
    {
        _health = GetComponent<HealthManager>();
        _movement = GetComponent<CharacterMovement>();
    }

	private void Update()
	{
        //if (PlayerPrefs.GetString("ShootingMode", "Pulsar") == "Pulsar")
        //{
        //    if (GameManager.Instance.CurrentLevelManager.GameStarted && !GameManager.Instance.CurrentLevelManager.GamePaused && !GameManager.Instance.CurrentLevelManager.getGameOver() &&
        //        Input.GetKey(KeyCode.Mouse0) && !shootingProjectiles.Shooting)
        //    {
        //        Vector3 dir = _movement.LookPos - shootingProjectiles.Hand.position;
        //        dir.y = 0;

        //        GameManager.Instance.CurrentLevelManager.UIManager.ShowShootingBar(shootingProjectiles.ShootCooldown);
        //        shootingProjectiles.Shoot(shootingProjectiles.Hand.forward + dir);
        //    }
        //}
        //else
        //{
        //    //Tipo disparo 2
        //    if (GameManager.Instance.CurrentLevelManager.GameStarted && !GameManager.Instance.CurrentLevelManager.GamePaused && !GameManager.Instance.CurrentLevelManager.getGameOver() && Input.GetKeyDown(KeyCode.Mouse0))
        //    {
        //        _autoShoot = !_autoShoot;
        //    }

        //    if (_autoShoot && !shootingProjectiles.Shooting)
        //    {
        //        Vector3 dir = _movement.LookPos - shootingProjectiles.Hand.position;
        //        dir.y = 0;

        //        GameManager.Instance.CurrentLevelManager.UIManager.ShowShootingBar(shootingProjectiles.ShootCooldown);
        //        shootingProjectiles.Shoot(shootingProjectiles.Hand.forward + dir);
        //    }
        //}

        if (_autoShoot && !shootingProjectiles.Shooting)
        {
            Vector3 dir = _movement.LookPos - shootingProjectiles.Hand.position;
            dir.y = 0;

            GameManager.Instance.CurrentLevelManager.UIManager.ShowShootingBar(shootingProjectiles.ShootCooldown);


            if (_movement.isController)
            {
                shootingProjectiles.Shoot(shootingProjectiles.Hand.forward);
            }
            else
            {
                shootingProjectiles.Shoot(shootingProjectiles.Hand.forward + dir);
            }
        }
    }

    public void Shoot(InputAction.CallbackContext ctx)
    {
        if (!GameManager.Instance.CurrentLevelManager.GameStarted || GameManager.Instance.CurrentLevelManager.GamePaused || GameManager.Instance.CurrentLevelManager.getGameOver()) return;

        //0 = Pulsar
        //1 = Alternar
        if (PlayerPrefs.GetInt("ShootingMode", 0) == 0)
        {
            if (ctx.phase == InputActionPhase.Performed)
                _autoShoot = true;
            else if (ctx.phase == InputActionPhase.Canceled)
                _autoShoot = false;
        }
        else
        {
            if(ctx.phase == InputActionPhase.Performed)
                _autoShoot = !_autoShoot;
        }
    }

    public void PlayPowerUpAnim(Sprite s)
	{
        powerUpIMG.sprite = s;
        powerUpAnim.Rewind();
        powerUpAnim.Play();
	}

	public void increaseMovementSpeed(float amount, float time)
    {
        if(time < 0)
          _movement.increaseSpeed(amount);
        else
          _movement.increaseSpeed_temp(amount, time);
    }

    public void IncreaseShootingSpeed(float multiplier, float time)
    {
        if (time < 0)
            shootingProjectiles.IncreaseShootingSpeed(multiplier);
        else
            shootingProjectiles.IncreaseShootingSpeed_temp(multiplier, time);
    }

    public void SetTripleShot(float time)
	{
        shootingProjectiles.ActiveTripleShot(time);
	}

    public void IncreaseShotSize(float time)
    {
        shootingProjectiles.IncreaseShotSize(time);
    }

    public void instantiateUmbrella(float time)
    {
        StartCoroutine(powerUpUmbrella(time));
    }
    public void instantiateHam(float time)
    {
        StartCoroutine(powerUpHam(time));
    }


    IEnumerator powerUpUmbrella(float time)
    {
        //Lo instanciamos en el padre de 
        prefab_Umbrella.GetComponent<UmbrellaController>().player = this.gameObject;
        GameObject actualUmbrella = Instantiate(prefab_Umbrella, transform.parent);

        yield return new WaitForSeconds(time);

        //Destruimos el objeto
        Destroy(actualUmbrella);
    }
    IEnumerator powerUpHam(float time)
    {
        //Lo instanciamos en el padre de 
        prefabHam.GetComponent<HamController>().player = this.gameObject;
        GameObject actualHam = Instantiate(prefabHam, transform.parent);

        yield return new WaitForSeconds(time);

        //Destruimos el objeto
        Destroy(actualHam);
    }

    IEnumerator powerUpShootingSpeed(float time)
    {
        //Lo instanciamos en el padre de 
        prefabHam.GetComponent<HamController>().player = this.gameObject;
        GameObject actualHam = Instantiate(prefabHam, transform.parent);

        yield return new WaitForSeconds(time);

        //Destruimos el objeto
        Destroy(actualHam);
    }

}
