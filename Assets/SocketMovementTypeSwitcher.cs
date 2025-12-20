using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SocketMovementTypeSwitcher : MonoBehaviour
{
    private UnityEngine.XR.Interaction.Toolkit.Interactors.XRSocketInteractor socket;

    private void Awake()
    {
        socket = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactors.XRSocketInteractor>();
    }

    private void OnEnable()
    {
        socket.selectEntered.AddListener(OnSelectEntered);
        socket.selectExited.AddListener(OnSelectExited);
    }

    private void OnDisable()
    {
        socket.selectEntered.RemoveListener(OnSelectEntered);
        socket.selectExited.RemoveListener(OnSelectExited);
    }

    private void OnSelectEntered(SelectEnterEventArgs args)
    {
        if (args.interactableObject is UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grab)
        {
           // Rigidbody rb = grab.GetComponent<Rigidbody>();
            //if (rb) rb.isKinematic = true;

            SetColliders(grab.transform, false);
        }
    }

    private void OnSelectExited(SelectExitEventArgs args)
    {
        if (args.interactableObject is UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grab)
        {

            Rigidbody rb = grab.GetComponent<Rigidbody>();
            rb.isKinematic = false;
            rb.useGravity = true;
            
            SetColliders(grab.transform, true);
        }
    }


    private void SetColliders(Transform root, bool enabled)
    {
         Collider[] colliders = root.GetComponentsInChildren<Collider>(true);

        foreach (Collider col in colliders)
        {
            // Skip the grab collider
            if (col.gameObject.name == "book")
                continue;

            col.enabled = enabled;
        }
    }
}
