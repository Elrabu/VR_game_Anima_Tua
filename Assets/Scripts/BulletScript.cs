using UnityEngine;
using UnityEngine.Audio;


public class BulletScript : MonoBehaviour
{  
    private float lifetime;
    new Rigidbody rigidbody; //use new keyword because Unity thinks hiding was intended

    protected GameObject spawnedParticle;
    protected Transform spawnPoint;
    
    [SerializeField] private AudioClip shootClip;

    [SerializeField] private AudioClip impactClip;
    [SerializeField] private AudioMixerGroup sfxMixer;



    public void setUpBullet(float shootForce, float timeToDestroy, GameObject spawned, Transform point)
    {
        rigidbody = GetComponent<Rigidbody>();
        GetComponent<Rigidbody>().AddForce(transform.forward * shootForce);

        lifetime = timeToDestroy;
        spawnedParticle = spawned;
        spawnPoint = point;
        PlayShootSound();
    }

    void Update()
    {
        lifetime -= Time.deltaTime;
        if (lifetime <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider collision)
    {   
        AudioSourceAtPoint audio = new AudioSourceAtPoint();
        audio.PlayClipAtPointWithFullControl(
            impactClip,
            transform.position,
            sfxMixer,
            1f
        );
        HandleCollision(collision);
        spawnParticleSystem(spawnedParticle, spawnPoint);
    }

    public virtual void HandleCollision(Collider collision) //base class method that can be overwritten
    {
        //Debug.Log("Collided with " + collision.gameObject.name);

        ColorChanger changer = collision.gameObject.GetComponent<ColorChanger>();

        if (changer != null)
        {
            changer.ChangeColor();   // change object color
        }

        Destroy(gameObject);
    }

    void spawnParticleSystem(GameObject spawnedParticle, Transform spawnPoint)
    {   
        
        GameObject particle = Instantiate(spawnedParticle, spawnPoint.position, spawnPoint.rotation);
        particle.GetComponent<ParticleSystem>().Play();
        Destroy(particle, 0.5f);
    }
    

    void PlayShootSound()
    {
        AudioSource src = gameObject.AddComponent<AudioSource>();
        src.clip = shootClip;
        src.outputAudioMixerGroup = sfxMixer;
        src.spatialBlend = 1f;               // 3D Sound
        src.rolloffMode = AudioRolloffMode.Logarithmic;
        src.minDistance = 0.3f;
        src.maxDistance = 15f;
        src.volume = 1f;
        src.playOnAwake = false;

        src.Play();
    }

}
