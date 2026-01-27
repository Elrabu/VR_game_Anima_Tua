using UnityEngine;

public class BurgerBase : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip assembleSound;

    public AudioClip breadClip;

    private int ingredientCount = 1; // BreadBottom zählt

    public void OnIngredientAdded()
    {
        ingredientCount++;

        if (ingredientCount >= 2)
        {
            audioSource.PlayOneShot(assembleSound);
        }
        else {
            audioSource.PlayOneShot(breadClip);
        }
    }
}
