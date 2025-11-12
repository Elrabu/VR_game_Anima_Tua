using UnityEngine;

public class BulletScript : MonoBehaviour
{  
    Rigidbody rigidbody;
    public void setUpBullet(float shootForce)
    {
        rigidbody = GetComponent<Rigidbody>();
        GetComponent<Rigidbody>().AddForce(transform.forward * shootForce);
    }

    public void RemoveBullet(float timeToDestroy)
    {
        timeToDestroy -= Time.deltaTime;
        if (timeToDestroy <= 0)
        {
            Destroy(gameObject);
        }
    }
}
