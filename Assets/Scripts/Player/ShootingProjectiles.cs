using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingProjectiles : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] Transform hand;
    [SerializeField] float shootCooldown;
    [SerializeField] float minShootCooldown = 0.15f;
    [SerializeField] bool auto = false;
    [SerializeField] bool canShoot = true;

    [SerializeField] bool isPlayer;

    float _currentTime;
    bool _shooting = false;
    bool _tripleShoot = false;
    float _tripleShootAngle = 20;
    float _scaleFactor = 1;
    int _buffDamage = 0;

	public float ShootCooldown { get => shootCooldown; set => shootCooldown = value; }
	public Transform Hand { get => hand; set => hand = value; }
	public bool Shooting { get => _shooting; set => _shooting = value; }
    //added by me
    public Animator enemyAnimator;


    private void Start()
    {
        if (auto || (!canShoot && isPlayer))
        {
            Shooting = true;

        }
    }
    // Update is called once per frame
    void Update()
    {
        if (!canShoot && isPlayer)
        {
            return;
        }
        else {

            if (Shooting)
            {
                _currentTime -= Time.deltaTime;
                if (_currentTime <= 0)
                {
                    Shooting = false;
                    if (auto)
                    {
                        Shoot(transform.forward, 1);

                    }
                }
            }
        }

    }

    public void Shoot(Vector3 direction, int inputDamage = -1)
    {
        if (Shooting && !auto)
            return;
        Shooting = true;
        _currentTime = ShootCooldown;

        Projectile proj = Instantiate(projectilePrefab, Hand.position, Quaternion.identity).GetComponent<Projectile>();
        proj.transform.localScale *= _scaleFactor;
        //Esto es para que los proyectiles enemigos no ignoren a otros enemigos
        if (isPlayer)
        {
            proj.IgnoringLayer = gameObject.layer;
            //AudioManager.Instance.PlaySFX3DRandomPitch("SnowShoot", transform.position);
            FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/2D/Player/player_shoots");
        }
        else
            proj.IgnoringLayer = 999;

        proj.Damage += _buffDamage;
        proj.Throw(direction, inputDamage);
        //added by me, hace la animacion de atacar
        if(enemyAnimator) enemyAnimator.SetTrigger("Attack");

        if (_tripleShoot)
		{
            Projectile proj2 = Instantiate(projectilePrefab, Hand.position, Quaternion.identity).GetComponent<Projectile>();
            proj2.transform.localScale *= _scaleFactor;
            Projectile proj3 = Instantiate(projectilePrefab, Hand.position, Quaternion.identity).GetComponent<Projectile>();
            proj3.transform.localScale *= _scaleFactor;
            if (isPlayer)
			{
                proj2.IgnoringLayer = gameObject.layer;
                proj3.IgnoringLayer = gameObject.layer;
            }
			else
			{
                proj2.IgnoringLayer = 999;
                proj3.IgnoringLayer = 999;
			}
            proj2.Damage += _buffDamage;
            proj2.Throw(Quaternion.Euler(0, _tripleShootAngle, 0) * direction, inputDamage);
            proj3.Damage += _buffDamage;
            proj3.Throw(Quaternion.Euler(0, -_tripleShootAngle, 0) * direction, inputDamage);
        }
    }

	public void IncreaseShootingSpeed(float multiplier)
	{
        shootCooldown += multiplier;
    }

    public void DecreaseShootingSpeed(float multiplier)
    {
        shootCooldown -= multiplier;
    }

    public void IncreaseShootingSpeed_temp(float multiplier, float time)
    {
        StartCoroutine(StartShootingSpeedBoost(multiplier, time));
    }


    IEnumerator StartShootingSpeedBoost(float speedBoost, float time)
    {
        float initial = shootCooldown;
        //Esperamos a aplicar el boost, shootcoldown debe ser mayor que el minimo para poder aplicarlo
        while (shootCooldown <= minShootCooldown)
        {
            yield return new WaitForEndOfFrame();
        }

        //Nos aseguramos de que no vamos a pasar el limite
        if (shootCooldown - speedBoost < minShootCooldown)
            shootCooldown = minShootCooldown;
        else
            shootCooldown -= speedBoost;

        //Esperamos a que se pase el boost para revertirlo
        yield return new WaitForSeconds(time);
        shootCooldown += speedBoost;

    }

    public void ActiveTripleShot(float time)
	{
        StopCoroutine("StartTripleShot");
        StartCoroutine(StartTripleShot(time));
    }

    public void Activate_TripleShot()
    {
        _tripleShoot = true;
    }

    public void Deactivate_TripleShot()
    {
        _tripleShoot = false;
    }


    IEnumerator StartTripleShot(float time)
    {
        _tripleShoot = true;
        yield return new WaitForSeconds(time);
        _tripleShoot = false;
    }

    public void IncreaseShotSize(float time)
    {
        StartCoroutine(StartShotSize(time));
    }

    public void IncreaseShotSize_Permanent()
    {
        _scaleFactor += 0.5f;
        _buffDamage += 1;
    }

    public void DecreaseShotSize()
    {
        _scaleFactor -= 0.5f;
        _buffDamage -= 1;
    }

    IEnumerator StartShotSize(float time)
    {
        //Subimos el tamaño y el daño
        _scaleFactor += 0.5f;
        _buffDamage += 1;
        yield return new WaitForSeconds(time);
        //Reducimos el tamaño y daño, con esto aseguramos que por muchos powerups que se acumulen, siempre volveremos al tamaño original de forma progresiva
        _scaleFactor -= 0.5f;
        _buffDamage -= 1;
    }
}
