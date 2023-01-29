using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField] PickUpObject pickUpEffect;
	public ParticleSystem particles;

    private void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Player")
        {
			pickUpEffect.Apply(other.gameObject);
			if (particles) TriggerParticles();
			Destroy(transform.parent.gameObject);

		}
	}
	private void TriggerParticles()
	{
		ParticleSystem expEff = Instantiate(particles);
		expEff.transform.position = transform.position;
		expEff.Play();

		//snowInst.GetComponent<ProjectileSnow>().Live();
	}

}
