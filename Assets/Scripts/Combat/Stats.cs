using System;
using Ky.ModifierSystem;

[Serializable]
public class Stats
{
    public AdvancedNumber healthMax;
    public float healthCurrent;
    public AdvancedNumber damage;
    public bool isDead = false;
    
    public Action onHealthChanged;
    
    public float HealthRatio => healthCurrent / healthMax.Calculate();

    public void ChangeHealth(float change)
    {
        healthCurrent += change;
        onHealthChanged?.Invoke();
        isDead = healthCurrent <= 0;
    }

    public void SetHealthMax()
    {
        healthCurrent = healthMax.Calculate();
    }

    public Stats DeepCopy()
    {
        var copy = new Stats();
        copy.healthMax = healthMax;
        copy.healthCurrent = healthCurrent;
        copy.damage = damage;
        return copy;
    }
}