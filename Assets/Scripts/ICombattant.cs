public interface ICombattant
{
    public float Health { get; set; }
    public void TakeDamage(ICombattant attacker, Damage damage);
    public void Attack(ICombattant opponent);
}