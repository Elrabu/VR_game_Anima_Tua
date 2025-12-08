using UnityEngine;
using UnityEngine.InputSystem;

public class FireHandShootScript : MonoBehaviour
{
    public InputActionProperty shoot; //need to be InputActionProperty!!
    public GameObject bulletPrefab;
    public Transform spawnPoint;
    public float secondsBetweenShoot = 0.1f;
    float trackShoot;
    void Update()
    {
        float value = shoot.action.ReadValue<float>();
        
        if (value == 1 && trackShoot <= 0)
        {
            Instantiate(bulletPrefab, spawnPoint.position, spawnPoint.rotation);
            trackShoot = secondsBetweenShoot;
        }
        trackShoot -= Time.deltaTime;
    }
}
