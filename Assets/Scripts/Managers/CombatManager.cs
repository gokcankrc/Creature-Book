using System;
using Ky;

public class CombatManager : Singleton<CombatManager>
{
    public ICombattantGroup currentGroup;
    public Action combatStarting;
    public Action combatEnding;

    public Creature TopCreature => PlayerManager.I.topCreature;
    public Creature MidCreature => PlayerManager.I.midCreature;
    public Creature BottomCreature => PlayerManager.I.bottomCreature;
    public ICombattantGroup PlayerGroup => PlayerManager.I;
    public ICombattant Enemy => EnemyManager.currentEnemy;
    public ICombattantGroup EnemyGroup => EnemyManager.I;
    public bool InCombat => GameManager.gameState == GameManager.State.InCombat;

    public void StartCombat()
    {
        EnemyManager.I.Opponent = PlayerManager.I;
        PlayerManager.I.Opponent = EnemyManager.I;
        PlayerManager.I.GetFightingCreatures();
        currentGroup = PlayerManager.I;
        GameManager.I.ChangeState(GameManager.State.InCombat);
        combatStarting?.Invoke();
        NextTurn();
    }

    public void NextTurn()
    {
        if (!InCombat) return;
        currentGroup.Opponent.ItIsYourTurn(currentGroup);
        currentGroup = currentGroup.Opponent;
    }

    public void CombatEnded()
    {
        combatEnding?.Invoke();
        GameManager.I.ChangeState(GameManager.State.Waiting);
        BackroundManager.I.TurnOnColoredParts(EncounterManager.I.LevelIndex);
        EncounterManager.I.LevelIndex++;
    }
}