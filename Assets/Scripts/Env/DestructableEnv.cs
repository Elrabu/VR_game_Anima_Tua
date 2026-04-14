using UnityEngine;

public class DestructableEnv : MonoBehaviour
{
    [SerializeField] private int Health = 1;
    [SerializeField] private GameObject fracturedPrefab;
    [SerializeField] private GameObject destroyParticlePrefab;

    public int health
    {
        get => Health;
        private set => Health = value;
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name == "Fire_Projectile(Clone)")
        {
            TakeDamage(1);
        }
    }

    public void TakeDamage(int amount)
    {
        Health -= amount;

        if (Health <= 0)
        {
            DestroyEnvironment();
        }
    }

    private void DestroyEnvironment()
    {
        if (destroyParticlePrefab != null)
        {
            Instantiate(destroyParticlePrefab, transform.position, transform.rotation);
        }

        if (fracturedPrefab != null)
        {
            Instantiate(fracturedPrefab, transform.position, transform.rotation);
        }

        Destroy(gameObject);
    }
}
