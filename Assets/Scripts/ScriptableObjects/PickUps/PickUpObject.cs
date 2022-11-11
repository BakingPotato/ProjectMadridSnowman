using UnityEngine;

public abstract class PickUpObject : ScriptableObject
{
    public abstract void Apply(GameObject target);
}
