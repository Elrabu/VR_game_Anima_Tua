using UnityEngine;

public class WhispererAudioTrigger : MonoBehaviour
{
    private AudioSource audioSource;
    private bool hasPlayed = false;


    void Awake(){
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hasPlayed) return;

        if (other.CompareTag("Player"))
        {
            audioSource.Play();
            hasPlayed = true;
        }
    }
}
