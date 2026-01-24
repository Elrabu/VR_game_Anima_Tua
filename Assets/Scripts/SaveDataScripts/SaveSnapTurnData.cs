using UnityEngine;
using UnityEngine.UI;

public class SaveSnapTurnData : MonoBehaviour
{
    private bool snapTurn = false;
    [SerializeField] private Toggle toggle;
    [SerializeField] private Toggle Continuoustoggle;
    [SerializeField] private GameObject snapturnProvider;
    public void Start()
    {
        SaveData.Instance.LoadFromJson();
        if (SaveData.Instance.settings.snapTurnEnabled)
        {
            toggle.isOn = true;
        }
    }
     public void ChangeSnapTurn()
    {
        if (snapTurn)
        {
            snapTurn = false;
            snapturnProvider.SetActive(false);
            SaveData.Instance.settings.snapTurnEnabled = false;
            SaveData.Instance.SaveToJson();
            Debug.Log("disabledSnapTurn");
        } else
        {
            snapTurn = true;
            snapturnProvider.SetActive(true);
            Continuoustoggle.isOn = false;
            SaveData.Instance.settings.snapTurnEnabled = true;
            SaveData.Instance.SaveToJson();
            Debug.Log("enabledSnapTurn");
        }
    }
}
