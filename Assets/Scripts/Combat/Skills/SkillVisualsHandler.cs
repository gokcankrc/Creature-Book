using UnityEngine;

public abstract class SkillVisualsHandler : MonoBehaviour, ISkillHandlerComponent
{
    public abstract void Init(InitArgs args);
    public abstract void Refresh(Args args);

    public class InitArgs
    {
        public Skill skill;

        public InitArgs(Skill skill)
        {
            this.skill = skill;
        }
    }

    public class Args
    {
        public int cooldownCurrent;
        public float cooldownRatio;

        public Args(int cooldownCurrent, float cooldownRatio)
        {
            this.cooldownCurrent = cooldownCurrent;
            this.cooldownRatio = cooldownRatio;
        }
    }
}