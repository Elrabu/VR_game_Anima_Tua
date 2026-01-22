using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AudioSource))]
public class BookImpactSound : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private AudioClip impactClip;

    [Header("Impact Settings")]
    [SerializeField] private float minImpactVelocity = 1.5f;
    [SerializeField] private float maxImpactVelocity = 6f;

    private AudioSource audioSource;
    private bool hasHitGround = false;

    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 1f;

        grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();

        grabInteractable.selectEntered.AddListener(OnGrabbed);
        grabInteractable.selectExited.AddListener(OnReleased);
    }

    private void OnGrabbed(SelectEnterEventArgs args)
    {
        // Buch wird aufgehoben → bereit für nächsten Drop
        hasHitGround = false;
    }

    private void OnReleased(SelectExitEventArgs args)
    {
        // nichts nötig, Physics übernimmt
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (hasHitGround)
            return;

        if (!collision.gameObject.CompareTag("Ground"))
            return;

        float impactStrength = collision.relativeVelocity.magnitude;
        if (impactStrength < minImpactVelocity)
            return;

        float volume = Mathf.InverseLerp(
            minImpactVelocity,
            maxImpactVelocity,
            impactStrength
        );

        audioSource.volume = volume;
        audioSource.PlayOneShot(impactClip);

        hasHitGround = true;
    }
}
