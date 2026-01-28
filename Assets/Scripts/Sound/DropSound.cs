using UnityEngine;

public class DropSound : MonoBehaviour
{
    [Header("Audio")]
    public AudioClip dropClip;

    [Range(0f, 1f)]
    public float volume = 1f;

    private void OnCollisionEnter(Collision collision)
    {
        if (dropClip == null) return;

        // Kollisionspunkt ermitteln
        Vector3 spawnPosition = collision.contacts[0].point;

        // Temporäre AudioSource erzeugen
        AudioSource tempSource = new GameObject("DropSound").AddComponent<AudioSource>();
        tempSource.transform.position = spawnPosition;
        tempSource.clip = dropClip;
        tempSource.volume = volume;
        tempSource.spatialBlend = 1f; // 3D Sound
        tempSource.Play();

        // GameObject nach Abspielen zerstören
        Destroy(tempSource.gameObject, dropClip.length);
    }
}
