using UnityEngine;

[CreateAssetMenu(menuName = "Skills/BasicAttack", fileName = "BasicAttack", order = 0)]
public class AttackSkill : Skill
{
    [SerializeField] private float physicalMult;
    [SerializeField] private float magicMult;
    [SerializeField] private float mainMult;
    [SerializeField] private float sideMult;

    public override void Act(ICombattant attacker, ICombattantGroup opponents)
    {
        var damage = new Damage();
        damage.physicalDamage = attacker.Stats.physicalDamage.Calculate() * physicalMult;
        damage.magicDamage = attacker.Stats.magicDamage.Calculate() * magicMult;
        damage.mult = mainMult;
        opponents.Main.TakeDamage(attacker, damage);
        damage.mult = sideMult;
        foreach (var opp in opponents.Side)
        {
            opp.TakeDamage(attacker, damage);
        }
    }
}