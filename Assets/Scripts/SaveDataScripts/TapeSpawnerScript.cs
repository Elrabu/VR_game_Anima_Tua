using UnityEngine;

public class TapeSpawnerScript : MonoBehaviour
{
    private string requiredLevelName = "Dungeon01";

    [SerializeField] private GameObject prefabToSpawn;
    [SerializeField] private Transform spawnPoint;

    void Start()
    {
        if (SaveData.Instance != null)
        {
            SaveData.Instance.LoadFromJson();
            if (SaveData.Instance.settings.levels.Contains(requiredLevelName))
            {
                Debug.Log("Spawned Tape");
                SpawnPrefab();
            }
        }

    }

    void SpawnPrefab()
    {
        Instantiate(prefabToSpawn, spawnPoint.position, spawnPoint.rotation);
    }
}
