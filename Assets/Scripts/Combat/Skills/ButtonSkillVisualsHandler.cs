using TMPro;
using UnityEngine.UI;

public class ButtonSkillVisualsHandler : SkillVisualsHandler
{
    public Image image;
    public Image radialFill;
    public TextMeshProUGUI tmp;

    public override void Init(InitArgs args)
    {
        if (args.skill != null)
        {
            image.sprite = args.skill.sprite;
            image.enabled = true;
        }
        else
        {
            image.enabled = false;
            tmp.text = "";
        }
        Refresh(new Args(0, 0));
    }

    public override void Refresh(Args args)
    {
        radialFill.fillAmount = args.cooldownRatio;
        tmp.text = args.cooldownCurrent > 0 ? args.cooldownCurrent.ToString() : "";
    }
}