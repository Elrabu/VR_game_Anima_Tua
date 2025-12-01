using UnityEngine;
using UnityEngine.InputSystem;

public class ShootTestScript : MonoBehaviour
{
    public InputActionProperty shoot; //need to be InputActionProperty!!
    public GameObject bulletPrefab;
    public Transform spawnPoint;
    public float secondsBetweenShoot = 0.1f;
    float trackShoot;
    string gun;
    [SerializeField] private GrabListener grabListener;
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
            trackShoot = secondsBetweenShoot;
        }
        trackShoot -= Time.deltaTime;
    }
}
