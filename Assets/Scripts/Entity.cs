using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using Logger = Ky.Logger;

[RequireComponent(typeof(EntityMovement))]
public abstract class Entity : MonoBehaviour, ICombattant
{
    public ICombattantGroup group;
    [PropertyOrder(0)] [ShowInInspector] public Stats Stats { get; protected set; }
    [Header("References")]
    [PropertyOrder(10)] public Image image;
    [PropertyOrder(10)] public HealthBar healthBar;

    public EntityMovement entityMovement;

    public GameObject GameObject => gameObject;
    public virtual MonoBehaviour Behaviour => this;
    public bool IsDead => Stats.isDead;
    protected abstract EntityData EntityData { get; }

    protected virtual void Awake()
    {
        entityMovement = GetComponent<EntityMovement>();
    }

    public void Engage()
    {
        entityMovement.Activate();
    }

    public void Disengage()
    {
        entityMovement.Deactivate();
    }

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
        PopupManager.I.PopupDmgNumber(dmg, transform.position);
        Logger.Log($"Took {dmg} dmg, down to {Stats.healthCurrent}, Me: {gameObject.name}, Them: {attacker.GameObject.name}",
            Logger.DomainType.Combat);
        if (IsDead)
        {
            Died();
            Logger.Log($"{gameObject.name} died", Logger.DomainType.Combat);
        }
    }

    private void Died()
    {
        image.color = Color.gray;
        entityMovement.Halt();
    }

    protected static float CalculateDamageTaken(Stats stats, Damage damage)
    {
        var phy = damage.physicalDamage * 100 / (100 + stats.armor.Calculate());
        var mag = damage.magicDamage * 100 / (100 + stats.magicArmor.Calculate());
        return (phy + mag) * damage.mult;
    }

    public virtual void ReadyToAct()
    {
        entityMovement.tired = false;
    }

    public virtual void Act(ICombatAction action)
    {
        action.Act(this, group);
        entityMovement.OnAttack();
    }
}