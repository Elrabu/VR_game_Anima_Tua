using UnityEngine;

public class BookAudioController : MonoBehaviour
{
    public AudioSource openAudio;
    public AudioSource closeAudio;

    public void PlayOpen()
    {
        if (openAudio != null)
            openAudio.Play();
    }

    public void PlayClose()
    {
        if (closeAudio != null)
            closeAudio.Play();
    }
}

