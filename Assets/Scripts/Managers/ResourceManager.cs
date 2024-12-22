using Ky;
using UnityEngine;
using TMPro;
public class ResourceManager : Singleton<ResourceManager>
{
    [SerializeField]TMP_Text blue,red,green,brown;
    public EnergyBundle resources = new EnergyBundle();
    public void Add (EnergyBundle added){
        resources += added;
        UpdateDisplay();
    }
    void Start (){
        UpdateDisplay();
    }
    public void Subtract (EnergyBundle subtracted){
        resources -= subtracted;
        UpdateDisplay();
    }
    public bool IsHigher (EnergyBundle comparison){
        return (resources >= comparison);
    }
    public void UpdateDisplay()
    {
        if (blue is null ||red is null||green is null||brown is null){
            Debug.LogWarning("A Display Text is missing from the ResourceManager");
            return;
        }

        blue.SetText(""+resources.blue);
        red.SetText(""+resources.red);
        green.SetText(""+resources.green);
        brown.SetText(""+resources.brown);
    }
}