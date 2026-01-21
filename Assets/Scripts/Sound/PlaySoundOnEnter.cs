using UnityEngine;


public class PlaySoundOnEnter : StateMachineBehaviour
{
    public enum SoundType { None, Floating, Attention, Attack }
    public SoundType soundToPlay = SoundType.None;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var audio = animator.GetComponent<WatcherAudioController>();
        if (audio == null) return;

        switch (soundToPlay)
        {
            case SoundType.Floating:  audio.StartFloating(); break;
            case SoundType.Attention: audio.PlayAttention(); break;
            case SoundType.Attack:    audio.PlayAttack();    break;
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var audio = animator.GetComponent<WatcherAudioController>();
        if (audio == null) return;

        if (soundToPlay == SoundType.Floating)
            audio.StopFloating();
    }
}