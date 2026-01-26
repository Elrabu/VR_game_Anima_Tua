using UnityEngine;
using UnityEngine.SceneManagement;

public class TestSceneChanger : MonoBehaviour
{
    void Update()
    {
        if (Input.anyKeyDown)
        {
            Debug.Log("[TestSceneChanger] irgendeine Taste gedrückt");
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log("[TestSceneChanger] Loading TestSceneB");
            SceneManager.LoadScene("TestSceneB");
        }
    } 
}