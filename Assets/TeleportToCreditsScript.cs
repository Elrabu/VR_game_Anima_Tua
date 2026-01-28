using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportToCreditsScript : MonoBehaviour
{
    public void TeleportToCredits()
    {
        SceneManager.LoadScene("Credits");
    }
}
