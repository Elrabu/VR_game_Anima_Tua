using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SocketBurgerTestScript : MonoBehaviour
{
    private UnityEngine.XR.Interaction.Toolkit.Interactors.XRSocketInteractor socket;
    [SerializeField] private BurgerStack burgerStack;

    private void Awake()
    {
        socket = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactors.XRSocketInteractor>();
    }

    private void OnEnable()
    {
        socket.selectEntered.AddListener(OnSelectEntered);
       // socket.selectExited.AddListener(OnSelectExited);
    }

    private void OnDisable()
    {
        socket.selectEntered.RemoveListener(OnSelectEntered);
       // socket.selectExited.RemoveListener(OnSelectExited);
    }

    private void OnSelectEntered(SelectEnterEventArgs args)
    {   //casts args.interactableObject to XRGrabInteractable and assigns it to "grab"
        if (args.interactableObject is UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grab) 
        {
            SetColliders(grab.transform, false);
            //Debug.Log(grab.gameObject.name);
            if (grab.gameObject.name == "patty" || grab.gameObject.name == "patty(Clone)")
            {
                Transform child = grab.transform.Find("patty_raw");
                if (child == null || child.gameObject.activeInHierarchy)
                {
                    return;
                } 
            }
            

            BurgerIngredient ingredient = grab.GetComponent<BurgerIngredient>();
            if (ingredient == null || burgerStack == null)
                return;

            burgerStack.AddIngredient(ingredient);
        }
    }
    private void SetColliders(Transform root, bool enabled)
    {
         Collider[] colliders = root.GetComponentsInChildren<Collider>(true);

        foreach (Collider col in colliders)
        {
            if (col.gameObject.name == "socket" || col.gameObject.name == "book")
            {
                continue;
            }
            col.enabled = enabled;
        }
    }
}

