using UnityEngine;

[CreateAssetMenu(menuName = "PickUp/Velocity")]
public class Velocity : PickUpObject
{
    public Sprite icon;
    public float amount;
	public float time = -1;
	public override void Apply(GameObject target)
    {
        target.GetComponent<PlayerManager>().increaseMovementSpeed(amount, time);
		//AudioManager.Instance.PlaySFX3D("PowerUp", target.transform.position);
		FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/2D/Player/player_get_power_up");
		target.GetComponent<PlayerManager>().PlayPowerUpAnim(icon);
	}
}
