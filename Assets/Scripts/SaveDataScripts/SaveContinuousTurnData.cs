using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Turning;

public class SaveContinuousTurnData : MonoBehaviour
{
    private bool continuousTurn;
    [SerializeField] private Toggle toggle;
    [SerializeField] private Toggle snapToggle;
    [SerializeField] private ContinuousTurnProvider continuousTurnProvider;
    [SerializeField] private SnapTurnProvider snapTurnProvider;

    public void Start()
    {
        SaveData.Instance.LoadFromJson();

        continuousTurn = SaveData.Instance.settings.continuousTurnEnabled;

        if (toggle != null)
        {
            toggle.isOn = continuousTurn;
        }

        SetTurnProviders(SaveData.Instance.settings.snapTurnEnabled, continuousTurn);
    }

    public void ChangeContinuousTurn()
    {
        continuousTurn = !continuousTurn;

        SaveData.Instance.settings.continuousTurnEnabled = continuousTurn;
        SaveData.Instance.settings.snapTurnEnabled = false;
        SaveData.Instance.SaveToJson();

        if (snapToggle != null)
        {
            snapToggle.isOn = false;
        }

        SetTurnProviders(false, continuousTurn);

        Debug.Log(continuousTurn ? "enabledContinuousTurn" : "disabledContinuousTurn");
    }

    private void SetTurnProviders(bool snapEnabled, bool continuousEnabled)
    {
        if (snapTurnProvider != null)
        {
            snapTurnProvider.enabled = snapEnabled;
        }

        if (continuousTurnProvider != null)
        {
            continuousTurnProvider.enabled = continuousEnabled;
        }
    }
}
