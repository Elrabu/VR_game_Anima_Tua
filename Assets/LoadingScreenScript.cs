using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScreenScript : MonoBehaviour
{
    public GameObject LoadingScreen;

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
        
        yield return new WaitForSeconds(0.3f);

        operation.allowSceneActivation = true;
        }
}
