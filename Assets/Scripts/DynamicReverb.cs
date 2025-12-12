using UnityEngine;

public class DynamicReverb : MonoBehaviour
{
    public static DynamicReverb instance; // Singleton reference

    [Tooltip("Max distance the raycasts will check")]
    public float maxCheckDistance = 20f;

    [Tooltip("LayerMask defining which layers are considered walls/room boundaries")]
    public LayerMask wallMask;

    // Current average distance to walls
    public float avgDistance { get; private set; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Keep between scene changes
        }
        else
        {
            Destroy(gameObject); // Prevent duplicates
        }
    }

    private void Update()
    {
        avgDistance = MeasureSpaceAround(transform.position);
    }

    /// <summary>
    /// Raycast in 6 directions from a position to measure average distance to walls
    /// </summary>
    private float MeasureSpaceAround(Vector3 position)
    {
        Vector3[] directions = {
            Vector3.forward, Vector3.back,
            Vector3.right, Vector3.left,
            Vector3.up, Vector3.down
        };

        float totalDist = 0f;
        foreach (var dir in directions)
        {
            if (Physics.Raycast(position, dir, out RaycastHit hit, maxCheckDistance, wallMask))
            {
                totalDist += hit.distance;
            }
            else
            {
                totalDist += maxCheckDistance; // No wall hit → max distance
            }
        }
        return totalDist / directions.Length;
    }

    /// <summary>
    /// Ensures there is always a DynamicReverb instance in the scene
    /// </summary>
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void AutoCreateInstance()
    {
        if (instance == null)
        {
            GameObject drObj = new GameObject("DynamicReverbManager");
            instance = drObj.AddComponent<DynamicReverb>();
            Debug.Log("DynamicReverb instance auto-created at runtime.");
        }
    }
}