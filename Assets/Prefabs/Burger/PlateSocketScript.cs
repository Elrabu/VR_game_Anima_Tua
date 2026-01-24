using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PlateSocketScript : MonoBehaviour
{
   /* [SerializeField] private GameObject tape;
    [SerializeField] private Transform tapespawn; */
    [SerializeField] private UnityEngine.XR.Interaction.Toolkit.Interactors.XRSocketInteractor socket;
    private bool spawned = false;
    private bool plate = false;
    public bool filledplate => plate;

    private void OnEnable()
    {
        socket.selectEntered.AddListener(OnSelectEntered);
    }

    private void OnDisable()
    {
        socket.selectEntered.RemoveListener(OnSelectEntered);
    }

    private void OnSelectEntered(SelectEnterEventArgs args)
    {   //casts args.interactableObject to XRGrabInteractable and assigns it to "grab"
        if (args.interactableObject is UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grab) 
        {
           checkburger(grab);
           SetColliders(grab.transform, false);
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

    private void checkburger(UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grab)
    {
         //check completed burger for completed var
        var checker = grab.GetComponent<SocketIngredientCheckerScript>();

        if (checker == null)
            return;

        if (checker.CompletedBurger)
        {
            if (spawned == false)
            {
                Debug.Log("Completed burger entered the box!");
              //  Instantiate(tape, tapespawn.position, tapespawn.rotation);
                spawned = true;
                plate = true;
            }  
        }
        else
        {
            Debug.Log("Burger not completed yet");
        }
    }
}
