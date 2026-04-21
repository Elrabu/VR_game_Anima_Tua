using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Filtering;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class PattySocketFilter : MonoBehaviour, IXRSelectFilter
{
    [SerializeField] private XRSocketInteractor socket;

    public bool canProcess => isActiveAndEnabled;

    private void Awake()
    {
        socket.selectFilters.Add(this);
    }

    private void OnDestroy()
    {
        socket.selectFilters.Remove(this);
    }

    public bool Process(IXRSelectInteractor interactor, IXRSelectInteractable interactable)
    {
        if (interactable is not XRGrabInteractable grab)
            return false;

        Transform grilledChild = grab.transform.Find("patty_grilled");

        // Nur akzeptieren wenn patty_grilled aktiv ist
        if (grilledChild != null && grilledChild.gameObject.activeSelf)
            return true;

        Debug.Log("Socket rejected: Not a grilled patty.");
        return false;
    }
}