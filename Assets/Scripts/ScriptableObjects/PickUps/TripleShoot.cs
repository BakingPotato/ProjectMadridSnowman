using UnityEngine;

[CreateAssetMenu(menuName = "PickUp/TripleShoot")]
public class TripleShoot : PickUpObject
{
	public float time = -1;
	public override void Apply(GameObject target)
	{

		target.GetComponent<PlayerManager>().SetTripleShoot(time);
		AudioManager.Instance.PlaySFX3D("PowerUp", target.transform.position);
	}
}
