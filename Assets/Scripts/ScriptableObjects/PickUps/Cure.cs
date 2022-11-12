using UnityEngine;

[CreateAssetMenu(menuName = "PickUp/Cure")]
public class Cure : PickUpObject
{
	public int amount;
	public override void Apply(GameObject target)
	{
		GameManager.Instance.CurrentLevelManager.HP += amount;

		AudioManager.Instance.PlaySFX("HealthUp");
	}
}
