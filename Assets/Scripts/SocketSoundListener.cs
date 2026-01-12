using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SocketSoundListener : MonoBehaviour
{
    [SerializeField] private UnityEngine.XR.Interaction.Toolkit.Interactors.XRSocketInteractor socket;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip insertClip;
    [SerializeField] private AudioClip removeClip;

    void OnEnable()
    {
        socket.selectEntered.AddListener(OnInserted);
        socket.selectExited.AddListener(OnRemoved);
    }

    void OnDisable()
    {
        socket.selectEntered.RemoveListener(OnInserted);
        socket.selectExited.RemoveListener(OnRemoved);
    }

    void OnInserted(SelectEnterEventArgs args)
    {   
        if (!args.interactableObject.transform.CompareTag("Book"))
            return;
        if (audioSource && insertClip)
            audioSource.PlayOneShot(insertClip);
    }

    void OnRemoved(SelectExitEventArgs args)
    {
        if (audioSource && removeClip)
            audioSource.PlayOneShot(removeClip);
    }
}
