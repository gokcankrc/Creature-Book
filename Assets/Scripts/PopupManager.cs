using Ky;
using Ky.Popup;
using UnityEngine;

public class PopupManager : Singleton<PopupManager>
{
    public Popup popupPrefab;
    public Popup.Dep settings;

    public void PopupDmgNumber(float dmg, Vector3 pos)
    {
        var popup = Instantiate(popupPrefab, pos, Quaternion.identity, transform);
        settings.text = dmg.ToString("0");
        popup.Initialize(pos, settings);
    }
}