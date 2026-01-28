using UnityEngine;

public class QuitGameScript : MonoBehaviour
{
    public void QuitGameButton()
    {
        Application.Quit();
        Debug.Log("touched");
    }
}
