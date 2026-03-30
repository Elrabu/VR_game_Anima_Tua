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

        AsyncOperation op = SceneManager.LoadSceneAsync(loadingSceneName);

        while (!op.isDone)
        {
            yield return null;
        }
    }
}