using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TeleportButtonScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private Button myButton;
    //[SerializeField] private GrabListener grabListener;

    private void Start()
    {
        myButton.onClick.AddListener(OnMyButtonClicked);
    }

    private void OnMyButtonClicked()
    {
        Debug.Log("button clicked");
        SceneManager.LoadScene("VRGameScene");
    }
}
