using System.Collections.Generic;
using Ky;

public class CombatManager : Singleton<CombatManager>
{
    // TODO: When then enemy dies, a new one comes.
    public static Enemy currentEnemy;
    public Creature TopCreature => slots[0].creature;
    public Creature MidCreature => slots[1].creature;
    public Creature BottomCreature => slots[2].creature;
    public List<CreatureSlot> slots;
}