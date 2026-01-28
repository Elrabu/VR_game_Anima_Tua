using UnityEngine;

public class SetSavedValuesScript : MonoBehaviour
{
    [SerializeField] private GameObject snapVignette;
    [SerializeField] private GameObject continuousVignette;
    [SerializeField] private GameObject vignette;
    [SerializeField] private GameObject snapturnProvider;
    [SerializeField] private GameObject continuousturnProvider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SaveData.Instance.LoadFromJson();
        Debug.Log("Loaded verry nice :D");

        if (SaveData.Instance.settings.continuousTurnEnabled){
            snapturnProvider.SetActive(false);
            continuousturnProvider.SetActive(true);
        } else if (SaveData.Instance.settings.snapTurnEnabled)
        {
            snapturnProvider.SetActive(true);
            continuousturnProvider.SetActive(false);
        } else
        {
            snapturnProvider.SetActive(false);
            continuousturnProvider.SetActive(false);
        }

        if (SaveData.Instance.settings.tunnelingVignetteEnabled && SaveData.Instance.settings.continuousTurnEnabled)
        {
            continuousVignette.SetActive(true);
        } else if (SaveData.Instance.settings.tunnelingVignetteEnabled && SaveData.Instance.settings.snapTurnEnabled)
        {
            snapVignette.SetActive(true);
        } else if (SaveData.Instance.settings.tunnelingVignetteEnabled)
        {
            vignette.SetActive(true);
        }
    }
}
