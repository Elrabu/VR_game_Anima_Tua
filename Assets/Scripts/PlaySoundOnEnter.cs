using UnityEngine;

public class PlayWatcherSound : StateMachineBehaviour
{
    public enum SoundType { None, Floating, Attention, Attack }
    public SoundType soundToPlay = SoundType.None;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var audio = animator.GetComponent<WatcherAudioController>();
        if (audio == null) return;

        switch (soundToPlay)
        {
            case SoundType.Floating:
                audio.PlayFloatingLoop();
                break;

            case SoundType.Attention:
                audio.PlayAttention();
                break;

            case SoundType.Attack:
                audio.PlayAttack();
                break;
        }
    }
}