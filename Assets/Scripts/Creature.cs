using Sirenix.OdinInspector;
using UnityEngine;

public class Creature : MonoBehaviour, ICombattant
{
    // TODO: Skills
    // TODO: Selecting
    // TODO: Popups
    public float Health { get; set; }
    public CreatureData creatureData;

    public void TakeDamage(ICombattant attacker, Damage damage)
    {
        throw new System.NotImplementedException();
    }

    public void Attack(ICombattant opponent)
    {
        throw new System.NotImplementedException();
    }

    [Button]
    public void DebugEvolve() { }
}