using System.Collections;
using UnityEngine;

public class SwapParticleSystems : MonoBehaviour
{
    [Header("Trigger")]
    [SerializeField] private string requiredTag = "Player";
    [SerializeField] private bool disableTriggerAfterUse = true;

    [Header("Previous section (unload)")]
    [SerializeField] private ParticleSystem[] previousSectionSystems;
    [SerializeField] private bool destroyPreviousSystems = false;

    [Header("Next section (load)")]
    [SerializeField] private ParticleSystem[] nextSectionSystems;
    [SerializeField] [Min(1)] private int systemsPerFrame = 2;

    private bool hasSwapped;

    private void OnTriggerEnter(Collider other)
    {
        if (hasSwapped)
        {
            return;
        }

        if (!string.IsNullOrEmpty(requiredTag) && !other.CompareTag(requiredTag))
        {
            return;
        }

        hasSwapped = true;

        UnloadPreviousSection();
        StartCoroutine(LoadNextSectionGradually());

        if (disableTriggerAfterUse)
        {
            var ownCollider = GetComponent<Collider>();
            if (ownCollider != null)
            {
                ownCollider.enabled = false;
            }
        }
    }

    private void UnloadPreviousSection()
    {
        if (previousSectionSystems == null)
        {
            return;
        }

        for (int i = 0; i < previousSectionSystems.Length; i++)
        {
            ParticleSystem system = previousSectionSystems[i];
            if (system == null)
            {
                continue;
            }

            system.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);

            GameObject systemObject = system.gameObject;
            if (destroyPreviousSystems)
            {
                Destroy(systemObject);
            }
            else
            {
                systemObject.SetActive(false);
            }
        }
    }

    private IEnumerator LoadNextSectionGradually()
    {
        if (nextSectionSystems == null || nextSectionSystems.Length == 0)
        {
            yield break;
        }

        int activatedThisFrame = 0;

        for (int i = 0; i < nextSectionSystems.Length; i++)
        {
            ParticleSystem system = nextSectionSystems[i];
            if (system == null)
            {
                continue;
            }

            GameObject systemObject = system.gameObject;
            if (!systemObject.activeSelf)
            {
                systemObject.SetActive(true);
            }

            system.Play(true);

            activatedThisFrame++;
            if (activatedThisFrame >= systemsPerFrame)
            {
                activatedThisFrame = 0;
                yield return null;
            }
        }
    }
}
