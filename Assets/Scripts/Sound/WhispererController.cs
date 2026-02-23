using UnityEngine;
using System.Collections.Generic;

public class WhispererController : MonoBehaviour
{
    public Transform playerHead;

    public float closeRange = 1.5f;
    public float mediumRange = 4f;

    public float globalCooldown = 6f;

    private float lastWhisperTime;

    private AudioSource audioSource;

    [Header("Audio Pools")]
    public List<AudioClip> closeLines;
    public List<AudioClip> behindLines;
    public List<AudioClip> sideLines;
    public List<AudioClip> generalLines;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Time.time < lastWhisperTime + globalCooldown)
            return;

        Vector3 dir = transform.position - playerHead.position;
        float dist = dir.magnitude;
        Vector3 dirNorm = dir.normalized;

        float forwardDot = Vector3.Dot(playerHead.forward, dirNorm);
        float rightDot = Vector3.Dot(playerHead.right, dirNorm);

        EvaluateWhisper(dist, forwardDot, rightDot);
    }

        void EvaluateWhisper(float dist, float forwardDot, float rightDot)
    {
        List<AudioClip> selectedPool = null;

        if (dist < closeRange)
        {
            selectedPool = closeLines;
        }
        else if (forwardDot < -0.6f)
        {
            selectedPool = behindLines;
        }
        else if (Mathf.Abs(forwardDot) < 0.3f)
        {
            selectedPool = sideLines;
        }
        else
        {
            selectedPool = generalLines;
        }

        if (selectedPool != null && selectedPool.Count > 0)
        {
            PlayRandom(selectedPool);
        }
    }

        void PlayRandom(List<AudioClip> pool)
    {
        if (audioSource.isPlaying)
            return;

        int index = Random.Range(0, pool.Count);
        audioSource.clip = pool[index];
        audioSource.Play();

        lastWhisperTime = Time.time;
    }
}