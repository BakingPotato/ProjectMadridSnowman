using UnityEngine;

[CreateAssetMenu(menuName = "PickUp/Umbrella")]
public class Umbrella : PickUpObject
{
	public float time = 10;

	public override void Apply(GameObject target)
	{
		target.GetComponent<PlayerManager>().instantiateUmbrella(time);

		//AudioManager.Instance.PlaySFX("VelocityUp");
		FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/2D/Player/player_get_power_up");

	}
}
