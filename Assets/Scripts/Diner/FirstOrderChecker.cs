using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class FirstOrderChecker : MonoBehaviour
{
    [Header("VFX")]
    [SerializeField] private ParticleSystem firstOrderSystem;
    [SerializeField] private GameObject placeIndicator;

    [Header("NextQuest")]
    [SerializeField] private GameObject currentTask;
    [SerializeField] private GameObject nextTask;
    [SerializeField] private GameObject nextTaskIndicator;
    [SerializeField] private AudioSource newOrder;

    [Header("Quest Requirements")]
    [SerializeField] private string requiredObjectNameFilter = "bottle";

    private DinerQuestController questController;

    private void Awake()
    {
        PlayFirstOrderSystem();
        placeIndicator.SetActive(true);
        nextTaskIndicator.SetActive(false);
        nextTask.SetActive(false);
        TryResolveQuestController();
    }

    void OnTriggerEnter(Collider collision)
    {
        Debug.Log("First order checker triggered");

        if (!collision.gameObject.name.Contains(requiredObjectNameFilter))
        {
            Debug.Log($"Object '{collision.gameObject.name}' does not contain '{requiredObjectNameFilter}'.");
            return;
        }

        StopFirstOrderSystem();
        placeIndicator.SetActive(false);
        nextTaskIndicator.SetActive(true);
        nextTask.SetActive(true);
        currentTask.SetActive(false);
        newOrder.Play();

        if (questController != null)
            questController.HandleFirstOrder();
    }

    private void TryResolveQuestController()
    {
        questController = FindAnyObjectByType<DinerQuestController>();
        if (questController == null)
            Debug.LogWarning("FirstOrderChecker: No DinerQuestController found.");
    }

    private void PlayFirstOrderSystem()
    {
        if (firstOrderSystem == null)
        {
            Debug.LogWarning("Reference is missing for particle system.");
            return;
        }

        firstOrderSystem.Play();
    }

    private void StopFirstOrderSystem()
    {
        if (firstOrderSystem == null)
            return;

        firstOrderSystem.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
    }
}
