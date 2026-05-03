using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportToScene : MonoBehaviour
{
    [SerializeField] private string sceneName;

    public void LoadTargetScene()
    {
        if (string.IsNullOrWhiteSpace(sceneName))
        {
            Debug.LogError("TeleportToScene: sceneName ist leer.");
            return;
        }

        SceneManager.LoadScene(sceneName);
    }
}
