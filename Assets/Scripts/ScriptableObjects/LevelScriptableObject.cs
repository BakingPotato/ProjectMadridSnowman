using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Level")]
public class LevelScriptableObject : ScriptableObject
{
    public string levelName;
    public string levelName_EN;
    public string sceneName;
    [TextArea]
    public string description;
    [TextArea]
    public string description_EN;
    public Sprite sprite;
    public string[] unlocks;
}
