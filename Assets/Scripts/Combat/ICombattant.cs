using UnityEngine;

public interface ICombattant
{
    public GameObject GameObject { get; }
    public MonoBehaviour Behaviour { get; }

    public void TakeDamage(ICombattant attacker, Damage damage);
    public void Act(ICombattantGroup opponent, ICombatAction action);

    public Stats Stats { get; }
    public bool IsDead => Stats.isDead;
}