using UnityEngine;
using System.Collections.Generic;

public class SaveData : MonoBehaviour
{
    public Settings settings = new Settings();

    private void Start()
    {
        //SaveToJson();
        LoadFromJson();
    }

    public void SaveToJson()
    {
        string settingsData = JsonUtility.ToJson(settings);
        string filePath = Application.persistentDataPath + "/SettingsData.json";
        Debug.Log(filePath);
        System.IO.File.WriteAllText(filePath, settingsData);
        Debug.Log("data written!");
    }

    public void LoadFromJson()
    {
        string filePath = Application.persistentDataPath + "/SettingsData.json";
        string settingsData = System.IO.File.ReadAllText(filePath);

        settings = JsonUtility.FromJson<Settings>(settingsData);
        Debug.Log("Data loaded");
        Debug.Log(settings.snapTurnEnabled +""+ settings.continuousTurnEnabled +""+ settings.tunnelingVignetteEnabled +""+ settings.levels[0]);
    }
}

[System.Serializable]
public class Settings
{
    public bool snapTurnEnabled = false;
    public bool continuousTurnEnabled = false;
    public bool tunnelingVignetteEnabled = false;
    public List<Levels> levels = new List<Levels>();
}

[System.Serializable]
public class Levels
{
    public string name;
    public bool completed;
} 
