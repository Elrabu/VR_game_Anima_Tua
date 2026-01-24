using UnityEngine;
using UnityEngine.UI;

public class SaveContinuousTurnData : MonoBehaviour
{
    private bool continuousTurn = false;
    [SerializeField] private Toggle toggle;
    public void Start()
    {
        SaveData.Instance.LoadFromJson();
        if (SaveData.Instance.settings.continuousTurnEnabled)
        {
            toggle.isOn = true;
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
}
