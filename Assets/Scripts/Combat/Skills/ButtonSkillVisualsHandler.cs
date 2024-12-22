using TMPro;
using UnityEngine.UI;

public class ButtonSkillVisualsHandler : SkillVisualsHandler
{
    public Image image;
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
    }

    public override void Refresh(Args args)
    {
        image.fillAmount = args.cooldownRatio;
        tmp.text = args.cooldownCurrent > 0 ? args.cooldownCurrent.ToString() : "";
    }
}