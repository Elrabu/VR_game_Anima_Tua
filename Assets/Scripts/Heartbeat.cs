using UnityEngine;

public class Heartbeat : MonoBehaviour
{
    public AudioSource heartbeatSource;
    public AudioSource secondHitSource;

    public float bpm = 80f;
    public float secondHitOffset = 0.1f;
    public float activeDuration = 5.0f; // ❤️ wie lange nach Schaden

    private bool heartbeatActive = false;
    private double secondsPerBeat;
    private double nextBeatTime;
    private float stopTimer = 0f;

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

        heartbeatActive = true;
        stopTimer = activeDuration;
        nextBeatTime = AudioSettings.dspTime;
    }

    public void StopHeartbeat()
    {
        heartbeatActive = false;
        heartbeatSource.Stop();
        secondHitSource.Stop();
    }
}
