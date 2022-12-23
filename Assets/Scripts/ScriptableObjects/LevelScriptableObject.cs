using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Level")]
public class LevelScriptableObject : ScriptableObject
{
    public string levelName;
    public string sceneName;
    public string description;
    public Sprite sprite;
    public string[] unlocks;
}
