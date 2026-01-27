using UnityEngine;

public class TableGoalCheckerScript : MonoBehaviour
{
    [SerializeField] private GameObject tape;
    [SerializeField] private Transform tapespawn;
    [SerializeField] private GameObject portal;
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
                                                                                                                                                // 
                                                                                                                                                // Sound change here
                                                                                                                                                //
                Instantiate(tape, tapespawn.position, tapespawn.rotation);
                AutoMusicCrossfade.instance.StartMusic();
                spawned = true;
                portal.SetActive(true);
            }  
        }
        else
        {
            Debug.Log("Burger not completed yet");
        }
    }
}
