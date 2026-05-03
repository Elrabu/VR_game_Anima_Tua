using UnityEngine;
using System.Collections.Generic;
using System.Collections;



public class TrashCanResetScript : MonoBehaviour
{
    [SerializeField] private GameObject breadBottom;
    [SerializeField] private GameObject patty;
    [SerializeField] private GameObject breadTop;
    [SerializeField] private GameObject pickels;
    [SerializeField] private GameObject plate;

    [SerializeField] private Transform breadBottomSpawn;
    [SerializeField] private Transform pattySpawn;
    [SerializeField] private Transform breadTopSpawn;
    [SerializeField] private Transform pickelsSpawn;
    [SerializeField] private Transform plateSpawn;

    private readonly List<GameObject> spawnedIngredients = new(); //all currrent ingredients
    void OnTriggerEnter(Collider collision)
    {
        Debug.Log("Interacted with: " + collision.gameObject.name);
        if (collision.gameObject.name != "VR Player")
        {
            ResetIngredient();
        }
    }

    void Start()
    {
        StartCoroutine(SpawnNextFrame());
    }

    private IEnumerator SpawnNextFrame()
    {
        yield return new WaitForEndOfFrame();

        Debug.Log("About to spawn burger items");
        Spawn(breadBottom, breadBottomSpawn);
        Spawn(patty, pattySpawn);
        Spawn(breadTop, breadTopSpawn);
        Spawn(pickels, pickelsSpawn);
        Spawn(plate, plateSpawn);
        Debug.Log("spawned burger items");
    }
    public void ResetIngredient()
    {
        ClearSpawnedIngredients();

        Spawn(breadBottom, breadBottomSpawn);
        Spawn(patty, pattySpawn);
        Spawn(breadTop, breadTopSpawn);
        Spawn(pickels, pickelsSpawn);
        Spawn(plate, plateSpawn);
    }

    private void Spawn(GameObject prefab, Transform spawnPoint)
    {
        GameObject instance = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation);
        Debug.Log($"Spawned {prefab.name} at {spawnPoint.position}");
        Debug.Log($"Spawn scale of {prefab.name}:{spawnPoint.parent?.lossyScale}");
        Debug.Log($"Spawnpoint world: {spawnPoint.position}");
        Debug.Log($"Spawnpoint local: {spawnPoint.localPosition}");
        spawnedIngredients.Add(instance);
    }

    private void ClearSpawnedIngredients()
    {
        foreach (var ingredient in spawnedIngredients)
        {
            if (ingredient != null)
                Destroy(ingredient);
        }

        spawnedIngredients.Clear();

        GameObject[] smokes = GameObject.FindGameObjectsWithTag("CookingSmoke");
        foreach (GameObject smoke in smokes)
        {
            Destroy(smoke);
        }
    }
}
