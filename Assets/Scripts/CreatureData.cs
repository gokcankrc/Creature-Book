using UnityEngine;

[CreateAssetMenu]
public class CreatureData : EntityData
{
    public CreatureData nextEvo;
    public EnergyBundle energyForNextEvo = new EnergyBundle();
    public Skill skill0Data;
    public Skill skill1Data;
    public Skill skill2Data;
}