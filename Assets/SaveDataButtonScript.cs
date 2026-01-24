using UnityEngine;

public class SaveDataButtonScript : MonoBehaviour
{
    private bool snapTurn = false;
    private bool continuousTurn = false;
    private bool tunnelingVignette = false;
    public void saveData()
    {
        SaveData.Instance.settings.snapTurnEnabled = false;
        SaveData.Instance.settings.continuousTurnEnabled = false;
        SaveData.Instance.settings.tunnelingVignetteEnabled = false;
        Levels newLevel = new Levels
        {
            name = "Level_3"
        };

        SaveData.Instance.settings.levels.Add(newLevel);
        SaveData.Instance.SaveToJson();
        Debug.Log("Saved");
    }

    public void ChangeSnapTurn()
    {
        if (snapTurn)
        {
            snapTurn = false;
            SaveData.Instance.settings.snapTurnEnabled = false;
            SaveData.Instance.SaveToJson();
            Debug.Log("disabledSnapTurn");
        } else
        {
            snapTurn = true;
            SaveData.Instance.settings.snapTurnEnabled = true;
            SaveData.Instance.SaveToJson();
            Debug.Log("enabledSnapTurn");
        }
    }

    public void ChangeContinuousTurn()
    {
        if (continuousTurn)
        {
            continuousTurn = false;
            SaveData.Instance.settings.continuousTurnEnabled = false;
            SaveData.Instance.SaveToJson();
            Debug.Log("disabledContinuousTurn");
        }
        else
        {
            continuousTurn = true;
            SaveData.Instance.settings.continuousTurnEnabled = true;
            SaveData.Instance.SaveToJson();
            Debug.Log("enabledContinuousTurn");
        }
    }

    public void ChangeTunnelingVignette()
    {
        if (tunnelingVignette)
        {
            tunnelingVignette = false;
            SaveData.Instance.settings.tunnelingVignetteEnabled = false;
            SaveData.Instance.SaveToJson();
            Debug.Log("disabledTunnelingVignette");
        }
        else
        {
            tunnelingVignette = true;
            SaveData.Instance.settings.tunnelingVignetteEnabled = true;
            SaveData.Instance.SaveToJson();
            Debug.Log("enabledTunnelingVignette");
        }
    }


    public void loadData()
    {
        SaveData.Instance.LoadFromJson();

        Debug.Log($"Snap Turn: {SaveData.Instance.settings.snapTurnEnabled}");
        Debug.Log($"Continuous Turn: {SaveData.Instance.settings.continuousTurnEnabled}");
        Debug.Log($"Vignette: {SaveData.Instance.settings.tunnelingVignetteEnabled}");
    }
}
