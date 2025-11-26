using UnityEngine;
using UnityEngine.UI;

public class ButtonClickTestScript : MonoBehaviour
{
    [SerializeField] private Button myButton;
    [SerializeField] private GrabListener grabListener;

    private void Start()
    {
        myButton.onClick.AddListener(OnMyButtonClicked);
    }

    private void OnMyButtonClicked()
    {
        var item = grabListener.CurrentlyHeld;
        if (item != null)
        {
            Debug.Log("Item: " + item.transform.name);
        }
        //grabListener.DestroyHeldGameObject();
        //Debug.Log("Button was clicked!");
    }
}
