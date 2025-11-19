using UnityEngine;

public class BulletScript : MonoBehaviour
{  
    private float lifetime;
    Rigidbody rigidbody;

    protected GameObject spawnedParticle;
    protected Transform spawnPoint;

    public void setUpBullet(float shootForce, float timeToDestroy, GameObject spawned, Transform point)
    {
        rigidbody = GetComponent<Rigidbody>();
        GetComponent<Rigidbody>().AddForce(transform.forward * shootForce);

        lifetime = timeToDestroy;
        spawnedParticle = spawned;
        spawnPoint = point;
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
        spawnParticleSystem(spawnedParticle, spawnPoint);
    }

    public virtual void HandleCollision(Collision collision) //base class method that can be overwritten
    {
       // Debug.Log("Collided with" + collision.gameObject.name);
        Destroy(gameObject);
    }

    void spawnParticleSystem(GameObject spawnedParticle, Transform spawnPoint)
    {
        GameObject particle = Instantiate(spawnedParticle, spawnPoint.position, spawnPoint.rotation);
        particle.GetComponent<ParticleSystem>().Play();
        Destroy(particle, 0.5f);
    }
    
}
