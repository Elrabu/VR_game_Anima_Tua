using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneTeleporter : MonoBehaviour
{
    [SerializeField] private string playerTag = "Player";
    [SerializeField] private string loadingSceneName = "LoadingScreen";
    [SerializeField] private int sceneId;
    
    // Drag your Canvas (with the ScreenFader script) into this slot in the Inspector
    [SerializeField] private ScreenFader fader;

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
        

        if (fader != null) 
        {
            fader.FadeToBlack();
            
            yield return new WaitForSeconds(fader.fadeDuration);
        }

        SceneManager.LoadScene(loadingSceneName);

        yield break; 
    }
}