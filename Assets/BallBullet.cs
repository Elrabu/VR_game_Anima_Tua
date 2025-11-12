using UnityEngine;

public class BallBullet : BulletScript
{
    public float shootForce;
    
    public float timeToDestroy;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        setUpBullet(shootForce);
    }

    // Update is called once per frame
    void Update()
    {
        RemoveBullet(timeToDestroy);
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collided with " + collision.gameObject.name);
        //Destroy(gameObject);
    }
}
