using UnityEngine;

[CreateAssetMenu]
public class CreatureData : EntityData
{
    public CreatureData nextEvo;
    public EnergyBundle energyForNextEvo = new EnergyBundle();
}