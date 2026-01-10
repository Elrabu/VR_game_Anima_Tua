using UnityEngine;
using UnityEngine.SceneManagement;

public class SzeneRestartButton : MonoBehaviour
{
    [SerializeField] private string resetScene;
    public void restartScene()
    {
        SceneManager.LoadScene(resetScene);
    }
}
