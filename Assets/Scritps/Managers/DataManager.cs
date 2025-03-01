using UnityEngine;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

public static class DataManager
{
    private static float mainBaseHP = 0;

    public static bool HasSaveFile()
    {
        string json = PlayerPrefs.GetString("saveCheck", "[]");
        if (json == "[]")
            return false;
        else
            return true;
    }

    public static void LoadGame()
    {
        List<Vector2Int> savedPath = LoadListFromPlayerPrefs<Vector2Int>("savedPath");
        GridManager.SetGridPath(savedPath);

        List<TurretData> savedTurrets = LoadListFromPlayerPrefs<TurretData>("savedTurrets");
        PlacementManager.instance.LoadAllTurrets(savedTurrets);

        int currentWave = LoadDataFromPlayerPrefs<int>("waveIndex");
        WaveManager.instance.SetWaveIndex(currentWave);

        // Set Currency


        // Set Base HP
        float savedHP = LoadDataFromPlayerPrefs<float>("health");
        mainBaseHP = savedHP;

        // Clear player prefs to avoid player restarting the game
        ClearPlayerPrefs();
    }

    public static void SaveGame()
    {
        ClearPlayerPrefs();
        SaveDataToPlayerPrefs("saveCheck", true);
        SaveDataToPlayerPrefs("savedPath", GridManager.mainPath);
        SaveDataToPlayerPrefs("savedTurrets", PlacementManager.instance.placedTurretList);
        SaveDataToPlayerPrefs("waveIndex", WaveManager.instance.currentWaveIndex);
        SaveDataToPlayerPrefs("health", MainBase.instance.GetHealth());
    }
    private static T LoadDataFromPlayerPrefs<T>(string _prefCategory)
    {
        string json = PlayerPrefs.GetString(_prefCategory, "[]");
        return JsonConvert.DeserializeObject<T>(json);
    }

    private static List<T> LoadListFromPlayerPrefs<T>(string _prefCategory)
    {
        string json = PlayerPrefs.GetString(_prefCategory, "[]");
        return JsonConvert.DeserializeObject<List<T>>(json).ToList();
    }
    public static void SaveDataToPlayerPrefs<T>(string _prefCategory, T _inData)
    {
        string json = JsonConvert.SerializeObject(_inData);
        PlayerPrefs.SetString(_prefCategory, json);
        PlayerPrefs.Save();
    }


    public static void ClearPlayerPrefs() => PlayerPrefs.DeleteAll();

    public static float GetMainBaseHp()
    {
        return mainBaseHP;
    }
}
