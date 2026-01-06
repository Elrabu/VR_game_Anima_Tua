using UnityEngine;

[CreateAssetMenu(menuName = "Audio/Footstep Surface")]
public class FootstepSurface : ScriptableObject
{
    public string surfaceName;                 // z.B. "Stone"
    public LayerMask layerMask;                // oder Tag, siehe unten
    public AudioClip[] footstepClips;

    [Header("Randomization")]
    public Vector2 volumeRange = new Vector2(0.8f, 1.0f);
    public Vector2 pitchRange  = new Vector2(0.9f, 1.1f);
}