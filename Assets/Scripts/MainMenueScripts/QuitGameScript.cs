using UnityEngine;

public class QuitGameScript : MonoBehaviour
{
    public void QuitGameButton()
    {
        SaveData.Instance.ResetSettings();
        Application.Quit();
       // Debug.Log("touched");
    }
}
