using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScreenScript : MonoBehaviour
{
    public GameObject LoadingScreen;
    public Image LoadingBarFill;

    public GameObject rightHand;
    public GameObject leftHand;

    public void LoadScene(int sceneId)
    {
        StartCoroutine(LoadSceneAsync(sceneId));
    }

    IEnumerator LoadSceneAsync(int sceneId)
    {
        leftHand.SetActive(false);
        rightHand.SetActive(false);

        LoadingScreen.SetActive(true);

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId);
        operation.allowSceneActivation = false;

        float displayedProgress = 0f;

        while (operation.progress < 0.9f)
        {
            float targetProgress = Mathf.Clamp01(operation.progress / 0.9f);

            displayedProgress = Mathf.MoveTowards(
                displayedProgress,
                targetProgress,
                Time.deltaTime
            );

            LoadingBarFill.fillAmount = displayedProgress;
            yield return null;
        }
        LoadingBarFill.fillAmount = 1f;

        yield return new WaitForSeconds(0.3f);

        operation.allowSceneActivation = true;
        }
}
