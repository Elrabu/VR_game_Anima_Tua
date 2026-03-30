using System.Collections;
using UnityEngine;

public class LightConeFlicker : MonoBehaviour
{
    [SerializeField] [Range(0.05f, 0.5f)] private float flickerInterval = 0.1f;
    [SerializeField] private GameObject[] lightCones;

    private bool isFlickering = false;

    public void StartFlicker(float duration)
    {
        if (isFlickering)
            return;

        if (lightCones == null || lightCones.Length == 0)
        {
            Debug.LogWarning("LightConeFlicker: No light cones assigned in the inspector.");
            return;
        }

        StartCoroutine(FlickerCoroutine(duration));
    }

    public void StopFlicker()
    {
        isFlickering = false;
        RestoreLightCones();
    }

    private IEnumerator FlickerCoroutine(float duration)
    {
        isFlickering = true;
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            for (int i = 0; i < lightCones.Length; i++)
            {
                if (lightCones[i] != null)
                {
                    lightCones[i].SetActive(Random.value > 0.5f);
                }
            }

            yield return new WaitForSeconds(flickerInterval);
        }

        RestoreLightCones();
        isFlickering = false;
    }

    private void RestoreLightCones()
    {
        for (int i = 0; i < lightCones.Length; i++)
        {
            if (lightCones[i] != null)
                lightCones[i].SetActive(true);
        }
    }
}
