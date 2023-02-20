using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileHolder : MonoBehaviour
{

	[SerializeField] float lifeSpan;

	[SerializeField] float impulseForce;

    private void Start()
    {
		//Les damos a los proyectiles la indicación de que ignoren su propio layer
		foreach (Projectile p in GetComponentsInChildren<Projectile>())
		{
			p.IgnoringLayer = gameObject.layer;
		}
	}

    private void Update()
	{
		lifeSpan -= Time.deltaTime;
		if (lifeSpan <= 0)
			DestroyProjectile();
	}

	void DestroyProjectile()
	{
		Destroy(gameObject);
	}

	public void Throw(Vector3 direction, int inputDamage = -1)
	{
		direction.Normalize();
		GetComponent<Rigidbody>().AddForce(direction * impulseForce, ForceMode.Impulse);
	}

}
