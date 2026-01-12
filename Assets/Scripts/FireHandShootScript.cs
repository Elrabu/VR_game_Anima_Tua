using UnityEngine;
using UnityEngine.InputSystem;

public class FireHandShootScript : MonoBehaviour
{
    [SerializeField] private InputActionProperty shoot; //need to be InputActionProperty!!
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float secondsBetweenShoot = 0.1f;
    [SerializeField] private AudioSource shootAudioSource;
    [SerializeField] private AudioClip fireShootSound;
    float trackShoot;

    void Awake()
    {
        shootAudioSource = GetComponentInChildren<AudioSource>();
    }

    void Update()
    {           
        float value = shoot.action.ReadValue<float>();
        
        if (value == 1 && trackShoot <= 0)
        {
            Instantiate(bulletPrefab, spawnPoint.position, spawnPoint.rotation);
            
            Debug.Log("shootAudioSource " + shootAudioSource);
            Debug.Log("fireShootSound " + fireShootSound);

            if (shootAudioSource != null && fireShootSound != null)
            {   
                Debug.Log("Shoot triggered");
                //shootAudioSource.pitch = Random.Range(0.95f, 1.05f);
                shootAudioSource.PlayOneShot(fireShootSound);            
            }
            trackShoot = secondsBetweenShoot;
        }
        trackShoot -= Time.deltaTime;
    }
}
