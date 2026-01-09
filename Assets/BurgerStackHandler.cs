using System.Collections.Generic;
using UnityEngine;

public class BurgerStack : MonoBehaviour
{
    public List<BurgerIngredientType> currentStack = new(); //List with current Burger Ingredients
    private readonly BurgerIngredientType[] correctRecipe =
    {
        //BurgerIngredientType.BottomBun,
        BurgerIngredientType.Patty,
        BurgerIngredientType.Pickles,
        BurgerIngredientType.TopBun
    };

    public void AddIngredient(BurgerIngredient ingredient)
    {
        currentStack.Add(ingredient.ingredientType);
        CheckBurger();
    }

    public void RemoveIngredient(BurgerIngredient ingredient)
    {
        currentStack.Remove(ingredient.ingredientType);
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

        Debug.Log("Correct burger assembled!");
    }
}
