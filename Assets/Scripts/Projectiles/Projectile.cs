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
	public int Damage { get => damage; set => damage = value; }

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
				hM.takeDamage(Damage);

			DestroyProjectile();
		}
	}

	void DestroyProjectile()
	{
		//AudioManager.Instance.PlaySFX3DRandomPitch("SnowImpact", transform.position);
		FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/3D/Objects/snowball_hits");
		TriggerSnow();
		Destroy(gameObject);
	}

	public void Throw(Vector3 direction, int inputDamage = -1)
	{
		Damage = (inputDamage == -1) ? Damage : inputDamage;
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
