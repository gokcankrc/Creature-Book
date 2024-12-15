using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Logger = Ky.Logger;

public class Creature : Entity,IBeginDragHandler, IDragHandler, IEndDragHandler
{
    // TODO: Skills
    // TODO: Selecting
    // TODO: Popups
    public CreatureData creatureData;
    public CreatureSlot currentSlot;
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

    public override void Act(ICombattantGroup opponents, ICombatAction action)
    {
        if (!CanAct) return;
        base.Act(opponents, action);
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
        //transform.localPosition = Vector3.zero;
    }

    [Button]
    public void DebugEvolve() { }
    public void OnBeginDrag(PointerEventData data)
    {
        
        
    }

    public void OnDrag(PointerEventData data)
    {
        if (GameManager.gameState == GameManager.State.InCombat) return;
        if (data.dragging){
            transform.position = new Vector3(data.position.x,data.position.y,0);
        }
    }

    public void OnEndDrag(PointerEventData data)
    {
        if (GameManager.gameState == GameManager.State.InCombat) return;
        CreatureSlot newSlot = CreatureSlotReferencer.I.GetNearestSlot(this.transform.position);
        if (newSlot != this.currentSlot && newSlot != null){
           CreatureSlot.Swap(newSlot, this.currentSlot);
        }
        else {
            transform.localPosition = Vector3.zero;
        }
        
    }
}