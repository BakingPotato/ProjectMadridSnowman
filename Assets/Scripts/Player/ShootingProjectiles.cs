using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingProjectiles : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] Transform hand;
    [SerializeField] float shootCooldown;

    [SerializeField] bool isPlayer;

    float _currentTime;
    bool _shooting = false;
    bool _tripleShoot = false;
    float _tripleShootAngle = 20;

	public float ShootCooldown { get => shootCooldown; set => shootCooldown = value; }
	public Transform Hand { get => hand; set => hand = value; }
	public bool Shooting { get => _shooting; set => _shooting = value; }

	// Update is called once per frame
	void Update()
    {
        if (Shooting)
        {
            _currentTime -= Time.deltaTime;
            if (_currentTime <= 0)
                Shooting = false;
        }
    }

    public void Shoot(Vector3 direction, int inputDamage = -1)
    {
        if (Shooting)
            return;
        Shooting = true;
        _currentTime = ShootCooldown;

        Projectile proj = Instantiate(projectilePrefab, Hand.position, Quaternion.identity).GetComponent<Projectile>();

        //Esto es para que los proyectiles enemigos no ignoren a otros enemigos
        if (isPlayer)
        {
            proj.IgnoringLayer = gameObject.layer;
            AudioManager.Instance.PlaySFX3DRandomPitch("SnowShoot", transform.position);
        }
        else
            proj.IgnoringLayer = 999;

            proj.Throw(direction, inputDamage);

		if (_tripleShoot)
		{
            Projectile proj2 = Instantiate(projectilePrefab, Hand.position, Quaternion.identity).GetComponent<Projectile>();
			Projectile proj3 = Instantiate(projectilePrefab, Hand.position, Quaternion.identity).GetComponent<Projectile>();
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
            proj2.Throw(Quaternion.Euler(0, _tripleShootAngle, 0) * direction, inputDamage);
            proj3.Throw(Quaternion.Euler(0, -_tripleShootAngle, 0) * direction, inputDamage);
        }
    }

	public void IncreaseShootingSpeed(float multiplier)
	{
        shootCooldown /= multiplier;
    }

	public void IncreaseShootingSpeed_temp(float multiplier, float time)
    {
        StartCoroutine(StartBoost(multiplier, time));
    }

    IEnumerator StartBoost(float multiplier, float time)
    {
        float initial = shootCooldown;
        shootCooldown /= multiplier;
        yield return new WaitForSeconds(time);
        shootCooldown = initial;
    }

    public void ActiveTripleShot(float time)
	{
        StartCoroutine(StartTripleShoot(time));
    }

    IEnumerator StartTripleShoot(float time)
    {
        _tripleShoot = true;
        yield return new WaitForSeconds(time);
        _tripleShoot = false;
    }
}
