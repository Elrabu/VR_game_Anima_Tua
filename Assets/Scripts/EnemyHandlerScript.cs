using UnityEngine;
using System.Collections;

public class EnemyHandlerScript : MonoBehaviour
{
    [SerializeField] private int Health;
    [SerializeField] private int Damage;
    [SerializeField] private Color flashColor = Color.white;
    [SerializeField] private float flashDuration = 0.5f;
    
    private Animator mAnimator;
    private Renderer rend;
    private Color originalColor;
    void Start()
    {
        rend = GetComponent<Renderer>();
        originalColor = rend.material.color;
    }
    
    void OnTriggerEnter(Collider collision)
    {
        Debug.Log("Interacted with: " + collision.gameObject.name);
        if (collision.gameObject.name == "Fire_Projectile(Clone)" )
        {
            takeDamage(1);
            playHitEffect();
        } 
    }

    void takeDamage(int amount)
    {
        Health -= amount;
    }

    public void playHitEffect()
    {
        StartCoroutine(FlashRoutine());
    }

    private IEnumerator FlashRoutine()
    {
        rend.material.color = flashColor;  
        yield return new WaitForSeconds(flashDuration);
        rend.material.color = originalColor;
    }
    void Update()
    {
        if (Health <= 0)
        {
            Destroy(gameObject);
        }
    }
}

