using System.Collections.Generic;
using Ky;
using UnityEngine;
using TMPro;
public class EncounterManager : Singleton<EncounterManager>
{

    private const string LEVEL_INDEX_KEY = "Level Index";
    public int LevelIndex { get => PlayerPrefs.GetInt(LEVEL_INDEX_KEY, 0); set => PlayerPrefs.SetInt(LEVEL_INDEX_KEY, value); }

    public List<EnemyData> encounters;
    [SerializeField]TMP_Text encounterDisplay;
    public EnemyData GetNextEnemy()
    {
        this.UpdateDisplay();
        return encounters[LevelIndex];
    }
    public void UpdateDisplay(){
        if (this.encounterDisplay != null){
            this.encounterDisplay.SetText(this.LevelIndex+" / "+this.encounters.Count);
        }
        
    }
}