using UnityEngine;

public class LockGate : MonoBehaviour
{
    [SerializeField] private Transform gateTransform;
    [SerializeField] private float closeOffsetY = 1.5f;
    [SerializeField] private AudioSource gateAudioSource;

    private bool isClosed;

    private void OnTriggerEnter(Collider other)
    {
        if (isClosed)
        {
            return;
        }

        gateTransform.position -= new Vector3(0f, closeOffsetY, 0f);

        if (gateAudioSource != null)
        {
            gateAudioSource.Play();
        }

        isClosed = true;
    }
}
