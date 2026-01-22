using UnityEngine;
using UnityEngine.Audio;

public class AudioSourceAtPoint : MonoBehaviour
{
    public void PlayClipAtPointWithFullControl(
        AudioClip clip,
        Vector3 position,
        AudioMixerGroup mixerGroup,
        float volume = 1f)
    {
        if (clip == null) return;

        // GameObject erzeugen
        GameObject go = new GameObject("OneShotAudio");
        go.transform.position = position;

        // AudioSource hinzufügen
        AudioSource src = go.AddComponent<AudioSource>();
        src.clip = clip;
        src.volume = volume;
        src.spatialBlend = 1f;           // 3D
        src.rolloffMode = AudioRolloffMode.Logarithmic;
        src.outputAudioMixerGroup = mixerGroup;
        src.playOnAwake = false;
        src.minDistance = 0.5f;
        src.maxDistance = 20f;

        src.Play();

        // Aufräumen nach Clip-Ende
        Destroy(go, clip.length);
    }
}
