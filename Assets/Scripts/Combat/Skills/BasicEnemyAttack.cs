using UnityEngine;

[CreateAssetMenu(menuName = "Skills/BasicEnemyAttack", fileName = "BasicEnemyAttack", order = 0)]
public class BasicEnemyAttack : Skill
{
    public float dmgToMain;
    public float dmgToSides;

    public override void Act(ICombattant attacker, ICombattantGroup opponents)
    {
        var damage = new Damage();
        damage.damage = attacker.Stats.damage.Calculate();
        damage.damage *= dmgToMain;
        opponents.Main.TakeDamage(attacker, damage);
        damage.damage = damage.damage * dmgToSides / dmgToMain;
        foreach (var sideOpponent in opponents.Side)
            sideOpponent.TakeDamage(attacker, damage);
    }
}