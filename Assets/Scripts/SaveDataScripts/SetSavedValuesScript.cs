using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Turning;

public class SetSavedValuesScript : MonoBehaviour
{
    [SerializeField] private GameObject snapVignette;
    [SerializeField] private GameObject continuousVignette;
    [SerializeField] private GameObject vignette;
    [SerializeField] private SnapTurnProvider snapTurnProvider;
    [SerializeField] private ContinuousTurnProvider continuousTurnProvider;

    void Start()
    {
        SaveData.Instance.LoadFromJson();

        ApplyTurnSettings();
        ApplyVignetteSettings();
    }

    private void ApplyTurnSettings()
    {
        if (SaveData.Instance.settings.continuousTurnEnabled)
        {
            SetTurnProviders(false, true);
            return;
        }

        if (SaveData.Instance.settings.snapTurnEnabled)
        {
            SetTurnProviders(true, false);
            return;
        }

        SetTurnProviders(false, false);
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

    private void ApplyVignetteSettings()
    {
        if (snapVignette != null)
        {
            snapVignette.SetActive(false);
        }

        if (continuousVignette != null)
        {
            continuousVignette.SetActive(false);
        }

        if (vignette != null)
        {
            vignette.SetActive(false);
        }

        if (!SaveData.Instance.settings.tunnelingVignetteEnabled)
        {
            return;
        }

        if (vignette != null)
        {
            vignette.SetActive(true);
        }

        if (SaveData.Instance.settings.continuousTurnEnabled)
        {
            if (continuousVignette != null)
            {
                continuousVignette.SetActive(true);
            }

            return;
        }

        if (SaveData.Instance.settings.snapTurnEnabled)
        {
            if (snapVignette != null)
            {
                snapVignette.SetActive(true);
            }

            return;
        }
    }
}
