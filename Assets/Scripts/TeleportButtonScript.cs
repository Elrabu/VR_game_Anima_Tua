using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TeleportButtonScript : MonoBehaviour
{
    [SerializeField] private Button myButton;

    private void Start()
    {
        myButton.onClick.AddListener(OnMyButtonClicked);
    }

    private void OnMyButtonClicked()
    {
        SceneManager.LoadScene("VRGameScene");
    }
}
