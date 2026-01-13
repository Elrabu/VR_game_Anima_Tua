using UnityEngine;

public class BookAudioController : MonoBehaviour
{
    public AudioSource openAudio;
    public AudioSource closeAudio;

    public void PlayOpen()
    {
        if (openAudio == null) return;
    
        openAudio.pitch  = Random.Range(0.95f, 1.08f);
        openAudio.volume = Random.Range(0.7f, 1.0f);
    
        openAudio.Play();
    }
    

    public void PlayClose()
    {
        if (closeAudio == null) return;

        closeAudio.pitch  = Random.Range(0.95f, 1.05f);
        closeAudio.volume = Random.Range(0.8f, 1.0f);

        closeAudio.Play();
    }

}

