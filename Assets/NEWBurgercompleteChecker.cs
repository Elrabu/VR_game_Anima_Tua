using UnityEngine;

public class NEWBurgercompleteChecker : MonoBehaviour
{
    [SerializeField] private GameObject tape;
    [SerializeField] private Transform tapespawn;
    private void OnTriggerEnter(Collider other)
    {
        var checker = other.GetComponent<SocketIngredientCheckerScript>();

        if (checker == null)
            return;

        if (checker.CompletedBurger)
        {
            Debug.Log("Completed burger entered the box!");
            Instantiate(tape, tapespawn.position, tapespawn.rotation);
        }
        else
        {
            Debug.Log("Burger not completed yet");
        }
    }
}
