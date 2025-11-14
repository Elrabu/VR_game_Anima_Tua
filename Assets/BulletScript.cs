using UnityEngine;

public class BulletScript : MonoBehaviour
{  
    private float lifetime;
    Rigidbody rigidbody;
    public void setUpBullet(float shootForce, float timeToDestroy)
    {
        rigidbody = GetComponent<Rigidbody>();
        GetComponent<Rigidbody>().AddForce(transform.forward * shootForce);

        lifetime = timeToDestroy;
    }

    void Update()
    {
        lifetime -= Time.deltaTime;
        if (lifetime <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        HandleCollision(collision);
    }

    public virtual void HandleCollision(Collision collision) //base class method that can be overwritten
    {
        Debug.Log("Collided with" + collision.gameObject.name);
    }
}
