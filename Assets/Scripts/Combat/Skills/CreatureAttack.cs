using UnityEngine;

[CreateAssetMenu(menuName = "Skills/CreatureAttack", fileName = "CreatureAttack", order = 0)]
public class CreatureAttack : Skill
{
    public override void Act(ICombattant attacker, ICombattantGroup opponents)
    {
        var damage = new Damage();
        damage.damage = attacker.Stats.damage.Calculate();
        opponents.Main.TakeDamage(attacker, damage);
    }
}