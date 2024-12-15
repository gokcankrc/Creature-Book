public class Enemy : Entity
{
    public override void TakeDamage(ICombattant attacker, Damage damage)
    {
        if (!CanTakeDamage()) return;
        base.TakeDamage(attacker, damage);
        if (Stats.isDead)
        {
            CombatManager.I.CombatEnded();
            Destroy(gameObject);
            // todo: give rewards
        }
    }
}