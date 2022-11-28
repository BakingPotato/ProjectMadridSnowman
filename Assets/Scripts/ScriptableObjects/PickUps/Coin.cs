using UnityEngine;

[CreateAssetMenu(menuName = "PickUp/Coin")]
public class Coin : PickUpObject
{
	public int amount;
	public override void Apply(GameObject target)
	{
		GameManager.Instance.CurrentLevelManager.Points += amount;

		//AudioManager.Instance.PlaySFX("Coin");
		FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/2D/Player/player_get_coin");
	}
}
