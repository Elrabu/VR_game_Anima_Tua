using UnityEngine;
using UnityEngine.UI;

public class SaveVignetteData : MonoBehaviour
{
    private bool tunnelingVignette = false;
    [SerializeField] private Toggle toggle;
    public void Start()
    {
        SaveData.Instance.LoadFromJson();
        if (SaveData.Instance.settings.tunnelingVignetteEnabled)
        {
            toggle.isOn = true;
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
}
