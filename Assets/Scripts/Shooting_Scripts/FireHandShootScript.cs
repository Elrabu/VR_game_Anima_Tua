using UnityEngine;
using UnityEngine.InputSystem;

public class FireHandShootScript : MonoBehaviour
{
    [SerializeField] private InputActionProperty shoot; //need to be InputActionProperty!!
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float secondsBetweenShoot = 0.1f;
    [SerializeField] private float pressedThreshold = 0.5f;
    [SerializeField] private bool autoEnableShootAction = true;
    [SerializeField] private bool verboseDebug = false;

    float trackShoot;

    private void OnEnable()
    {
        if (autoEnableShootAction && shoot.action != null && !shoot.action.enabled)
        {
            shoot.action.Enable();
        }

        if (verboseDebug)
        {
            Debug.Log($"FireHandShootScript enabled on {name}. Action: {shoot.action?.name ?? "<none>"}");
        }
    }

    private void OnDisable()
    {
        if (autoEnableShootAction && shoot.action != null && shoot.action.enabled)
        {
            shoot.action.Disable();
        }
    }

    void Update()
    {           
        if (shoot.action == null)
        {
            if (verboseDebug)
            {
                Debug.LogWarning($"FireHandShootScript on {name}: shoot action is missing.");
            }
            return;
        }

        if (bulletPrefab == null || spawnPoint == null)
        {
            if (verboseDebug)
            {
                Debug.LogWarning($"FireHandShootScript on {name}: bulletPrefab or spawnPoint missing.");
            }
            return;
        }

        float value = shoot.action.ReadValue<float>();
        
        if (value > pressedThreshold && trackShoot <= 0)
        {
            Instantiate(bulletPrefab, spawnPoint.position, spawnPoint.rotation);

            if (verboseDebug)
            {
                Debug.Log($"FireHandShootScript fired on {name}.");
            }
             
            trackShoot = secondsBetweenShoot;
        }
        trackShoot -= Time.deltaTime;
    }
}
