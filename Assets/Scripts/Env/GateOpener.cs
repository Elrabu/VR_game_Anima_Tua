using UnityEngine;

public class GateOpener : MonoBehaviour
{
    [SerializeField] private Transform gateTransform;
    [SerializeField] private float openOffsetY = 1.5f;
    [SerializeField] private AudioSource gateAudioSource;

    private bool isOpen;

    public void OpenGate()
    {
        if (isOpen)
        {
            return;
        }

        gateTransform.position += new Vector3(0f, openOffsetY, 0f);

        if (gateAudioSource != null)
        {
            gateAudioSource.Play();
        }

        isOpen = true;
    }
}
