using UnityEngine;

public class ShowAndHideTut : MonoBehaviour
{
    private void Start()
    {
        // Optionally start with children deactivated
        SetChildrenActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player entered the trigger
        if (other.CompareTag("Player"))
        {
            SetChildrenActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the player left the trigger
        if (other.CompareTag("Player"))
        {
            SetChildrenActive(false);
        }
    }

    private void SetChildrenActive(bool active)
    {
        // Loop through all children and set them active/inactive
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(active);
        }
    }
}
