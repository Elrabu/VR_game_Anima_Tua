using UnityEngine;

public class WatcherSight : MonoBehaviour
{
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private LayerMask obstacleLayer;
    [SerializeField] private float detectionRange = 10.0f;
    [SerializeField] private float detectionHeight = 3.0f;
    [SerializeField] private bool isDebug = true;

    public GameObject PerformDetection(GameObject gameObject)
    {
        Vector3 startPosition = transform.position + Vector3.up * detectionHeight;
        Vector3 targetPosition = gameObject.transform.position;
        Vector3 dir = (targetPosition - startPosition).normalized;
        float distanceToTarget = Vector3.Distance(startPosition, targetPosition);

        if (distanceToTarget > detectionRange)
        {
            if (isDebug && this.enabled)
            {
                Debug.DrawLine(startPosition, startPosition + dir * detectionRange, Color.yellow);
            }
            return null;
        }

        RaycastHit hit;
        int combinedLayers = playerLayer | obstacleLayer;

        if (Physics.Raycast(startPosition, dir, out hit, detectionRange, combinedLayers))
        {
            if (hit.collider.gameObject == gameObject)
            {
                if (isDebug && this.enabled)
                {
                    Debug.DrawLine(startPosition, hit.point, Color.red);
                }
                return hit.collider.gameObject;
            }
            else
            {
                // Hit wall or something like this
                if (isDebug && this.enabled)
                {
                    Debug.DrawLine(startPosition, hit.point, Color.blue);
                    Debug.DrawLine(hit.point, targetPosition, Color.cyan);
                }
                return null;
            }
        }

        // Nothing hit
        if (isDebug && this.enabled)
        {
            Debug.DrawLine(startPosition, targetPosition, Color.green);
        }

        return null;
    }

    private void OnDrawGizmos()
    {
        if (isDebug)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position + Vector3.up * detectionHeight, 0.3f);

            Gizmos.color = new Color(1, 0, 0, 0.3f);
            Gizmos.DrawWireSphere(transform.position + Vector3.up * detectionHeight, detectionRange);
        }
    }
}
