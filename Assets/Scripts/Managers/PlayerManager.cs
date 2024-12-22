using System.Collections.Generic;
using System.Linq;
using Ky;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;
using Logger = Ky.Logger;

public class PlayerManager : Singleton<PlayerManager>, ICombattantGroup
{
    public Creature creaturePrefab;
    public List<CreatureData> creatureDatas;
    [FormerlySerializedAs("Slots")] public List<CreatureSlot> slots;
    [ReadOnly] public Creature topCreature;
    [ReadOnly] public Creature midCreature;
    [ReadOnly] public Creature bottomCreature;

    public ICombattantGroup Opponent { get; set; }

    public ICombattant Main => midCreature;
    public List<ICombattant> Side => new() { topCreature, bottomCreature };
    public List<ICombattant> All => new() { topCreature, midCreature, bottomCreature };
    public List<Creature> AllCombatingCreatures => new() { topCreature, midCreature, bottomCreature };
    public List<Creature> AllCreatures => slots.Select(slot => slot.creature).ToList();

    protected virtual void Start()
    {
        CombatManager.I.combatStarting += OnCombatStart;
        CombatManager.I.combatEnding += OnCombatEnd;
        GameManager.gameInitialized += OnGameInitialized;
    }

    public void GetFightingCreatures()
    {
        slots = CreatureSlotReferencer.I.slots;
        topCreature = slots[0].creature;
        midCreature = slots[1].creature;
        bottomCreature = slots[2].creature;
    }

    private void OnGameInitialized()
    {
        PlayerManager.I.GetFightingCreatures();
        var minIndex = Mathf.Min(slots.Count, creatureDatas.Count);
        if (minIndex != 6)
            Debug.LogError($"Either slots or creatures are not enough");
        for (int i = 0; i < minIndex; i++)
        {
            var newCreature = Instantiate(creaturePrefab, slots[i].transform);
            newCreature.SetDataAndReset(creatureDatas[i]);
            slots[i].Initialize(newCreature);
        }
    }

    private void OnCombatStart()
    {
        foreach (var creature in AllCreatures)
            creature.Deactivate();

        foreach (var creature in AllCombatingCreatures)
            creature.Activate();
    }

    private void OnCombatEnd()
    {
        topCreature.Reset();
        midCreature.Reset();
        bottomCreature.Reset();
        foreach (var creature in AllCombatingCreatures)
            creature.Deactivate();
    }

    public void ItIsYourTurn(ICombattantGroup opponent)
    {
        // TODO: check if everyone is dead
        Logger.Log($"<color=blue>Player Turn</color>", Logger.DomainType.System);
        Opponent = opponent;
        topCreature.ReadyToAct();
        midCreature.ReadyToAct();
        bottomCreature.ReadyToAct();
    }

    public void CreatureUsedTurn(Creature creature)
    {
        if (ShouldTurnBeOver())
        {
            CombatManager.I.NextTurn();
        }
    }

    private bool ShouldTurnBeOver()
    {
        bool turnShouldBeOver = true;
        foreach (var combattant in AllCombatingCreatures)
        {
            if (combattant.CanAct)
            {
                turnShouldBeOver = false;
            }
        }

        return turnShouldBeOver;
    }

    public void CreatureDied(Creature creatureThatDied)
    {
        bool allCombatantIsDead = true;
        foreach (var creature in AllCombatingCreatures)
        {
            if (!creature.IsDead)
            {
                allCombatantIsDead = false;
            }
        }

        if (allCombatantIsDead)
        {
            Logger.LogError("Player Died Completely", Logger.DomainType.Critical);
        }
    }
}