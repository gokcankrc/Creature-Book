using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class SkillHandler : MonoBehaviour, IPointerClickHandler
{
    public int cooldownMax;
    public int cooldownCurrent;

    [ReadOnly] public Entity entity;
    [ReadOnly] public Skill skill;

    public abstract SkillVisualsHandler VisualsHandlerMaker { get; }
    [NonSerialized] public SkillVisualsHandler visualsHandler;

    public bool Active => skill != null;

    private void Awake()
    {
        visualsHandler = VisualsHandlerMaker;
    }

    public void Initialize(Entity entity, Skill skill)
    {
        this.entity = entity;
        this.skill = skill;
        visualsHandler.Init(new SkillVisualsHandler.InitArgs(skill));

        if (skill == null) return;

        cooldownMax = skill.GetCooldownMax();
        cooldownCurrent = 0;
        Refresh();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!CanAct()) return;

        entity.Act(skill);
    }

    private bool CanAct()
    {
        return cooldownCurrent <= 0;
    }

    public void OnTurnPass()
    {
        if (!Active) return;
        cooldownCurrent -= 1;
        Refresh();
        if (cooldownCurrent <= 0) { }
    }

    private void Refresh()
    {
        if (!Active) return;
        var ratio = (float)cooldownCurrent / cooldownMax;
        visualsHandler.Refresh(new SkillVisualsHandler.Args(cooldownCurrent, ratio));
    }
}