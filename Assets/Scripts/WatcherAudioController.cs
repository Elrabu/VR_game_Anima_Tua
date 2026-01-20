using UnityEngine;

public class WatcherAudioController : MonoBehaviour
{
    public AudioSource loopSource;
    public AudioSource sfxSource;

    public AudioClip[] floatingClips;
    public AudioClip attentionClip;
    public AudioClip attackClip;
    private bool isFloating = false;


    public void PlayFloatingLoop()
    {   
        isFloating = true;
        while(isFloating){
        int index = Random.Range(0, floatingClips.Length);
        loopSource.clip = floatingClips[index];
        loopSource.Play();
        }
    }

    public void PlayAttention()
    {   
        isFloating = false;
        if (attentionClip != null)
            sfxSource.PlayOneShot(attentionClip);
    }

    public void PlayAttack()
    {   
        isFloating = false;
        if (attackClip != null)
            sfxSource.PlayOneShot(attackClip);
    }
}