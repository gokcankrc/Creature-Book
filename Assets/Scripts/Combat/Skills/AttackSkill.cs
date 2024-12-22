using UnityEngine;

[CreateAssetMenu(menuName = "Skills/BasicAttack", fileName = "BasicAttack", order = 0)]
public class AttackSkill : Skill
{
    [SerializeField] private float physicalMult;
    [SerializeField] private float magicMult;
    [SerializeField] private float mainMult;
    [SerializeField] private float sideMult;
    [SerializeField] private int cooldown = 0;

    public override void Act(ICombattant attacker, ICombattantGroup actingGroup)
    {
        var damage = new Damage();
        damage.physicalDamage = attacker.Stats.physicalDamage.Calculate() * physicalMult;
        damage.magicDamage = attacker.Stats.magicDamage.Calculate() * magicMult;
        damage.mult = mainMult;
        actingGroup.Opponent.Main.TakeDamage(attacker, damage);
        damage.mult = sideMult;
        foreach (var opp in actingGroup.Opponent.Side)
        {
            opp.TakeDamage(attacker, damage);
        }
    }

    public override int GetCooldownMax()
    {
        return cooldown;
    }
}