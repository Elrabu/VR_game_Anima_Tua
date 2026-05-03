using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Turning;

public class SaveSnapTurnData : MonoBehaviour
{
    private bool snapTurn;
    [SerializeField] private Toggle toggle;
    [SerializeField] private Toggle continuousToggle;
    [SerializeField] private SnapTurnProvider snapTurnProvider;
    [SerializeField] private ContinuousTurnProvider continuousTurnProvider;

    public void Start()
    {
        SaveData.Instance.LoadFromJson();

        snapTurn = SaveData.Instance.settings.snapTurnEnabled;

        if (toggle != null)
        {
            toggle.isOn = snapTurn;
        }

        SetTurnProviders(snapTurn, SaveData.Instance.settings.continuousTurnEnabled);
    }

    public void ChangeSnapTurn()
    {
        snapTurn = !snapTurn;

        SaveData.Instance.settings.snapTurnEnabled = snapTurn;
        SaveData.Instance.settings.continuousTurnEnabled = false;
        SaveData.Instance.SaveToJson();

        if (continuousToggle != null)
        {
            continuousToggle.isOn = false;
        }

        SetTurnProviders(snapTurn, false);

        Debug.Log(snapTurn ? "enabledSnapTurn" : "disabledSnapTurn");
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
