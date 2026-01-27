using UnityEngine;

public class Heartbeat : MonoBehaviour
{
    public AudioSource heartbeatSource;
    public AudioSource secondHitSource;
    public AudioSource tinitus;

    public float bpm = 80f;
    public float secondHitOffset = 0.1f;
    public float activeDuration = 5.0f;

    private bool heartbeatActive = false;
    private double secondsPerBeat;
    private double nextBeatTime;
    private float stopTimer = 0f;
    private static Heartbeat instance;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Debug.Log("[HeartBeat] Duplicate instance, destroying " + gameObject.name);
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
        Debug.Log("[HeartBeat] Awake, id = " + GetInstanceID());
    }

    void Start()
    {
        secondsPerBeat = 60.0 / bpm;
    }

    void Update()
    {
        if (!heartbeatActive)
            return;

        stopTimer -= Time.deltaTime;
        if (stopTimer <= 0f)
        {
            StopHeartbeat();
            return;
        }

        if (AudioSettings.dspTime >= nextBeatTime)
        {
            PlayHeartbeat();
            nextBeatTime += secondsPerBeat;
        }
    }

    void PlayHeartbeat()
    {
        heartbeatSource.Play();
        secondHitSource.PlayScheduled(AudioSettings.dspTime + secondHitOffset);
    }

    public void TriggerHeartbeat(float newBpm)
{
    bpm = newBpm;
    secondsPerBeat = 60.0 / bpm;

    // If already active, just extend timer and update BPM;
    // don't reset the beat phase.
    if (!heartbeatActive)
    {
        heartbeatActive = true;
        nextBeatTime = AudioSettings.dspTime;
    }

    stopTimer = activeDuration;  // always extend to full duration from last hit
}

    public void StopHeartbeat()
    {
        heartbeatActive = false;
        heartbeatSource.Stop();
        secondHitSource.Stop();
    }

    public void PlayTinitus()
    {
        tinitus.Play();
    }
}
