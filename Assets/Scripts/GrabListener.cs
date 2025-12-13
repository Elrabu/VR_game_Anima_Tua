using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class GrabListener : MonoBehaviour
{
    [SerializeField] private UnityEngine.XR.Interaction.Toolkit.Interactors.XRBaseInteractor interactor;
    private IXRSelectInteractable currentlyHeld;

    public IXRSelectInteractable CurrentlyHeld => currentlyHeld; //exposes the private currentlyHeld as public

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
        currentlyHeld = args.interactableObject;
       // Debug.Log("Grabbed: " + args.interactableObject.transform.name);
    }

    void OnRelease(SelectExitEventArgs args)
    {
        currentlyHeld = null;
        //Debug.Log("Released: " + args.interactableObject.transform.name);
    }
}
