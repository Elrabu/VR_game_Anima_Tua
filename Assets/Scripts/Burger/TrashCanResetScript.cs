using UnityEngine;
using System.Collections.Generic;


public class TrashCanResetScript : MonoBehaviour
{
    [SerializeField] private GameObject breadBottom;
    [SerializeField] private GameObject patty;
    [SerializeField] private GameObject breadTop;
    [SerializeField] private GameObject pickels;

    [SerializeField] private Transform breadBottomSpawn;
    [SerializeField] private Transform pattySpawn;
    [SerializeField] private Transform breadTopSpawn;
    [SerializeField] private Transform pickelsSpawn;

    private readonly List<GameObject> spawnedIngredients = new(); //all currrent ingredients
    void OnTriggerEnter(Collider collision)
    {
        Debug.Log("Interacted with: " + collision.gameObject.name);
        ResetIngredient();
    }
    
     void Start()
    {
        Spawn(breadBottom, breadBottomSpawn);
        Spawn(patty, pattySpawn);
        Spawn(breadTop, breadTopSpawn);
        Spawn(pickels, pickelsSpawn);
    }
    public void ResetIngredient()
    {
        ClearSpawnedIngredients();
        
        Spawn(breadBottom, breadBottomSpawn);
        Spawn(patty, pattySpawn);
        Spawn(breadTop, breadTopSpawn);
        Spawn(pickels, pickelsSpawn);
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
    }
}
