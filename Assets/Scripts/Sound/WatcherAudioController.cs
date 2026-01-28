using UnityEngine;
using System.Collections;

public class WatcherAudioController : MonoBehaviour
{
    public AudioSource loopSource;
    public AudioSource sfxSource;

    public AudioClip[] floatingClips;
    public AudioClip attentionClip;
    public AudioClip attackClip;

    [SerializeField] private float clipDistance = 0f;

    private Coroutine floatingRoutine;

    // Called when entering Floating state
    public void StartFloating()
    {
        StopFloating();
        floatingRoutine = StartCoroutine(FloatingLoop());
    }

    // Called when leaving Floating state
    public void StopFloating()
    {
        if (floatingRoutine != null)
        {
            StopCoroutine(floatingRoutine);
            floatingRoutine = null;
        }

        loopSource.Stop();
    }

    private IEnumerator FloatingLoop()
    {
        while (true)
        {
            if (floatingClips == null || floatingClips.Length == 0)
                yield break;

            // Pick random clip
            AudioClip clip = floatingClips[Random.Range(0, floatingClips.Length)];
            loopSource.clip = clip;
            loopSource.Play();

            // Wait until clip is finished
            yield return new WaitForSeconds(clip.length);

            // Wait additional distance (if any)
            if (clipDistance > 0f)
                yield return new WaitForSeconds(clipDistance);
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
