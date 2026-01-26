using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IDTeleportButtonScript : MonoBehaviour
{
    public void changeScene(int sceneId)
    {
        SceneManager.LoadScene(sceneId);
    }
}
