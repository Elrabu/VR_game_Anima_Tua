using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DoorHandleAudio : MonoBehaviour
{
    public AudioSource doorAudio;

    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grab;
    private bool hasPlayed = false;

    void Awake()
    {
        grab = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
    }

    void OnEnable()
    {
        grab.selectEntered.AddListener(OnGrab);
        grab.selectExited.AddListener(OnRelease);
    }

    void OnDisable()
    {
        grab.selectEntered.RemoveListener(OnGrab);
        grab.selectExited.RemoveListener(OnRelease);
    }

    void OnGrab(SelectEnterEventArgs args)
    {
        if (!hasPlayed)
        {
            doorAudio.pitch = Random.Range(0.95f, 1.05f); // realism
            doorAudio.Play();
            hasPlayed = true;
        }
    }

    void OnRelease(SelectExitEventArgs args)
    {
        hasPlayed = false;
    }
}
