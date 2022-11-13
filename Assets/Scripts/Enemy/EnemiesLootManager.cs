using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesLootManager : MonoBehaviour
{
    public enum LootType
    {
        Empty, Coin, PowerUp
    }
    public struct Loot
    {
        public LootType type;
        public float weight;
    }

    [SerializeField] protected List<GameObject> powerUps;
    [SerializeField] protected List<GameObject> coins;
    [SerializeField] protected Loot[] lootArray;

    [Tooltip("probabilidades de aparecer para cada objeto")]
    [SerializeField] int probEmpty;
    [SerializeField] List<int> probsPowerUps;
    [SerializeField] List<int> probsCoins;

    private int[] accumProbs;
    
    public void Start()
    {
        //Rellenamos el array de probabilidades acumuladasw
        accumProbs = new int[1 + probsPowerUps.Count + probsCoins.Count];
        accumProbs[0] = probEmpty;
        int index = 1;
        foreach (int powerup in probsPowerUps)
        {
            accumProbs[index] = powerup + accumProbs[index - 1];
            index++;
        }

        foreach (int coin in probsCoins)
        {
            accumProbs[index] = coin + accumProbs[index - 1];
            index++;
        }
    }

    public void InstanceRandomLoot()
    {
        GameObject loot = GetRandomLoot();
        if(loot != null)
        {

            Instantiate(loot, transform.position, Quaternion.identity);
        }
        //switch (randomType)
        //{
        //    case LootType.Empty:
        //        break;
        //    case LootType.Coin:
        //        Instantiate(coins[Random.Range(0, coins.Count)], transform.position, Quaternion.identity);
        //        break;
        //    case LootType.PowerUp:
        //        Instantiate(powerUps[Random.Range(0, powerUps.Count)], transform.position, Quaternion.identity);
        //        break;
        //}
    }

    protected GameObject GetRandomLoot()
    {
        int random = Random.Range(0, accumProbs[accumProbs.Length - 1]);
        //int random = Random.Range(accumProbs[0], accumProbs[accumProbs.Length - 1]);
        int foundIndex = -1;
        //Binarysearch mejor aqui
        for(int i = 0; i< accumProbs.Length; i++)
        {
            bool found = random <= accumProbs[i];
            if(found)
            {
                foundIndex = i;
                break;
            }
        }

        GameObject instance = null;
        if(foundIndex == 0)
        {
            instance = null; //empty
        }else if(foundIndex <= powerUps.Count)
        {
            foundIndex--;
            instance = powerUps[foundIndex];
        }
        else
        {
            foundIndex = foundIndex - powerUps.Count - 1;
            instance = coins[foundIndex];
        }

        return instance;
    }
    //protected LootType GetRandomLootType()
    //{
    //    float totalWeight = 0;
    //    foreach (Loot loot in lootArray)
    //    {
    //        totalWeight += loot.weight;
    //    }

    //    float p = Random.Range(0, totalWeight);
    //    float runningTotal = 0;

    //    foreach (Loot loot in lootArray.Reverse())
    //    {
    //        runningTotal += loot.weight;
    //        if (p < runningTotal) return loot.type;
    //    }

    //    return LootType.Empty;
    //}
}
