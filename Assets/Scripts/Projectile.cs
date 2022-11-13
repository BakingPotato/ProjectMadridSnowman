using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
	[SerializeField] int damage = 1;

	[SerializeField] float lifeSpan;

	[SerializeField] float impulseForce;

	LayerMask _ignoringLayer;

	public LayerMask IgnoringLayer { get => _ignoringLayer; set => _ignoringLayer = value; }

	public GameObject snow;

	private void Update()
	{
		lifeSpan -= Time.deltaTime;
		if (lifeSpan <= 0)
			DestroyProjectile();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer != IgnoringLayer)
		{
			HealthManager hM = other.GetComponent<HealthManager>();
			if (hM)
				hM.takeDamage(damage);
			else if(other.gameObject.name == "SnowBall(Clone)a")
            {
				//Mantener Combo
				Debug.Log("PinPon");
            }

			DestroyProjectile();
		}
	}

	void DestroyProjectile()
	{
		//Animation
		//Sound
		TriggerSnow();
		Destroy(gameObject);
	}

	public void Throw(Vector3 direction, int inputDamage = -1)
	{
		damage = (inputDamage == -1) ? damage : inputDamage;
		direction.Normalize();
		GetComponent<Rigidbody>().AddForce(direction * impulseForce, ForceMode.Impulse);
	}

	private void TriggerSnow()
	{
		GameObject snowSnowBall = Instantiate(snow);
		snowSnowBall.transform.position = this.gameObject.transform.position;
		//snowInst.GetComponent<ProjectileSnow>().Live();
	}
}
