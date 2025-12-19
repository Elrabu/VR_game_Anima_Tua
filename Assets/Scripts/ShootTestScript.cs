using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Audio;

public class ShootTestScript : MonoBehaviour
{
    [SerializeField] private InputActionProperty shoot; //need to be InputActionProperty!!
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float secondsBetweenShoot = 0.1f;
    [SerializeField] private GrabListener grabListener;
    [SerializeField] private AudioClip shootClip;
    [SerializeField] private AudioMixerGroup sfxMixerGroup;

    float trackShoot;
    string gun;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float value = shoot.action.ReadValue<float>();
        var item = grabListener.CurrentlyHeld; //get currently held item
        
        if (item != null)
        {
            gun = item.transform.name;
            //Debug.Log("Item: " + item.transform.name); 
        } else
        {
            gun = "null";
        }
        //Debug.Log("Value: " + value);
        //Debug.Log(gun);

        if (value == 1 && trackShoot <= 0 && gun == gameObject.transform.name)
        {
            Instantiate(bulletPrefab, spawnPoint.position, spawnPoint.rotation);
            SFXManager.instance.PlaySFXClip(shootClip, sfxMixerGroup, transform.position, 1f);
            trackShoot = secondsBetweenShoot;
        }
        trackShoot -= Time.deltaTime;
    }
}
