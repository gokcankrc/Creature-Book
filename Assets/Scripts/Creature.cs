using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;

public class Creature : Entity, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    // TODO: Skills
    // TODO: Selecting
    // TODO: Popups
    [PropertyOrder(0)] public CreatureSlot currentSlot;
    [PropertyOrder(0)] public CreatureData creatureData;
    [PropertyOrder(10)] public SkillHandler skillHandler0;
    [PropertyOrder(10)] public SkillHandler skillHandler1;
    [PropertyOrder(10)] public SkillHandler skillHandler2;

    private bool hasTurn;

    public bool CanAct => hasTurn & !Stats.isDead & CombatManager.I.InCombat;
    protected override EntityData EntityData => creatureData;

    private void Start()
    {
        Reset();
    }

    public void SetDataAndReset(CreatureData newCreatureData)
    {
        creatureData = newCreatureData;
        Reset();
    }

    public override void Reset()
    {
        base.Reset();
        group = PlayerManager.I;
        skillHandler0.Initialize(this, creatureData.skill0Data);
        skillHandler1.Initialize(this, creatureData.skill1Data);
        skillHandler2.Initialize(this, creatureData.skill2Data);
    }

    private void Update()
    {
        throw new NotImplementedException();
    }

    public override void TakeDamage(ICombattant attacker, Damage damage)
    {
        if (!CanTakeDamage()) return;
        base.TakeDamage(attacker, damage);
        if (IsDead)
        {
            PlayerManager.I.CreatureDied(this);
        }
    }

    public override void ReadyToAct()
    {
        base.ReadyToAct();
        skillHandler0.OnReadyToAct();
        skillHandler1.OnReadyToAct();
        skillHandler2.OnReadyToAct();
        hasTurn = true;
    }

    public override void Act(ICombatAction action)
    {
        if (!CanAct) return;
        base.Act(action);
        hasTurn = false;
        skillHandler0.OnActed();
        skillHandler1.OnActed();
        skillHandler2.OnActed();
        PlayerManager.I.CreatureUsedTurn(this);
    }

    public bool CanEvolve(bool ignoreResources = false)
    {
        if (ignoreResources)
        {
            return (creatureData.nextEvo != null);
        }
        else
        {
            return (creatureData.nextEvo != null && ResourceManager.I.IsHigher(creatureData.energyForNextEvo));
        }
    }

    [Button]
    public void DebugEvolve()
    {
        Evolve(false);
    }

    public void Evolve(bool ignoreResources = false)
    {
        if (GameManager.gameState == GameManager.State.InCombat) return;
        if (CanEvolve(ignoreResources))
        {
            if (!ignoreResources)
                ResourceManager.I.Subtract(creatureData.energyForNextEvo);

            creatureData = creatureData.nextEvo;

            Reset();
        }
    }

    public string[] GenerateTooltipText()
    {
        string[] result = new string [2];

        if (creatureData.stats.isDead)
        {
            result[0] = "FAINTED<br>";
            result[1] = "<br>";
        }
        else
        {
            result[0] = "";
            result[1] = "";
        }

        result[0] += "HP: " + creatureData.stats.healthCurrent + " / " + creatureData.stats.healthMax.Calculate();
        result[0] += "<br><br>Damage: ";
        result[0] += "<br> Physical: " + creatureData.stats.physicalDamage.Calculate();
        result[0] += "<br> Magical: " + creatureData.stats.magicDamage.Calculate();
        /*result [1] goes into the second line, starts with a <br> to account for the HP line could add the name as the first line*/
        result[1] += "<br><br>Armor: ";
        result[1] += "<br> Physical: " + creatureData.stats.armor.Calculate();
        result[1] += "<br> Magical: " + creatureData.stats.magicArmor.Calculate();
        return result;
    }

    public void OnBeginDrag(PointerEventData data) { }

    public void OnDrag(PointerEventData data)
    {
        if (GameManager.gameState == GameManager.State.InCombat) return;
        if (data.dragging)
        {
            transform.position = new Vector3(data.position.x, data.position.y, 0);
        }
    }

    public void OnEndDrag(PointerEventData data)
    {
        if (GameManager.gameState == GameManager.State.InCombat) return;
        CreatureSlot newSlot = CreatureSlotReferencer.I.GetNearestSlot(transform.position);
        if (newSlot != currentSlot && newSlot != null)
        {
            CreatureSlot.Swap(newSlot, currentSlot);
        }
        else
        {
            transform.localPosition = Vector3.zero;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        TooltipManager.I.ActivateOnPosition(this);
    }
}