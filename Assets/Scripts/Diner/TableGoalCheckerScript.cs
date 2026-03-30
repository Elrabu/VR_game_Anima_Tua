using UnityEngine;

public class TableGoalCheckerScript : MonoBehaviour
{
    [SerializeField] private GameObject tape;
    [SerializeField] private GameObject tapeHolder;
    [SerializeField] private GameObject tapeDeck;
    [SerializeField] private GameObject portal;

    [Header("Quest End")]
    [SerializeField] private ParticleSystem questEndParticleSystem;
    [SerializeField] private GameObject placeIndicator;
    [SerializeField] private GameObject currentTask;
    [SerializeField] private AudioSource newOrder;

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
                spawned = true;
                portal.SetActive(true);
                tapeHolder.SetActive(true);
                tape.SetActive(true);
                tapeDeck.SetActive(true);

                currentTask.SetActive(false);
                placeIndicator.SetActive(false);
                questEndParticleSystem.Stop();
                newOrder.Play();
            }  
        }
        else
        {
            Debug.Log("Burger not completed yet");
        }
    }
}
