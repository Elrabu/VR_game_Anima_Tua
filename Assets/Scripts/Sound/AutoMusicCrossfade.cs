using System.Collections;
using UnityEngine;

public class AutoMusicCrossfade : MonoBehaviour
{   
    public static AutoMusicCrossfade instance { get; private set;  }
    [SerializeField] private AudioSource[] sources; // Größe = 4
    [SerializeField] private float fadeTime = 2f;
    [SerializeField] private float holdTime = 5f;   // Zeit, die ein Track voll hörbar bleibt

    int currentTrack = 0;
    Coroutine fadeCoroutine;

    void Awake()
    {
        instance = this;
    }

    public void StartMusic()
    {
        StartAllSynced();
        StartCoroutine(AutoFadeLoop());
    }

    void StartAllSynced()
    {
        double startTime = AudioSettings.dspTime + 0.1;

        foreach (var src in sources)
        {
            src.volume = 0f;
            src.loop = true;
            src.PlayScheduled(startTime);
        }

        sources[0].volume = 1f;
    }

    IEnumerator AutoFadeLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(holdTime);

            int nextTrack = (currentTrack + 1) % sources.Length;
            yield return StartCoroutine(FadeRoutine(currentTrack, nextTrack));

            currentTrack = nextTrack;
        }
    }

    IEnumerator FadeRoutine(int from, int to)
    {
        float t = 0f;

        float startFrom = sources[from].volume;
        float startTo = sources[to].volume;

        while (t < fadeTime)
        {
            t += Time.deltaTime;
            float k = t / fadeTime;

            sources[from].volume = Mathf.Lerp(startFrom, 0f, k);
            sources[to].volume = Mathf.Lerp(startTo, 1f, k);

            yield return null;
        }

        sources[from].volume = 0f;
        sources[to].volume = 1f;
    }
}