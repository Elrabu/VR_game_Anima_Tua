using UnityEngine;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable))]
public class BurgerBase : MonoBehaviour
{
    public AudioClip assembleSound;

    private AudioSource audioSource;
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grab;

    private int ingredientCount = 0;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        grab = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();

        grab.selectExited.AddListener(OnBurgerReleased);
    }

    public void OnIngredientAdded()
    {
        ingredientCount++;
    }

    private void OnBurgerReleased(SelectExitEventArgs args)
    {
        if (ingredientCount >= 2 && assembleSound != null)
        {
            audioSource.PlayOneShot(assembleSound);
        }
    }
}

