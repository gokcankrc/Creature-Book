using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using Logger = Ky.Logger;

public class Creature : EntityBase
{
    // TODO: Skills
    // TODO: Selecting
    // TODO: Popups
    public CreatureData creatureData;

    private bool hasTurn;
    private bool CanAct => hasTurn & !Stats.isDead & CombatManager.I.InCombat;

    public override void TakeDamage(ICombattant attacker, Damage damage)
    {
        if (!CanTakeDamage()) return;
        base.TakeDamage(attacker, damage);
        if (IsDead)
        {
            Logger.Log($"Creature death stuff here");
        }
    }

    public void Act(ICombattantGroup opponents, ICombatAction action)
    {
        if (!CanAct) return;
        action.Act(this, opponents);
        PlayerManager.I.CreatureUsedTurn(this);
        hasTurn = false;
    }

    public void ReadyToAct()
    {
        hasTurn = true;
    }

    public void SetDataAndReset(CreatureData newCreatureData)
    {
        creatureData = newCreatureData;
        Reset();
    }

    public void Reset()
    {
        Stats = creatureData.stats.DeepCopy();
        Stats.SetHealthMax();
        gameObject.name = creatureData.displayName;
        image.sprite = creatureData.sprite;
        image.SetNativeSize();
    }

    [Button]
    public void DebugEvolve() { }
}