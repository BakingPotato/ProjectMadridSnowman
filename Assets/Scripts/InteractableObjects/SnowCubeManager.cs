using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SnowCubeManager : HealthManager
{
    public enum LootType
	{
        Empty, Coin, PowerUp
	}
    [System.Serializable]
    public struct Loot
	{
        public LootType type;
        public float weight;
	}

    [SerializeField] List<GameObject> powerUps;
    [SerializeField] List<GameObject> coins;
    [SerializeField] Loot[] lootArray;

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
            Destroy(gameObject);
        }
    }

	private void InstanceRandomLoot()
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
		}
	}

    /// <summary>
    /// Relative probability based on weights
    /// </summary>
    /// <returns></returns>
	private LootType GetRandomLootType()
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
