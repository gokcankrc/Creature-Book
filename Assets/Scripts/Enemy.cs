public class Enemy : Entity
{
    public EnergyBundle reward;
    private EnemyData enemyData;

    protected override EntityData EntityData => enemyData;

    public void SetDataAndReset(EnemyData newEnemyData)
    {
        enemyData = newEnemyData;
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
        reward = enemyData.reward;
    }
}