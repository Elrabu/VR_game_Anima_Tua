using UnityEngine;
using System.Collections.Generic;


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
        if (collision.gameObject.name == "player_diner")
        {
            
        }
        Debug.Log("Interacted with: " + collision.gameObject.name);
        ResetIngredient();
    }
    
     void Start()
    {
        Spawn(breadBottom, breadBottomSpawn);
        Spawn(patty, pattySpawn);
        Spawn(breadTop, breadTopSpawn);
        Spawn(pickels, pickelsSpawn);
        Spawn(plate, plateSpawn);
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
