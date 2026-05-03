using UnityEngine;
using UnityEngine.XR;
using System.Collections.Generic;

public class EnemyFeedback : MonoBehaviour
{
    [Header("Heartbeat Settings")]
    [SerializeField] private float maxHeartbeatDistance = 5f;
    [SerializeField] private float minHeartbeatDistance = 0.5f;
    [SerializeField] private float minHeartbeatRate = 60f; // BPM
    [SerializeField] private float maxHeartbeatRate = 180f; // BPM
    [SerializeField] private float heartbeatDuration = 0.1f;

    [Header("Haptic Settings")]
    [SerializeField] private float minHapticIntensity = 0.1f;
    [SerializeField] private float maxHapticIntensity = 1f;

    [Header("Line of Sight Settings")]
    [SerializeField] private LayerMask heartbeatBlockerMask;

    [Header("References")]
    [SerializeField] private Transform playerHead;

    private List<Transform> nearbyEnemies = new List<Transform>();
    private float heartbeatTimer = 0f;

    private void Start()
    {
        // Try to find player head automatically
        if (playerHead == null)
        {
            Camera cam = GetComponentInParent<Camera>();
            if (cam != null)
                playerHead = cam.transform;
        }

        if (heartbeatBlockerMask == 0)
            heartbeatBlockerMask = LayerMask.GetMask("HeartBeatBlocker");

        if (playerHead == null)
        {
            Debug.LogWarning("Player head not assigned!");
        }
    }

    private void Update()
    {
        if (playerHead == null) return;

        UpdateNearbyEnemies();
        UpdateHeartbeat();
    }

    private void UpdateNearbyEnemies()
    {
        nearbyEnemies.Clear();

        EnemyHandlerScript[] allEnemies = FindObjectsOfType<EnemyHandlerScript>();

        foreach (var enemy in allEnemies)
        {
            if (enemy == null) continue;

            float distance = Vector3.Distance(playerHead.position, enemy.transform.position);

            if (distance < maxHeartbeatDistance && !IsHeartbeatBlocked(enemy.transform))
            {
                nearbyEnemies.Add(enemy.transform);
            }
        }
    }

    private void UpdateHeartbeat()
    {
        if (nearbyEnemies.Count == 0)
            return;

        float closestDistance = float.MaxValue;

        foreach (var enemy in nearbyEnemies)
        {
            float distance = Vector3.Distance(playerHead.position, enemy.position);
            if (distance < closestDistance)
                closestDistance = distance;
        }

        float distanceRatio = Mathf.Clamp01(
            (closestDistance - minHeartbeatDistance) /
            (maxHeartbeatDistance - minHeartbeatDistance)
        );

        float heartbeatRate = Mathf.Lerp(maxHeartbeatRate, minHeartbeatRate, distanceRatio);
        float hapticIntensity = Mathf.Lerp(maxHapticIntensity, minHapticIntensity, distanceRatio);

        float timeBetweenPulses = 60f / heartbeatRate;

        heartbeatTimer += Time.deltaTime;

        if (heartbeatTimer >= timeBetweenPulses)
        {
            TriggerHeartbeatHaptics(hapticIntensity);
            heartbeatTimer = 0f;
        }
    }

    private bool IsHeartbeatBlocked(Transform enemy)
    {
        Vector3 origin = playerHead.position;
        Vector3 direction = enemy.position - origin;
        float distance = direction.magnitude;

        if (distance <= 0f)
            return false;

        direction /= distance;

        return Physics.Raycast(origin, direction, distance, heartbeatBlockerMask, QueryTriggerInteraction.Ignore);
    }

    private void TriggerHeartbeatHaptics(float intensity)
    {
        SendHapticImpulse(XRNode.LeftHand, intensity, heartbeatDuration);
        SendHapticImpulse(XRNode.RightHand, intensity, heartbeatDuration);
    }

    private void SendHapticImpulse(XRNode node, float intensity, float duration)
    {
        InputDevice device = InputDevices.GetDeviceAtXRNode(node);

        if (!device.isValid)
            return;

        if (device.TryGetHapticCapabilities(out HapticCapabilities capabilities))
        {
            if (capabilities.supportsImpulse)
            {
                // channel 0 = default vibration channel
                device.SendHapticImpulse(0u, intensity, duration);
            }
        }
    }
}