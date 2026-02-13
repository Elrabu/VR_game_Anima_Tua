using UnityEngine;
using System.Collections;

public class EnemyHandlerScript : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private int Health;
    [SerializeField] private int Damage;
    [SerializeField] private Color flashColor = Color.white;
    [SerializeField] private float flashDuration = 0.5f;

    [Header("Audio")]
    [SerializeField] private AudioSource damageDeathAudioSource;
    [SerializeField] private AudioClip damageClip;
    [SerializeField] private AudioClip deathClip;
    
    
    private Animator mAnimator;
    private Renderer rend;
    private Color originalColor;


    public int health                      // Property, Zugriff von außen
    {
        get => Health;
        private set => Health = value;
    }

    void Start()
    {
        rend = GetComponent<Renderer>();
        originalColor = rend.material.color;
    }
    
    void OnTriggerEnter(Collider collision)
    {
        //Debug.Log("Interacted with: " + collision.gameObject.name);
        if (collision.gameObject.name == "Fire_Projectile(Clone)" )
        {   
            PlaySoundEffect(damageClip);
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

    private void PlaySoundEffect(AudioClip audioClip)
    {   
        AudioSource.PlayClipAtPoint(audioClip, transform.position, 1f);
    }

    void Update()
    {
        if (Health <= 0)
        {   
            PlaySoundEffect(deathClip);
            Destroy(gameObject);
        }
    }
}

