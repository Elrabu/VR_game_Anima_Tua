using UnityEngine;
using UnityEngine.Audio;

public class EchoDistanceControl : MonoBehaviour
{
    public Transform player;
    public AudioMixer mixer;

    // Exposed Parameter Name = genau wie im Mixer: "Reverb"
    public string reverbParam = "Reverb";
    public float maxDistance = 40f;

    void Update()
    {
        float dist = Vector3.Distance(player.position, transform.position);
        float t = Mathf.Clamp01(dist / maxDistance);

        // nahe = wenig Reverb, weit = viel Reverb
        float reverbValue = Mathf.Lerp(-30f, 20f, t);        
        mixer.SetFloat(reverbParam, reverbValue);
    }
}