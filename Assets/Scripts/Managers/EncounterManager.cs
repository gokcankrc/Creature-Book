using System.Collections.Generic;
using Ky;
using UnityEngine;

public class EncounterManager : Singleton<EncounterManager>
{
    private const string LEVEL_INDEX_KEY = "Level Index";
    public int LevelIndex { get => PlayerPrefs.GetInt(LEVEL_INDEX_KEY, 0); set => PlayerPrefs.SetInt(LEVEL_INDEX_KEY, value); }

    public List<EnemyData> encounters;

    public EnemyData GetNextEnemy()
    {
        return encounters[LevelIndex];
    }
}