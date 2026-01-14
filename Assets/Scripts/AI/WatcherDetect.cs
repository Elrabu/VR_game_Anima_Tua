using UnityEngine;

public class WatcherDetect : MonoBehaviour
{
    [SerializeField] private float detectionRadius = 10f;
    [SerializeField] private LayerMask detectionMask;
    [SerializeField] private bool isDebug = true;

    public GameObject Target
    {
        get;
        set;
    }

    public GameObject Detector()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius, detectionMask);

        if (colliders.Length > 0)
        {
            Target = colliders[0].gameObject;
        }
        else
        {
            Target = null;
        }
        return Target;
    }

    private void OnDrawGizmos()
    {
        if (!isDebug || this.enabled == false) return;

        Gizmos.color = Target ? Color.green : Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

    }
}
