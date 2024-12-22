using UnityEngine;

[RequireComponent(typeof(ButtonSkillVisualsHandler))]
public class CreatureSkillHandler : SkillHandler
{
    public override SkillVisualsHandler VisualsHandlerMaker => GetComponent<ButtonSkillVisualsHandler>();
}