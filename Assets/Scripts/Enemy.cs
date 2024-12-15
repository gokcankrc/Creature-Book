public class Enemy : Entity
{
    public EnergyBundle reward;
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
    public override void SetDataAndReset(EnemyData newEnemy)
    {
        reward = newEnemy.reward;
        Stats = newEnemy.stats.DeepCopy();
        Stats.SetHealthMax();
        gameObject.name = newEnemy.displayName;
        image.sprite = newEnemy.sprite;
        image.SetNativeSize();
        healthBar.Initialize(this);
    }
}