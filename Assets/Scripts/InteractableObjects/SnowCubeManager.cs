using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SnowCubeManager : HealthManager
{
    public enum LootType
	{
        Empty, Coin, PowerUp, Cure
	}
    [System.Serializable]
    public struct Loot
	{
        public LootType type;
        public float weight;
	}

    [SerializeField] protected List<GameObject> powerUps;
    [SerializeField] protected List<GameObject> coins;
    [SerializeField] protected List<GameObject> cures;
    [SerializeField] protected Loot[] lootArray;

    public void Start()
    {
        GM = GameManager.Instance;
    }

    public override void takeDamage(int amount)
    {
        health -= amount;

        checkDeath();
    }
    protected override void checkDeath()
    {
        if (health <= 0)
        {
            InstanceRandomLoot();
            GM.CurrentLevelManager.BoxCount++;
            //AudioManager.Instance.PlaySFX3D("BreakBox", this.gameObject.transform.position);
            FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/3D/Objects/box_is_broken", transform.position);
            Destroy(gameObject);
        }
    }

	protected void InstanceRandomLoot()
	{
        LootType randomType = GetRandomLootType();

		switch (randomType)
		{
			case LootType.Empty:
				break;
			case LootType.Coin:
                Instantiate(coins[Random.Range(0, coins.Count)], transform.position, Quaternion.identity);
				break;
			case LootType.PowerUp:
                Instantiate(powerUps[Random.Range(0, powerUps.Count)], transform.position, Quaternion.identity);
                break;
            case LootType.Cure:
                Instantiate(cures[Random.Range(0, cures.Count)], transform.position, Quaternion.identity);
                break;
        }
	}

    /// <summary>
    /// Relative probability based on weights
    /// </summary>
    /// <returns></returns>
	protected virtual LootType GetRandomLootType()
    {
        float totalWeight = 0;
        foreach (Loot loot in lootArray)
        {
            totalWeight += loot.weight;
        }

        float p = Random.Range(0, totalWeight);
        float runningTotal = 0;

        foreach (Loot loot in lootArray.Reverse())
        {
            runningTotal += loot.weight;
            if (p < runningTotal) return loot.type;
        }

        return LootType.Empty;
    }
}
