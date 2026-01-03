using UnityEngine;

public class DoorAudioController : MonoBehaviour
{
    [Header("Audio")]
    public AudioSource creakAudio;
    public AudioSource latchAudio;

    [Header("Movement")]
    public float movementThreshold = 0.5f;
    public float volumeMultiplier = 0.05f;

    [Header("Latch")]
    public float closeAngle = 2f;
    public float latchCooldown = 0.3f;

    private Quaternion lastRotation;
    private bool latchPlayed;
    private float lastLatchTime;

    void Start()
    {
        lastRotation = transform.rotation;
    }

    void Update()
    {
        float angleDelta = Quaternion.Angle(lastRotation, transform.rotation);
        float currentAngle = GetLocalYaw();

        // 🔊 CREAK (movement based)
        if (angleDelta > movementThreshold)
        {
            if (!creakAudio.isPlaying)
                creakAudio.Play();

            creakAudio.volume = Mathf.Clamp(angleDelta * volumeMultiplier, 0.05f, 0.4f);
            creakAudio.pitch = Random.Range(0.95f, 1.05f);
        }
        else
        {
            creakAudio.volume = Mathf.Lerp(creakAudio.volume, 0f, Time.deltaTime * 8f);
            if (creakAudio.volume < 0.01f)
                creakAudio.Stop();
        }

        // 🔒 LATCH
        if (currentAngle <= closeAngle && !latchPlayed && Time.time - lastLatchTime > latchCooldown)
        {
            latchAudio.pitch = Random.Range(0.98f, 1.02f);
            latchAudio.Play();
            latchPlayed = true;
            lastLatchTime = Time.time;
        }

        if (currentAngle > closeAngle + 1f)
            latchPlayed = false;

        lastRotation = transform.rotation;
    }

    float GetLocalYaw()
    {
        float y = transform.localEulerAngles.y;
        return y > 180 ? y - 360 : y;
    }

    public void ResetOpenSound()
    {
    if (creakAudio.isPlaying)
        creakAudio.Stop();
    }
}
