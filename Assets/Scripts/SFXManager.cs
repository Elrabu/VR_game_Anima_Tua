
using UnityEngine;
using UnityEngine.Audio;

public class SFXManager : MonoBehaviour
{
    public static SFXManager instance;
    public AudioSource sfxObject; // Prefab with AudioSource

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    public void PlaySFXClip(AudioClip audioClip, AudioMixerGroup sfxMixerGroup, Vector3 spawnPosition, float volume)
    {
        AudioSource audioSource = Instantiate(sfxObject, spawnPosition, Quaternion.identity);
        audioSource.clip = audioClip;
        audioSource.volume = volume;
        audioSource.outputAudioMixerGroup = sfxMixerGroup;

        // Get the current measured room size from DynamicReverb.instance
        if (DynamicReverb.instance != null)
        {
            float avgDist = DynamicReverb.instance.avgDistance;
            AdjustReverb(audioSource, avgDist);
        }
        else
        {
            Debug.LogWarning("No DynamicReverb instance found – Hall effect skipped.");
        }

        audioSource.Play();
        Destroy(audioSource.gameObject, audioClip.length);
        Debug.Log("Playing SFX");
    }

    

    private void AdjustReverb(AudioSource source, float avgDist)
    {
        AudioReverbFilter filter = source.gameObject.AddComponent<AudioReverbFilter>();

        if (avgDist < 5f)       // Small room
        {
            filter.decayTime = 0.5f;
            filter.room = -200;
            Debug.Log("Playing audio with small room reverb");
        }
        else if (avgDist < 12f) // Medium room
        {
            filter.decayTime = 1.2f;
            filter.room = -100;
            Debug.Log("Playing audio with medium room reverb");
        }
        else                    // Large hall
        {
            filter.decayTime = 2.5f;
            filter.room = 0;
            Debug.Log("Playing audio with large room reverb");
        }
    }
}