using UnityEngine;

public class WatcherAudioController : MonoBehaviour
{
    public AudioSource loopSource;
    public AudioSource sfxSource;

    public AudioClip[] floatingClips;
    public AudioClip attentionClip;
    public AudioClip attackClip;

    private bool isFloating = false;

    // Called by your StateMachineBehaviour when entering the Floating state
    public void StartFloating()
    {
        isFloating = true;
        PlayNextFloatingClip();
    }

    // Called by behaviour when leaving Floating state (enter Attention/Attack/etc.)
    public void StopFloating()
    {
        isFloating = false;
        loopSource.Stop();
    }

    private void PlayNextFloatingClip()
    {
        if (floatingClips == null || floatingClips.Length == 0) return;

        int index = Random.Range(0, floatingClips.Length);
        loopSource.clip = floatingClips[index];
        loopSource.Play();
    }

    private void Update()
    {
        // Every frame: if we are in floating mode and nothing is playing,
        // start the next random clip.
        if (isFloating && !loopSource.isPlaying)
        {
            PlayNextFloatingClip();
        }
    }

    public void PlayAttention()
    {
        StopFloating();
        if (attentionClip != null)
            sfxSource.PlayOneShot(attentionClip);
    }

    public void PlayAttack()
    {
        StopFloating();
        if (attackClip != null)
            sfxSource.PlayOneShot(attackClip);
    }
}