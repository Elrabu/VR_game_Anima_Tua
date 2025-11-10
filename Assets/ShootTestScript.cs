using UnityEngine;
using UnityEngine.InputSystem;

public class ShootTestScript : MonoBehaviour
{
    public InputActionProperty shoot; //need to be InputActionProperty!!
    public GameObject bulletPrefab;
    public Transform spawnPoint;
    public float secondsBetweenShoot = 0.1f;
    float trackShoot;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float value = shoot.action.ReadValue<float>();
        Debug.Log("Value: " + value);
        if (value == 1 && trackShoot <= 0)
        {
            Instantiate(bulletPrefab, spawnPoint.position, spawnPoint.rotation);
            trackShoot = secondsBetweenShoot;
        }
        trackShoot -= Time.deltaTime;
    }
}
