using UnityEngine;

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