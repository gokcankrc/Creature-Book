using Sirenix.OdinInspector;
using UnityEngine;

[RequireComponent(typeof(EnemySkillHandler))]
public class Enemy : Entity
{
    public EnergyBundle reward;
    [ShowInInspector, ReadOnly] private EnemyData enemyData;
    [ShowInInspector, ReadOnly] private EnemySkillHandler skillHandler;
    [ShowInInspector, ReadOnly] private Skill skill;

    protected override EntityData EntityData => enemyData;

    public void SetDataAndReset(EnemyData newEnemyData)
    {
        enemyData = newEnemyData;
        skill = newEnemyData.defaultSkill;
        skillHandler = GetComponent<EnemySkillHandler>();
        skillHandler.Initialize(this, skill);
        Reset();
    }

    public override void TakeDamage(ICombattant attacker, Damage damage)
    {
        if (!CanTakeDamage()) return;
        base.TakeDamage(attacker, damage);
        if (Stats.isDead)
        {
            CombatManager.I.CombatEnded();
            ResourceManager.I.Add(this.reward);
            Destroy(gameObject);
        }
    }

    public override void Reset()
    {
        base.Reset();
        group = EnemyManager.I;
        reward = enemyData.reward;
    }

    public override void ReadyToAct()
    {
        base.ReadyToAct();
        Act(skill);
    }
}