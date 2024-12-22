using UnityEngine;

public abstract class Skill : ScriptableObject, ICombatAction
{
    public Sprite sprite;

    public abstract void Act(ICombattant attacker, ICombattantGroup actingGroup);
    public abstract int GetCooldownMax();
}