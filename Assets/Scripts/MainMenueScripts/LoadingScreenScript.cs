using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class LoadingScreenScript : MonoBehaviour
{   
    [SerializeField] Slider progressBar;
    [SerializeField] TMPro.TextMeshProUGUI tipText;
    [SerializeField] Image loadingImage;
    [SerializeField] Sprite[] images;

    string[] tips =
    {
        "Tip: Keep distance to enemies.",
        "Tip: Try to hide your self from watchers by using the shield",
        "Tip: Listen to the noises in the dungeon.", 
    };

    void Start()
    {
        StartCoroutine(LoadSceneAsync());
        StartCoroutine(ShowTips());
        StartCoroutine(ChangeImages());
    }

    IEnumerator LoadSceneAsync()
    {   
        yield return new WaitForSeconds(4f); // nur zum Testen
        yield return new WaitForEndOfFrame();

        AsyncOperation operation = SceneManager.LoadSceneAsync(
            LoadingManager.sceneToLoad,
            LoadSceneMode.Additive
        );

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            
            progressBar.value = progress;
            

            yield return null;
        }

                
        Scene targetScene = SceneManager.GetSceneByBuildIndex(LoadingManager.sceneToLoad);
        SceneManager.SetActiveScene(targetScene);

        SceneManager.UnloadSceneAsync("LoadingScreen");

        //Player Freeze + Abgedunkelter bildschirm

        //Shader abspielen
    }

    IEnumerator ShowTips()
    {
        while (true)
        {
            int lastTip = -1;
            int newTip;
            do
            {
                newTip =Random.Range(0, tips.Length);
            } while(newTip == lastTip);
            lastTip = newTip;
            tipText.text = tips[newTip];
            yield return new WaitForSeconds(4f);
        }
    }

    IEnumerator ChangeImages()
    {
        int lastImage = -1;
        while (true)
        {
            int newImage;
            do
            {
                newImage = Random.Range(0, images.Length);

            }while(newImage == lastImage);
            lastImage = newImage;
            loadingImage.sprite = images[newImage];

            yield return new WaitForSeconds(4f);
        }
    }
}
