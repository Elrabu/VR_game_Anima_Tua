using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SocketCollisionEnabledSwitcher : MonoBehaviour
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
    {   //casts args.interactableObject to XRGrabInteractable and assigns it to "grab"
        if (args.interactableObject is UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grab) 
        {
            //remove cooking some if put here:
            GameObject[] smokes = GameObject.FindGameObjectsWithTag("CookingSmoke");
            foreach (GameObject smoke in smokes)
            {
                Destroy(smoke);
            }

            SetColliders(grab.transform, false);
        }
    }

    private void OnSelectExited(SelectExitEventArgs args)
    {   //casts args.interactableObject to XRGrabInteractable and assigns it to "grab"
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
            {
                Animator animator = col.gameObject.GetComponentInChildren<Animator>(); //get Animator from book Game Object
                if (animator != null && enabled == false && animator.GetBool("IsOpen"))
                {
                    animator.SetBool("IsOpen", false);
                    animator.SetTrigger("Close");
                }
                continue;
            }
            col.enabled = enabled;
        }
    }
}
