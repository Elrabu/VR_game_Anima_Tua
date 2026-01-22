using UnityEngine;

public class SetSavedValuesScript : MonoBehaviour
{
    public GameObject snapVignette;
    public GameObject continuousVignette;
    public GameObject snapturnProvider;
    public GameObject continuousturnProvider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SaveData.Instance.LoadFromJson();

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

        if (SaveData.Instance.settings.tunnelingVignetteEnabled)
        {
            continuousVignette.SetActive(true);
        }
    }
}
