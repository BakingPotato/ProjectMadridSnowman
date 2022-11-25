using UnityEngine;

[CreateAssetMenu(menuName = "PickUp/TripleShoot")]
public class TripleShoot : PickUpObject
{
	public Sprite icon;
	public float time = -1;
	public override void Apply(GameObject target)
	{
		target.GetComponent<PlayerManager>().SetTripleShoot(time);
		//AudioManager.Instance.PlaySFX3D("PowerUp", target.transform.position);
		FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/2D/Player/player_get_power_up");
		target.GetComponent<PlayerManager>().PlayPowerUpAnim(icon);
	}
}
