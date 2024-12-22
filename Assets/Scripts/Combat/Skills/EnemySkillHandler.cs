using UnityEngine;

[RequireComponent(typeof(NullSkillVisualsHandler))]
public class EnemySkillHandler : SkillHandler
{
    public override SkillVisualsHandler VisualsHandlerMaker => GetComponent<NullSkillVisualsHandler>();
}