using Ky;
using UnityEngine;
using TMPro;
public class ResourceManager : Singleton<ResourceManager>
{
    [SerializeField]TMP_Text blue,red,green,brown;
    public EnergyBundle resources = new EnergyBundle();
    public void Add (EnergyBundle added){
        this.resources += added;
        this.UpdateDisplay();
    }
    public void Subtract (EnergyBundle subtracted){
        this.resources -= subtracted;
        this.UpdateDisplay();
    }
    public bool IsHigher (EnergyBundle comparison){
        return (this.resources >= comparison);
    }
    public void UpdateDisplay(){
        if (this.blue is null ||this.red is null||this.green is null||this.brown is null){
            Debug.LogWarning("A Display Text is missing from the ResourceManager");
        }
        else {
            this.blue.SetText(""+this.resources.blue);
            this.red.SetText(""+this.resources.red);
            this.green.SetText(""+this.resources.green);
            this.brown.SetText(""+this.resources.brown);
        }
        
    }

}