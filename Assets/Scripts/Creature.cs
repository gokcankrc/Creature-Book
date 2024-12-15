using System;
using Ky;
using Sirenix.OdinInspector;
using UnityEngine;

public class Creature : MonoBehaviour, ICombattant
{
    // TODO: Skills
    // TODO: Selecting
    // TODO: Popups
    public float Health { get; set; }
    public CreatureData creatureData;

    public void TakeDamage(ICombattant attacker, Damage damage)
    {
        throw new System.NotImplementedException();
    }

    public void Attack(ICombattant opponent)
    {
        throw new System.NotImplementedException();
    }

    [Button]
    public void DebugEvolve()
    {
        
    }
}

public class CreatureData : EntityData
{
    public CreatureData nextEvo;
    public EnergyBundle energyForNextEvo;
}

[Serializable]
public class EnergyBundle
{
    public int blue;
    public int red;
    public int green;
}

public class EnemyData : EntityData { }

public abstract class EntityData : ScriptableObject
{
    public float health;
    public float damage;
    public Sprite sprite;
}

public class GameManager : Singleton<GameManager>
{
    private Action gameInitialized;

    private void Start()
    {
        gameInitialized?.Invoke();
    }
}

public class CombatManager : MonoBehaviour
{
    // TODO: When then enemy dies, a new one comes.
    public static Enemy currentEnemy;
    public static Creature currentCreature;
}

public class Enemy : MonoBehaviour, ICombattant
{
    public float Health { get; set; }

    public void TakeDamage(ICombattant attacker, Damage damage)
    {
        throw new System.NotImplementedException();
    }

    public void Attack(ICombattant opponent)
    {
        throw new System.NotImplementedException();
    }
}

public class EnemyManager : MonoBehaviour { }

public interface ICombattant
{
    public float Health { get; set; }
    public void TakeDamage(ICombattant attacker, Damage damage);
    public void Attack(ICombattant opponent);
}

public class Damage
{
    //this might want to handle weird skills
    private float damage;
}