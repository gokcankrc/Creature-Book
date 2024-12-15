using Sirenix.OdinInspector;
using UnityEngine;

public abstract class EntityData : ScriptableObject
{
    public string displayName;
    public Stats stats;
    public Sprite sprite;

    [Button]
    private void SetName()
    {
        displayName = name;
    }
}