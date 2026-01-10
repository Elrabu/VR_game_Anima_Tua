using System.Collections.Generic;
using UnityEngine;

public class BurgerStack : MonoBehaviour
{
    public List<BurgerIngredientType> currentStack = new(); //List with current Burger Ingredients
    private readonly BurgerIngredientType[] correctRecipe =
    {
        BurgerIngredientType.BottomBun,
        BurgerIngredientType.Patty,
        BurgerIngredientType.Pickles,
        BurgerIngredientType.TopBun
    };

    private bool completed = false;

    private void Awake()
    {
        currentStack = new List<BurgerIngredientType>
        {
            BurgerIngredientType.BottomBun
        };
    }

    public void AddIngredient(BurgerIngredient ingredient)
    {
        if (completed)
            return;

        currentStack.Add(ingredient.ingredientType);
        Debug.Log($"{name}: {string.Join(", ", currentStack)}");
        CheckBurger();
    }


    private void CheckBurger()
    {
        if (currentStack.Count != correctRecipe.Length)
            return;

        for (int i = 0; i < correctRecipe.Length; i++)
        {
            if (currentStack[i] != correctRecipe[i])
                return;
        }

        completed = true;
        Debug.Log("Correct burger assembled!");
    }
}
