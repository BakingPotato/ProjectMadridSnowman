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

			DestroyProjectile();
		}
	}

	void DestroyProjectile()
	{
		//Animation
		//Sound
		Destroy(gameObject);
	}

	public void Throw(Vector3 direction)
	{
		direction.Normalize();
		GetComponent<Rigidbody>().AddForce(direction * impulseForce, ForceMode.Impulse);
	}
	public void Throw(Vector3 direction, int inputDamage)
	{
		damage = inputDamage;
		direction.Normalize();
		GetComponent<Rigidbody>().AddForce(direction * impulseForce, ForceMode.Impulse);
	}
}