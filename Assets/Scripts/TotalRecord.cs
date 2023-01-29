using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TotalRecord : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI record;

    // Start is called before the first frame update
    void Start()
    {
        int totalRecord = 0;

        foreach (SaveManager.LevelData levelData in SaveManager.GameDataInstance.levels)
        {
            totalRecord += levelData.record;
        }

        record.text = "Puntuación Media: " + (totalRecord / SaveManager.GameDataInstance.levels.Length);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
