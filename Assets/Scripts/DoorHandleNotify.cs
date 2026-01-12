using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable))]
public class DoorHandleNotify : MonoBehaviour
{
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grab;
    public DoorAudioController doorAudio;

    void Awake()
    {
        grab = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();

        // automatisch im Parent suchen (Prefab-freundlich)
        doorAudio = GetComponentInParent<DoorAudioController>();

        if (doorAudio == null)
            Debug.LogWarning("DoorAudioController nicht gefunden!", this);
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
        if (doorAudio != null)
            doorAudio.ResetOpenSound();
    }
}
