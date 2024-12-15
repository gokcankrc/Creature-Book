using Ky;
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
        var dmg = CalculateDamageTaken(Stats, damage);
        Stats.ChangeHealth(-dmg);
        Logger.Log($"Took {dmg} dmg, down to {Stats.healthCurrent}, Me: {gameObject.name}, Them: {attacker.GameObject.name}",
            Logger.DomainType.Combat);
        if (IsDead)
        {
            Logger.Log($"{gameObject.name} died", Logger.DomainType.Combat);
        }
    }

    protected static float CalculateDamageTaken(Stats stats, Damage damage)
    {
        var phy = damage.physicalDamage * 100 / (100 + stats.armor.Calculate());
        var mag = damage.magicDamage * 100 / (100 + stats.magicArmor.Calculate());
        return (phy + mag) * damage.mult;
    }

    public virtual void Act(ICombattantGroup opponents, ICombatAction action)
    {
        action.Act(this, opponents);
    }
}