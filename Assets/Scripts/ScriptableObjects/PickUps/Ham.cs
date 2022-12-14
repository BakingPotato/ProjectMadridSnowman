using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PickUp/Ham")]
public class Ham : PickUpObject
{
	public float time = 10;

	public override void Apply(GameObject target)
	{


		target.GetComponent<PlayerManager>().instantiateHam(time);

		//AudioManager.Instance.PlaySFX3D("PowerUp", target.transform.position);
		FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/2D/Player/player_get_power_up");
	}
}
