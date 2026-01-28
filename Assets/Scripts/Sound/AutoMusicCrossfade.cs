using UnityEngine;

public class AutoMusicCrossfade : MonoBehaviour
{
    public static AutoMusicCrossfade instance { get; private set; }

    [SerializeField] private AudioSource[] sources; // z.B. Größe = 4
    [SerializeField] private float fadeTime = 2f;
    [SerializeField] private float holdTime = 5f;

    int currentTrack = 0;
    int nextTrack = 0;

    double fadeStartDSP;
    double nextFadeDSP;
    bool isFading = false;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    public void StartMusic()
    {
        StartAllSynced();
        nextFadeDSP = AudioSettings.dspTime + holdTime;
    }

    void StartAllSynced()
    {
        double startTime = AudioSettings.dspTime + 0.1;

        for (int i = 0; i < sources.Length; i++)
        {
            sources[i].volume = 0f;
            sources[i].loop = true;
            sources[i].PlayScheduled(startTime);
        }

        sources[0].volume = 1f;
        currentTrack = 0;
    }

    void Update()
    {
        double dspTime = AudioSettings.dspTime;

        // Starte neuen Fade nach Hold-Zeit
        if (!isFading && dspTime >= nextFadeDSP)
        {
            nextTrack = (currentTrack + 1) % sources.Length;
            fadeStartDSP = dspTime;
            isFading = true;
        }

        // Fading
        if (isFading)
        {
            float k = (float)((dspTime - fadeStartDSP) / fadeTime);
            k = Mathf.Clamp01(k);

            // Equal-Power-Crossfade
            float fadeOut = Mathf.Cos(k * Mathf.PI * 0.5f);
            float fadeIn  = Mathf.Sin(k * Mathf.PI * 0.5f);

            sources[currentTrack].volume = fadeOut;
            sources[nextTrack].volume    = fadeIn;

            if (k >= 1f)
            {
                sources[currentTrack].volume = 0f;
                sources[nextTrack].volume = 1f;

                currentTrack = nextTrack;
                isFading = false;
                nextFadeDSP = dspTime + holdTime;
            }
        }
    }
}
