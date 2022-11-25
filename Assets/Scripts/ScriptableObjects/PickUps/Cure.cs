using UnityEngine;

[CreateAssetMenu(menuName = "PickUp/Cure")]
public class Cure : PickUpObject
{
	public Sprite icon;
	public int amount;
	public override void Apply(GameObject target)
	{
		GameManager.Instance.CurrentLevelManager.HP += amount;

		//AudioManager.Instance.PlaySFX3D("HealthUp", target.transform.position);
		FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/2D/Player/player_is_healed");
		target.GetComponent<PlayerManager>().PlayPowerUpAnim(icon);
	}
}
