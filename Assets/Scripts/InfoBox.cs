using UnityEngine;
using UnityEngine.EventSystems;

public class InfoBox : MonoBehaviour, IPointerClickHandler
{
    private Entity entity;

    public void Init(Entity entity)
    {
        this.entity = entity;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        entity.OnInfo();
    }
}