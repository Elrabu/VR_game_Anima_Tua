using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class CollisionOnGrabRemover : MonoBehaviour
{
   [SerializeField] private UnityEngine.XR.Interaction.Toolkit.Interactors.XRBaseInteractor interactor;
    void OnEnable()
    {
        interactor.selectEntered.AddListener(OnGrab);
        interactor.selectExited.AddListener(OnRelease);
    }

    void OnDisable()
    {
        interactor.selectEntered.RemoveListener(OnGrab);
        interactor.selectExited.RemoveListener(OnRelease);
    }

    void OnGrab(SelectEnterEventArgs args)
    {
        if (args.interactableObject is UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grab) 
        {
            SetColliders(grab.transform, false);
        }
    }

    void OnRelease(SelectExitEventArgs args)
    {
        if (args.interactableObject is UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grab)
        {
            SetColliders(grab.transform, true);
        }
    }

     private void SetColliders(Transform root, bool enabled)
    {
         Collider[] colliders = root.GetComponentsInChildren<Collider>(true);

        foreach (Collider col in colliders)
        {
            col.enabled = enabled;
        }
    }
}
