using UnityEngine;

[CreateAssetMenu(menuName = "PickUp/ShootingSpeed")]
public class ShootingSpeed : PickUpObject
{
	public float multiplier;
	public float time = -1;
	public override void Apply(GameObject target)
	{
		

		target.GetComponent<PlayerManager>().IncreaseShootingSpeed(multiplier, time);

		AudioManager.Instance.PlaySFX3D("PowerUp", target.transform.position);
	}
}
