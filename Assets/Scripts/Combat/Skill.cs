using UnityEngine;

public abstract class Skill : ScriptableObject, ICombatAction
{
    public abstract void Act(ICombattant attacker, ICombattantGroup opponents);
}