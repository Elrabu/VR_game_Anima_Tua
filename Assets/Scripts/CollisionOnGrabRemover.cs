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

    }

    void OnRelease(SelectExitEventArgs args)
    {

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
