using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using Logger = Ky.Logger;

public abstract class Entity : MonoBehaviour, ICombattant
{
    public Image image;
    public HealthBar healthBar;
    [ShowInInspector] public Stats Stats { get; protected set; }

    public GameObject GameObject => gameObject;
    public virtual MonoBehaviour Behaviour => this;
    public bool IsDead => Stats.isDead;
    protected abstract EntityData EntityData { get; }

    public virtual void Reset()
    {
        Stats = EntityData.stats.DeepCopy();
        Stats.SetHealthMax();
        gameObject.name = EntityData.displayName;
        image.sprite = EntityData.sprite;
        image.SetNativeSize();
        healthBar.Initialize(this);
    }

    public virtual bool CanTakeDamage()
    {
        if (IsDead) return false;
        return true;
    }

    public virtual void TakeDamage(ICombattant attacker, Damage damage)
    {
        if (!CanTakeDamage()) return;
        Stats.ChangeHealth(-damage.damage);
        Logger.Log($"Took {damage.damage} dmg, down to {Stats.healthCurrent}, Me: {gameObject.name}, Them: {attacker.GameObject.name}",
            Logger.DomainType.Combat);
        if (IsDead)
        {
            Logger.Log($"{gameObject.name} died", Logger.DomainType.Combat);
        }
    }

    public virtual void Act(ICombattantGroup opponents, ICombatAction action)
    {
        action.Act(this, opponents);
    }
}