using UnityEngine;

public class NEWBurgercompleteChecker : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var checker = other.GetComponent<SocketIngredientCheckerScript>();

        if (checker == null)
            return;

        if (checker.CompletedBurger)
        {
            Debug.Log("Completed burger entered the box!");
            // Do success logic here
        }
        else
        {
            Debug.Log("Burger not completed yet");
            // Do failure / reject logic here
        }
    }
}
