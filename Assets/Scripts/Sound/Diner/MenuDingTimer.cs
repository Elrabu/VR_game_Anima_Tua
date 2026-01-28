using UnityEngine;

public class MenuDingTimer : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    private bool hasPlayed = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasPlayed)
        {
            audioSource.Play();
            hasPlayed = true;
        }
    }
}
