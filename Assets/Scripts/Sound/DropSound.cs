using UnityEngine;

public class DropSound : MonoBehaviour
{
    [Header("Audio")]
    public AudioClip dropClip;

    [SerializeField] private AudioSource audioSource;

    void Awake()
    {
        
        audioSource = GetComponentInChildren<AudioSource>();

    }

    void OnCollisionEnter(Collision collision)
    {
        audioSource.PlayOneShot(dropClip);
    }
}
