using System.Collections;
using System.Collections.Generic;
using Ky;
using UnityEngine;
using Logger = Ky.Logger;

public class EnemyManager : Singleton<EnemyManager>, ICombattantGroup
{
    public static Enemy currentEnemy;
    public float attackDelay;
    public Enemy enemyPrefab;
    public Skill debugEnemyAttack;
    public EnemySlot enemySlot;

    public ICombattant Main => currentEnemy;
    public List<ICombattant> Side => new();
    public ICombattantGroup Opponent { get; set; }

    protected override void Awake()
    {
        base.Awake();

        GameManager.gameInitialized += OnGameInitialized;
    }

    private void OnGameInitialized() { }

    public void SetNextEnemy(EnemyData newEnemyData)
    {
        var newEnemy = Instantiate(enemyPrefab, enemySlot.transform.position, Quaternion.identity, enemySlot.transform);
        newEnemy.SetDataAndReset(newEnemyData);
        currentEnemy = newEnemy;
    }

    public void ItIsYourTurn(ICombattantGroup opponent)
    {
        Opponent = opponent;
        Logger.Log($"<color=red>Enemy Turn</color>", Logger.DomainType.System);
        StartCoroutine(Delayed());

        IEnumerator Delayed()
        {
            yield return new WaitForSeconds(attackDelay);
            currentEnemy.Act(opponent, debugEnemyAttack);
            CombatManager.I.NextTurn();
        }
    }
}