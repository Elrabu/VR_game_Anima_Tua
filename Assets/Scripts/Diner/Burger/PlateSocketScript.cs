using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Filtering;

public class PlateSocketScript : MonoBehaviour, IXRSelectFilter
{
    [SerializeField] private UnityEngine.XR.Interaction.Toolkit.Interactors.XRSocketInteractor socket;
    [SerializeField] private DinerQuestController questController;

    private bool spawned = false;
    private bool plate = false;

    public bool filledplate => plate;
    public bool canProcess => isActiveAndEnabled;

    
    public bool Process(UnityEngine.XR.Interaction.Toolkit.Interactors.IXRSelectInteractor interactor, UnityEngine.XR.Interaction.Toolkit.Interactables.IXRSelectInteractable interactable)
    {
        // Nur XRGrabInteractables prüfen
        if (interactable is not UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grab)
            return false;

        var checker = grab.GetComponent<SocketIngredientCheckerScript>();

        // Objekt wird NUR akzeptiert, wenn CompletedBurger == true
        if (checker == null || !checker.CompletedBurger)
        {
            Debug.Log("Plate rejected: Burger not completed.");
            return false;
        }

        return true;
    }
    
    private void Awake()
    {
        // Filter beim Socket registrieren
        socket.selectFilters.Add(this);
    }

    private void OnDestroy()
    {
        socket.selectFilters.Remove(this);
    }

    private void OnEnable()
    {
        socket.selectEntered.AddListener(OnSelectEntered);
    }

    private void OnDisable()
    {
        socket.selectEntered.RemoveListener(OnSelectEntered);
    }

    private void OnSelectEntered(SelectEnterEventArgs args)
    {   // casts args.interactableObject to XRGrabInteractable and assigns it to "grab"
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
        // check completed burger for completed var
        var checker = grab.GetComponent<SocketIngredientCheckerScript>();

        if (checker == null)
            return;

        if (checker.CompletedBurger)
        {
            if (spawned == false)
            {
                Debug.Log("Completed burger!");
                handleQuest();
            }

            spawned = true;
            plate = true;
            
        }
        else
        {
            Debug.Log("Burger not completed yet");
        }
    }

    private void handleQuest()
    {
        if (!TryResolveQuestController())
        {
            Debug.LogWarning("No DinerQuestController found.");
            return;
        }

        questController.HandleQuest();
    }

    private bool TryResolveQuestController()
    {
        if (questController != null)
            return true;

        questController = FindAnyObjectByType<DinerQuestController>();
        return questController != null;
    }
}
