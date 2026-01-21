using UnityEngine;

public class TableGoalCheckerScript : MonoBehaviour
{
    [SerializeField] private GameObject tape;
    [SerializeField] private Transform tapespawn;
    private bool spawned = false;

    void OnTriggerEnter(Collider collision)
    {
        var checker = collision.GetComponent<PlateSocketScript>();

        if (checker == null)
            return;

        if (checker.filledplate)
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
