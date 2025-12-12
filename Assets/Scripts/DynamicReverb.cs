using UnityEngine;

public class DynamicReverb : MonoBehaviour
{
    public static DynamicReverb instance; // Singleton reference

    public float maxCheckDistance = 20f;
    public LayerMask wallMask;
    public float avgDistance { get; private set; }

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    private void Update()
    {
        avgDistance = MeasureSpaceAround(transform.position);
    }

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
                totalDist += hit.distance;
            else
                totalDist += maxCheckDistance;
        }

        return totalDist / directions.Length;
    }
}