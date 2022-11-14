using UnityEngine;

[CreateAssetMenu(menuName = "PickUp/Velocity")]
public class Velocity : PickUpObject
{
	public float amount;
	public float time = -1;
	public override void Apply(GameObject target)
    {

        target.GetComponent<PlayerManager>().increaseMovementSpeed(amount, time);
        AudioManager.Instance.PlaySFX3D("PowerUp", target.transform.position);
    }
}
