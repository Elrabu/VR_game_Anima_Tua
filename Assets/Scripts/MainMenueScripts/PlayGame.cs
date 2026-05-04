using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayGame : MonoBehaviour
{
    [SerializeField] private string playerTag = "Player";
    [SerializeField] private string loadingSceneName = "LoadingScreen";
    [SerializeField] private int sceneId;

    [SerializeField] private ScreenFader fader;

    public void StartGame()
    {
        StartCoroutine(LoadLoadingScene());
    }

    IEnumerator LoadLoadingScene()
    {
        LoadingManager.sceneToLoad = sceneId;


        if (fader != null)
        {
            fader.FadeToBlack();

            yield return new WaitForSeconds(fader.fadeDuration);
        }

        SceneManager.LoadScene(loadingSceneName);

        yield break;
    }
}
