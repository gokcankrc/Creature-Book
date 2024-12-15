using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image healthImage;
    private Entity entity;

    public void Initialize(Entity entity)
    {
        this.entity = entity;
        entity.Stats.onHealthChanged += Refresh;
    }

    private void Refresh()
    {
        healthImage.fillAmount = entity.Stats.HealthRatio;
    }
}