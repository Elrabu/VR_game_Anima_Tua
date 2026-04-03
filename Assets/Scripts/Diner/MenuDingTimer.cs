using UnityEngine;

public class MenuDingTimer : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private GameObject firstTask;
    [SerializeField] private GameObject firstTaskText;

    private bool hasPlayed = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasPlayed)
        {
            audioSource.Play();
            hasPlayed = true;
            firstTask.SetActive(true);
            firstTaskText.SetActive(true);
        }
    }
}
