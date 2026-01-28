using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(AudioSource))]
public class BurgerIngredient : MonoBehaviour
{
    public BurgerIngredientType ingredientType;
    public AudioClip placeSound;

    private AudioSource audioSource;
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grab;

    public bool isPartOfBurger = false;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        grab = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();

        grab.selectExited.AddListener(OnReleased);
    }

    private void OnReleased(SelectExitEventArgs args)
    {
        //  Kein Placement-Sound, wenn bereits Teil des Burgers
        if (isPartOfBurger)
            return;

        audioSource.PlayOneShot(placeSound);
    }

    public void MarkAsBurgerPart()
    {
        isPartOfBurger = true;
    }
}
