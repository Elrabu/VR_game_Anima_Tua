using UnityEngine;

public class DoorAudioController : MonoBehaviour
{
    [Header("Audio")]
    public AudioSource openAudio;
    public AudioSource latchAudio;

    [Header("Open Settings")]
    public float openMovementThreshold = 2f;

    [Header("Close Settings")]
    public float closeAngle = 2f; // degrees from closed
    public float latchCooldown = 0.3f;

    private Quaternion lastRotation;
    private bool openPlayed = false;
    private bool latchPlayed = false;
    private float lastLatchTime;

    void Start()
    {
        lastRotation = transform.rotation;
    }

    void Update()
    {
        float angleDelta = Quaternion.Angle(lastRotation, transform.rotation);
        float currentAngle = GetLocalYaw();

        // OPEN SOUND (movement-based)
        if (angleDelta > openMovementThreshold && !openPlayed)
        {
            openAudio.pitch = Random.Range(0.95f, 1.05f);
            openAudio.Play();
            openPlayed = true;
        }

        // LATCH CLICK (close angle-based)
        if (currentAngle <= closeAngle && !latchPlayed && Time.time - lastLatchTime > latchCooldown)
        {
            latchAudio.pitch = Random.Range(0.98f, 1.02f);
            latchAudio.Play();

            latchPlayed = true;
            lastLatchTime = Time.time;
        }

        // Reset latch when door opens again
        if (currentAngle > closeAngle + 1f)
        {
            latchPlayed = false;
        }

        lastRotation = transform.rotation;
    }

    public void ResetOpenSound()
    {
        openPlayed = false;
    }

    float GetLocalYaw()
    {
        float y = transform.localEulerAngles.y;
        return (y > 180f) ? y - 360f : y; // normalize
    }
}
