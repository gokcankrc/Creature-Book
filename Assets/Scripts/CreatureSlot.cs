using Sirenix.OdinInspector;

public class CreatureSlot : EntitySlot
{
    [ReadOnly] public Creature creature;

    public void Initialize(Creature newCreature)
    {
        creature = newCreature;
        Reset();
    }

    private void Reset()
    {
        creature.transform.position = transform.position;
        creature.transform.SetParent(transform);
    }

    public static void Swap(CreatureSlot a, CreatureSlot b)
    {
        if (GameManager.gameState == GameManager.State.InCombat) return;

        (a.creature, b.creature) = (b.creature, a.creature);
        a.Reset();
        b.Reset();
    }
}