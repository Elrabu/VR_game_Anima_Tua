using UnityEngine;
using UnityEngine.UI;

public class ButtonClickTestScript : MonoBehaviour
{
    [SerializeField] private Button myButton;
    //[SerializeField] private GrabListener grabListener;

    private void Start()
    {
        myButton.onClick.AddListener(OnMyButtonClicked);
    }

    private void OnMyButtonClicked()
    {
        Debug.Log("button clicked");
    }
}
