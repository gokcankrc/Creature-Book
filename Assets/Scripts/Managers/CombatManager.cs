using Ky;

public class CombatManager : Singleton<CombatManager>
{
    public ICombattantGroup currentGroup;

    // TODO: When then enemy dies, a new one comes.
    public Creature TopCreature => PlayerManager.I.TopCreature;
    public Creature MidCreature => PlayerManager.I.MidCreature;
    public Creature BottomCreature => PlayerManager.I.BottomCreature;
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
        EncounterManager.I.LevelIndex++;
        
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
        GameManager.I.ChangeState(GameManager.State.Waiting);
        BackroundManager.I.TurnOnColoredParts(EncounterManager.I.LevelIndex);
    }
}