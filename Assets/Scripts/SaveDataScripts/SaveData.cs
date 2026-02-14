using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.IO;

public class SaveData : MonoBehaviour
{
    public static SaveData Instance;
    public Settings settings = new Settings();
    private string requiredLevelName = "Dungeon01";
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    } 

    public void SaveToJson()
    {
        string settingsData = JsonUtility.ToJson(settings);
        string filePath = Application.persistentDataPath + "/SettingsData.json";
        Debug.Log(filePath);
        System.IO.File.WriteAllText(filePath, settingsData);
        //Debug.Log("data written!");
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
    }

    public void ResetSettings()
    {
        string filePath = Application.persistentDataPath + "/SettingsData.json";
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
            //Debug.Log("Settings file deleted (VRGameScene).");
        }

        settings = new Settings(); // re-instantiate defaults
        SaveToJson();
    }

}

[System.Serializable]
public class Settings
{
    public bool snapTurnEnabled = false;
    public bool continuousTurnEnabled = false;
    public bool tunnelingVignetteEnabled = false;
    public List<string> levels = new List<string>();
}