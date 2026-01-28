using UnityEngine;
using UnityEngine.UI;

public class SaveDataButtonScript : MonoBehaviour
{
    public void Start()
    {
      //  SaveData.Instance.LoadFromJson();
    }
    public void saveData()
    {
        SaveData.Instance.settings.snapTurnEnabled = false;
        SaveData.Instance.settings.continuousTurnEnabled = false;
        SaveData.Instance.settings.tunnelingVignetteEnabled = false;
        /*Levels newLevel = new Levels
        {
            name = "Level_3"
        }; */

       // SaveData.Instance.settings.levels.Add(newLevel);
        SaveData.Instance.SaveToJson();
        Debug.Log("Saved");
    }
}
