using UnityEngine;

public class FoodSpawnScript : MonoBehaviour
{
    public GameObject objectToSpawn;
    public Transform spawnPoint;   

    public void SpawnObject()
    {
        Instantiate(objectToSpawn, spawnPoint.position, spawnPoint.rotation);
    }
}
