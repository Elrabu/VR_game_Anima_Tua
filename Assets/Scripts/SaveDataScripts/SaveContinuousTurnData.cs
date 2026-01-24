using UnityEngine;
using UnityEngine.UI;

public class SaveContinuousTurnData : MonoBehaviour
{
    private bool continuousTurn = false;
    [SerializeField] private Toggle toggle;
    [SerializeField] private Toggle snaptoggle;
    [SerializeField] private GameObject continuousturnProvider;
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
            continuousturnProvider.SetActive(false);
            SaveData.Instance.settings.continuousTurnEnabled = false;
            SaveData.Instance.SaveToJson();
            Debug.Log("disabledContinuousTurn");
        }
        else
        {
            continuousTurn = true;
            continuousturnProvider.SetActive(true);
            snaptoggle.isOn = false;
            SaveData.Instance.settings.continuousTurnEnabled = true;
            SaveData.Instance.SaveToJson();
            Debug.Log("enabledContinuousTurn");
        }
    }
}
