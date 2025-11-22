using UnityEngine;
using UnityEngine.UI;

public class ButtonClickTestScript : MonoBehaviour
{
    [SerializeField] private Button myButton;

    private void Start()
    {
        myButton.onClick.AddListener(OnMyButtonClicked);
    }

    private void OnMyButtonClicked()
    {
        Debug.Log("Button was clicked!");
    }
}
