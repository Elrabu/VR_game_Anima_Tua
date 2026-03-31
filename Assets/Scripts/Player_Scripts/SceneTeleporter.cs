using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneTeleporter : MonoBehaviour
{
    [SerializeField] private string playerTag = "Player";
    [SerializeField] private int sceneId;
    [SerializeField] private string loadingSceneName = "LoadingScreen";

    private void OnTriggerEnter(Collider other)
{
    if (other.CompareTag(playerTag))
    {
        StartCoroutine(LoadLoadingScene());
    }
}

IEnumerator LoadLoadingScene()
{
    LoadingManager.sceneToLoad = sceneId;

    // Bildschirm schwarz machen
    yield return StartCoroutine(ScreenFader.Instance.Fade(0, 1));

    // Szene wechseln (jetzt ist sowieso alles schwarz)
    SceneManager.LoadScene(loadingSceneName);
}
}