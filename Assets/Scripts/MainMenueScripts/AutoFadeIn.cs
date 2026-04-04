using UnityEngine;
using System.Collections;

public class AutoFadeIn : MonoBehaviour
{
    [SerializeField] private float prewarmDelay = 1.0f; // Seconds to wait for GPU
    [SerializeField] private ScreenFader fader;

    void Awake()
    {
        fader = GetComponent<ScreenFader>();
        if (fader != null) fader.SetImmediateBlack(); // Force black on frame 1
    }

    IEnumerator Start()
    {
        if (fader == null) yield break;

        // The screen stays black while Unity finishes "waking up" the shaders
        yield return new WaitForSeconds(prewarmDelay);

        fader.FadeToClear();
    }
}