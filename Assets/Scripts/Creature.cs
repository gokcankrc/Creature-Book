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
    public bool CanAct => hasTurn & !Stats.isDead & CombatManager.I.InCombat;

    protected override EntityData EntityData => creatureData;

    public override void TakeDamage(ICombattant attacker, Damage damage)
    {
        if (!CanTakeDamage()) return;
        base.TakeDamage(attacker, damage);
        if (IsDead)
        {
            PlayerManager.I.CreatureDied(this);
        }
    }

    public override void Act(ICombattantGroup opponents, ICombatAction action)
    {
        if (!CanAct) return;
        base.Act(opponents, action);
        hasTurn = false;
        PlayerManager.I.CreatureUsedTurn(this);
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
    bool CanEvolve (bool ignoreResources = false){
        if (ignoreResources){
            return (this.creatureData.nextEvo != null);
        }
        else {
            return (this.creatureData.nextEvo != null && ResourceManager.I.IsHigher(this.creatureData.energyForNextEvo));
        }
        

    }

    [Button]
    public void DebugEvolve() { 
        this.Evolve(false);
    }
    public void Evolve (bool ignoreResources = false){
        if (GameManager.gameState == GameManager.State.InCombat) return;
        if (this.CanEvolve(ignoreResources)){
            if (ignoreResources){
                this.creatureData = this.creatureData.nextEvo;
            }
            else {
                ResourceManager.I.Subtract(this.creatureData.energyForNextEvo);
                this.creatureData = this.creatureData.nextEvo;
            }
            this.Reset();
        }
    }
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