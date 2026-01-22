using UnityEngine;

public class SaveDataButtonScript : MonoBehaviour
{
    public void saveData()
    {
        SaveData.Instance.settings.snapTurnEnabled = false;
        SaveData.Instance.settings.continuousTurnEnabled = false;
        SaveData.Instance.settings.tunnelingVignetteEnabled = false;
        Levels newLevel = new Levels
        {
            name = "Level_2",
            completed = false
        };

        SaveData.Instance.settings.levels.Add(newLevel);
        SaveData.Instance.SaveToJson();
        Debug.Log("Saved");
    }

    public void loadData()
    {
        SaveData.Instance.LoadFromJson();

        Debug.Log($"Snap Turn: {SaveData.Instance.settings.snapTurnEnabled}");
        Debug.Log($"Continuous Turn: {SaveData.Instance.settings.continuousTurnEnabled}");
        Debug.Log($"Vignette: {SaveData.Instance.settings.tunnelingVignetteEnabled}");
    }
}
