using UnityEngine;

[CreateAssetMenu]
public class EnemyData : EntityData
{
    public EnergyBundle reward;
    public Skill defaultSkill;

    public EnemyData(EnergyBundle reward, Skill defaultSkill)
    {
        this.reward = reward;
        this.defaultSkill = defaultSkill;
    }
}