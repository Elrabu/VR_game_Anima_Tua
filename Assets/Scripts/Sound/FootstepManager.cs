using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class FootstepManager : MonoBehaviour
{
    [Header("Surfaces")]
    public FootstepSurface defaultSurface;
    public FootstepSurface[] surfaces;

    [Header("Raycast")]
    public Transform footOrigin;
    public float rayDistance = 1.5f;

    [Header("Step Distance")]
    public float stepDistance = 0.4f;   // Meter pro Schritt
    public bool requireGrounded = true;

    public CharacterController characterController;

    private AudioSource source;
    private Vector3 lastStepPosition;

    void Awake()
    {
        source = GetComponent<AudioSource>();
        if (footOrigin == null) footOrigin = transform;
        if (characterController == null)
            characterController = GetComponent<CharacterController>();
    }

    void Start()
    {
        lastStepPosition = transform.position;
    }

    void Update()
    {
        HandleDistanceBasedFootsteps();
    }

    void HandleDistanceBasedFootsteps()
    {
        if (requireGrounded && characterController != null && !characterController.isGrounded)
            return;

        // horizontale Distanz seit letztem Schritt
        Vector3 pos = transform.position;
        pos.y = 0;
        Vector3 last = lastStepPosition;
        last.y = 0;

        float dist = Vector3.Distance(pos, last);
        if (dist >= stepDistance)
        {
            PlayFootstep();
            lastStepPosition = transform.position;
        }
    }

    FootstepSurface GetSurfaceUnderFoot()
    {
        if (Physics.Raycast(footOrigin.position, Vector3.down,
                            out RaycastHit hit, rayDistance))
        {
            int hitLayer = hit.collider.gameObject.layer;
            foreach (var s in surfaces)
            {
                if (((1 << hitLayer) & s.layerMask) != 0)
                    return s;
            }
        }
        return defaultSurface;
    }

    public void PlayFootstep()
    {
        FootstepSurface surface = GetSurfaceUnderFoot();
        if (surface == null || surface.footstepClips.Length == 0) return;

        AudioClip clip = surface.footstepClips[
            Random.Range(0, surface.footstepClips.Length)];

        source.pitch  = Random.Range(surface.pitchRange.x,  surface.pitchRange.y);
        source.volume = Random.Range(surface.volumeRange.x, surface.volumeRange.y);

        source.PlayOneShot(clip);
    }
}