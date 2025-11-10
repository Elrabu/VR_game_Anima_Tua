using UnityEngine;

public class BulletScript : MonoBehaviour
{

    public float shootForce = 1000;
    Rigidbody rigidbody;
    public float timeToDestroy = 10;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.AddForce(transform.forward * shootForce);
    }

    // Update is called once per frame
    void Update()
    {
        timeToDestroy -= Time.deltaTime;
        if (timeToDestroy <= 0)
        {
            Destroy(gameObject);
        }
    }
}
