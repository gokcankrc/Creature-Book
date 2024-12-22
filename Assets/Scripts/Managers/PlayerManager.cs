using System.Collections.Generic;
using Ky;
using UnityEngine;
using Logger = Ky.Logger;

public class PlayerManager : Singleton<PlayerManager>, ICombattantGroup
{
    public Creature creaturePrefab;
    public Skill debugCreatureAttack;
    public List<CreatureData> creatureDatas;

    public ICombattant Main => MidCreature;
    public List<ICombattant> Side => new() { TopCreature, BottomCreature };
    public List<ICombattant> All => new() { TopCreature, MidCreature, BottomCreature };
    public List<Creature> AllCombatingCreatures => new() { TopCreature, MidCreature, BottomCreature };

    public ICombattantGroup Opponent { get; set; }
    /*
    public List<CreatureSlot> Slots => CreatureSlotReferencer.I.slots;
    public Creature TopCreature => Slots[0].creature;
    public Creature MidCreature => Slots[1].creature;
    public Creature BottomCreature => Slots[2].creature;
    */
    public List<CreatureSlot> Slots;
    public Creature TopCreature;
    public Creature MidCreature;
    public Creature BottomCreature;

    protected override void Awake()
    {
        base.Awake();

        GameManager.gameInitialized += OnGameInitialized;
    }

    public void GetFightingCreatures()
    {
        Slots = CreatureSlotReferencer.I.slots;
        TopCreature = Slots[0].creature;
        MidCreature = Slots[1].creature;
        BottomCreature = Slots[2].creature;
    }

    private void OnGameInitialized()
    {
        PlayerManager.I.GetFightingCreatures();
        var minIndex = Mathf.Min(Slots.Count, creatureDatas.Count);
        if (minIndex != 6)
            Debug.LogError($"Either slots or creatures are not enough");
        for (int i = 0; i < minIndex; i++)
        {
            var newCreature = Instantiate(creaturePrefab, Slots[i].transform);
            newCreature.SetDataAndReset(creatureDatas[i]);
            Slots[i].Initialize(newCreature);
        }
    }

    public void ItIsYourTurn(ICombattantGroup opponent)
    {
        // TODO: check if everyone is dead
        Logger.Log($"<color=blue>Player Turn</color>", Logger.DomainType.System);
        Opponent = opponent;
        TopCreature.ReadyToAct();
        MidCreature.ReadyToAct();
        BottomCreature.ReadyToAct();
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