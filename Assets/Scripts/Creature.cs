using UnityEngine;

public class Creature : MonoBehaviour, ICombattable
{
    // TODO: Skills
    // TODO: Selecting
    // TODO: Popups
    public float Health { get; set; }

    public void TakeDamage(ICombattable attacker, Damage damage)
    {
        throw new System.NotImplementedException();
    }

    public void Attack(ICombattable opponent)
    {
        throw new System.NotImplementedException();
    }
}

public class GameManager : MonoBehaviour { }

public class CombatManager : MonoBehaviour
{
    // TODO: When then enemy dies, a new one comes.
    public static Enemy currentEnemy;
    public static Creature currentCreature;
}

public class Enemy : MonoBehaviour, ICombattable
{
    public float Health { get; set; }

    public void TakeDamage(ICombattable attacker, Damage damage)
    {
        throw new System.NotImplementedException();
    }

    public void Attack(ICombattable opponent)
    {
        throw new System.NotImplementedException();
    }
}

public class EnemyManager : MonoBehaviour { }

public interface ICombattable
{
    public float Health { get; set; }
    public void TakeDamage(ICombattable attacker, Damage damage);
    public void Attack(ICombattable opponent);
}

public class Damage
{
    //this might want to handle weird skills
    private float damage;
}