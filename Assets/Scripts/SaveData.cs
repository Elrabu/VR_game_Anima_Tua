using UnityEngine;
using System.Collections.Generic;

public class SaveData : MonoBehaviour
{
    public static SaveData Instance;
    public Settings settings = new Settings();

    private void Start()
    {
        //SaveToJson();
        //LoadFromJson();
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); //Does not need to be attached to another Game Object in another scene
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

        if (!System.IO.File.Exists(filePath))
        {
            Debug.Log("No save file found!");
            return;
        }

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
} 
