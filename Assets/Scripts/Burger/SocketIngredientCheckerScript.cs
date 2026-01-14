using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections.Generic;

public class SocketIngredientCheckerScript : MonoBehaviour
{
    [SerializeField] private UnityEngine.XR.Interaction.Toolkit.Interactors.XRSocketInteractor socket1;
    [SerializeField] private UnityEngine.XR.Interaction.Toolkit.Interactors.XRSocketInteractor socket2;
    [SerializeField] private UnityEngine.XR.Interaction.Toolkit.Interactors.XRSocketInteractor socket3;

    public List<BurgerIngredientType> currentStack = new();

    private readonly BurgerIngredientType[] correctRecipe =
    {
        BurgerIngredientType.Patty,
        BurgerIngredientType.Pickles,
        BurgerIngredientType.TopBun
    };

    private bool completed = false;
    public bool CompletedBurger => completed; //exposes the variable

    void Awake()
    {
        socket1.enabled = true;
        socket2.enabled = false;
        socket3.enabled = false;

        socket1.selectEntered.AddListener(OnSocket1Filled);
        socket2.selectEntered.AddListener(OnSocket2Filled);
        socket3.selectEntered.AddListener(OnSocket3Filled);
    }

    private void OnSocket1Filled(SelectEnterEventArgs args)
    {
        Transform child = args.interactableObject.transform.Find("patty_grilled");
        if (child != null)
        {
            AddIngredient(args);
        }

        socket2.enabled = true;
    }

    private void OnSocket2Filled(SelectEnterEventArgs args)
    {
        AddIngredient(args);
        socket3.enabled = true;
    }

    private void OnSocket3Filled(SelectEnterEventArgs args)
    {
        AddIngredient(args);
        CheckRecipe();
    }

    private void AddIngredient(SelectEnterEventArgs args)
    {
        var ingredient = args.interactableObject.transform.GetComponent<BurgerIngredient>();

        if (ingredient == null)
        {
            Debug.LogWarning("Object placed in socket has no BurgerIngredient component.");
            return;
        }

        currentStack.Add(ingredient.ingredientType);
        Debug.Log($"Added ingredient: {ingredient.ingredientType}");
    }

    private void CheckRecipe()
    {
        if (currentStack.Count != correctRecipe.Length)
            return;

        for (int i = 0; i < correctRecipe.Length; i++)
        {
            if (currentStack[i] != correctRecipe[i])
            {
                Debug.Log("Wrong recipe");
                return;
            }
        }
        Debug.Log("Correct burger!");
        completed = true;
    }
}
