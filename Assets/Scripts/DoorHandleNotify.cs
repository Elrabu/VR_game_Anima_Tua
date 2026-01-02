using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DoorHandleNotify : MonoBehaviour
{
    public DoorAudioController doorAudio;
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grab;

    void Awake()
    {
        grab = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
    }

    void OnEnable()
    {
        grab.selectExited.AddListener(OnRelease);
    }

    void OnDisable()
    {
        grab.selectExited.RemoveListener(OnRelease);
    }

    void OnRelease(SelectExitEventArgs args)
    {
        doorAudio.ResetOpenSound();
    }
}
