using UnityEngine;

public class BurgerCompleteChecker : MonoBehaviour
{
    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name == "bread_bottom")
        {
            Debug.Log("Checking!");
            BurgerStack burger = collision.GetComponent<BurgerStack>();
            Debug.Log("The burger is completed: " + burger.CompletedBurger);
        }
    }
}
