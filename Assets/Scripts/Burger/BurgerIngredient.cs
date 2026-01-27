using UnityEngine;

public class BurgerIngredient : MonoBehaviour
{
    public BurgerIngredientType ingredientType;


    public AudioClip placeSound;
    public AudioSource audioSource;

    public bool isPartOfBurger = false;

    private void Awake()
    {
        if (!audioSource)
            audioSource = GetComponent<AudioSource>();
    }

    // Wird aufgerufen, wenn Zutat irgendwo abgelegt wird
    public void TryPlayPlaceSound()
    {
        if (isPartOfBurger) return; // 🔒 blockiert Sound wenn Burger-Teil

        audioSource.PlayOneShot(placeSound);
    }

    // Wird NUR aufgerufen, wenn es auf eine andere Zutat gelegt wird
    public void MarkAsBurgerPart()
    {
        isPartOfBurger = true;
        audioSource.enabled = false; // ab jetzt stumm
    }
}
