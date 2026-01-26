using UnityEngine;

public class JukeBoxPlayAudioSource : MonoBehaviour
{
    [SerializeField] private GameObject audiotrack;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void PlayMusic()
    {
        audiotrack.SetActive(true);
    }
}
