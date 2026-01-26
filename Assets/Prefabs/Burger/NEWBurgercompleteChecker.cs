using UnityEngine;

public class NEWBurgercompleteChecker : MonoBehaviour
{
    [SerializeField] private GameObject tape;
    [SerializeField] private Transform tapespawn;
    private bool spawned = false;
    private void OnTriggerEnter(Collider other)
    {
        var checker = other.GetComponent<SocketIngredientCheckerScript>();

        if (checker == null)
            return;

        if (checker.CompletedBurger)
        {
            if (spawned == false)
            {
                Debug.Log("Completed burger entered the box!");
                Instantiate(tape, tapespawn.position, tapespawn.rotation);
                spawned = true;
            }  
        }
        else
        {
            Debug.Log("Burger not completed yet");
        }
    }
}
