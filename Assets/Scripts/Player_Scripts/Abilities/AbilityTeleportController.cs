using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Teleportation;

public class AbilityTeleportController : MonoBehaviour
{
    [SerializeField] private AbilityInventory abilityInventory;
    [SerializeField] private AbilityType teleportAbility = AbilityType.Teleport;

    [Header("Teleport Interactors")]
    [SerializeField] private GameObject rightTeleportInteractorObject;
    [SerializeField] private GameObject leftTeleportInteractorObject;
    [SerializeField] private XRRayInteractor rightTeleportRayInteractor;
    [SerializeField] private XRRayInteractor leftTeleportRayInteractor;

    [Header("Teleport Confirm Fallback")]
    [SerializeField] private bool enableManualTriggerConfirm = true;
    [SerializeField] private InputActionProperty rightConfirmAction;
    [SerializeField] private InputActionProperty leftConfirmAction;
    [SerializeField] private float confirmPressedThreshold = 0.5f;
    [SerializeField] private TeleportationProvider teleportationProvider;

    [Header("Teleport Visual Indicator")]
    [SerializeField] private GameObject teleportIndicatorPrefab;
    [SerializeField] private Transform rightHandAnchor;
    [SerializeField] private Transform leftHandAnchor;
    [SerializeField] private bool equipInRightHand = true;

    [Header("Indicator Local Transform")]
    [SerializeField] private Vector3 indicatorLocalPosition;
    [SerializeField] private Vector3 indicatorLocalRotationEuler;
    [SerializeField] private Vector3 indicatorLocalScale = Vector3.one;

    [Header("Indicator FX")]
    [SerializeField] private bool autoAddPulseEffect = true;
    [SerializeField] private float pulseSpeed = 3.5f;
    [SerializeField] private float pulseAmount = 0.12f;
    [SerializeField] private float hoverSpeed = 2.5f;
    [SerializeField] private float hoverAmount = 0.01f;
    [SerializeField] private float rotationSpeed = 80f;
    [SerializeField] private Color emissionColor = new Color(0.2f, 1f, 0.95f);
    [SerializeField] private float minEmission = 0.2f;
    [SerializeField] private float maxEmission = 1.5f;

    [SerializeField] private bool verboseDebug = false;

    private GameObject equippedIndicator;
    private bool wasRightConfirmPressed;
    private bool wasLeftConfirmPressed;

    public void SetPreferredHand(bool preferRightHand)
    {
        equipInRightHand = preferRightHand;

        if (abilityInventory != null && abilityInventory.CurrentAbility == teleportAbility)
        {
            EquipIndicator();
        }
    }

    private void Reset()
    {
        if (abilityInventory == null)
        {
            abilityInventory = GetComponent<AbilityInventory>();
        }

        if (rightTeleportInteractorObject != null && rightTeleportRayInteractor == null)
        {
            rightTeleportRayInteractor = rightTeleportInteractorObject.GetComponentInChildren<XRRayInteractor>(true);
        }

        if (leftTeleportInteractorObject != null && leftTeleportRayInteractor == null)
        {
            leftTeleportRayInteractor = leftTeleportInteractorObject.GetComponentInChildren<XRRayInteractor>(true);
        }

        if (teleportationProvider == null)
        {
            teleportationProvider = FindObjectOfType<TeleportationProvider>();
        }
    }

    private void OnEnable()
    {
        if (enableManualTriggerConfirm)
        {
            TryEnableAction(rightConfirmAction);
            TryEnableAction(leftConfirmAction);
        }

        if (abilityInventory != null)
        {
            abilityInventory.OnCurrentAbilityChanged += OnCurrentAbilityChanged;
            ApplyForAbility(abilityInventory.CurrentAbility);
        }
        else
        {
            ApplyForAbility(AbilityType.None);
        }
    }

    private void OnDisable()
    {
        if (enableManualTriggerConfirm)
        {
            TryDisableAction(rightConfirmAction);
            TryDisableAction(leftConfirmAction);
        }

        wasRightConfirmPressed = false;
        wasLeftConfirmPressed = false;

        if (abilityInventory != null)
        {
            abilityInventory.OnCurrentAbilityChanged -= OnCurrentAbilityChanged;
        }

        SetTeleportState(false);
        UnequipIndicator();
    }

    private void Update()
    {
        if (!enableManualTriggerConfirm || abilityInventory == null)
        {
            return;
        }

        if (abilityInventory.CurrentAbility != teleportAbility)
        {
            wasRightConfirmPressed = false;
            wasLeftConfirmPressed = false;
            return;
        }

        UpdateConfirmHand(
            rightConfirmAction,
            ref wasRightConfirmPressed,
            rightTeleportRayInteractor,
            rightTeleportInteractorObject,
            "right");

        UpdateConfirmHand(
            leftConfirmAction,
            ref wasLeftConfirmPressed,
            leftTeleportRayInteractor,
            leftTeleportInteractorObject,
            "left");
    }

    private void OnCurrentAbilityChanged(AbilityType ability)
    {
        ApplyForAbility(ability);
    }

    private void ApplyForAbility(AbilityType ability)
    {
        bool shouldEnableTeleport = ability == teleportAbility;
        SetTeleportState(shouldEnableTeleport);

        if (shouldEnableTeleport)
        {
            EquipIndicator();
        }
        else
        {
            UnequipIndicator();
        }

        if (verboseDebug)
        {
            Debug.Log($"AbilityTeleportController -> ability={ability}, teleportEnabled={shouldEnableTeleport}");
        }
    }

    private void SetTeleportState(bool active)
    {
        if (rightTeleportInteractorObject != null)
        {
            rightTeleportInteractorObject.SetActive(active);
        }

        if (leftTeleportInteractorObject != null)
        {
            leftTeleportInteractorObject.SetActive(active);
        }
    }

    private void EquipIndicator()
    {
        if (teleportIndicatorPrefab == null)
        {
            return;
        }

        Transform targetHand = equipInRightHand ? rightHandAnchor : leftHandAnchor;
        if (targetHand == null)
        {
            if (verboseDebug)
            {
                Debug.LogWarning($"AbilityTeleportController: target hand anchor missing on {name}.");
            }
            return;
        }

        if (equippedIndicator == null)
        {
            equippedIndicator = Instantiate(teleportIndicatorPrefab, targetHand);
            equippedIndicator.name = "TeleportIndicator";
        }
        else
        {
            equippedIndicator.transform.SetParent(targetHand, false);
        }

        ApplyIndicatorLocalTransform(equippedIndicator.transform);
        ConfigureIndicatorFx(equippedIndicator);
    }

    private void UnequipIndicator()
    {
        if (equippedIndicator == null)
        {
            return;
        }

        Destroy(equippedIndicator);
        equippedIndicator = null;
    }

    private void ApplyIndicatorLocalTransform(Transform indicator)
    {
        indicator.localPosition = indicatorLocalPosition;
        indicator.localRotation = Quaternion.Euler(indicatorLocalRotationEuler);
        indicator.localScale = indicatorLocalScale;
    }

    private void ConfigureIndicatorFx(GameObject indicatorObject)
    {
        if (!autoAddPulseEffect || indicatorObject == null)
        {
            return;
        }

        TeleportIndicatorPulse pulse = indicatorObject.GetComponent<TeleportIndicatorPulse>();
        if (pulse == null)
        {
            pulse = indicatorObject.AddComponent<TeleportIndicatorPulse>();
        }

        pulse.Configure(
            pulseSpeed,
            pulseAmount,
            hoverSpeed,
            hoverAmount,
            rotationSpeed,
            emissionColor,
            minEmission,
            maxEmission);
    }

    private void UpdateConfirmHand(
        InputActionProperty action,
        ref bool wasPressed,
        XRRayInteractor explicitRayInteractor,
        GameObject interactorObject,
        string handLabel)
    {
        float value = ReadActionValue(action);
        bool pressed = value > confirmPressedThreshold;
        bool pressedNow = pressed && !wasPressed;
        wasPressed = pressed;

        if (!pressedNow)
        {
            return;
        }

        XRRayInteractor rayInteractor = explicitRayInteractor;
        if (rayInteractor == null && interactorObject != null)
        {
            rayInteractor = interactorObject.GetComponentInChildren<XRRayInteractor>(true);
        }

        if (rayInteractor == null)
        {
            if (verboseDebug)
            {
                Debug.LogWarning($"AbilityTeleportController: No XRRayInteractor found for {handLabel} hand.");
            }
            return;
        }

        Vector3 destinationPosition;
        bool foundDestination = false;

        if (rayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit hit))
        {
            BaseTeleportationInteractable teleportTarget = hit.collider != null
                ? hit.collider.GetComponentInParent<BaseTeleportationInteractable>()
                : null;

            if (teleportTarget != null)
            {
                destinationPosition = hit.point;
                foundDestination = true;
            }
            else
            {
                destinationPosition = default;
            }
        }
        else
        {
            destinationPosition = default;
        }

        if (!foundDestination)
        {
            // Some XRI setups show a valid teleport endpoint while TryGetCurrent3DRaycastHit() is empty.
            if (rayInteractor.TryGetHitInfo(out Vector3 hitPosition, out _, out _, out bool isValidTarget) && isValidTarget)
            {
                destinationPosition = hitPosition;
                foundDestination = true;
            }
        }

        if (!foundDestination)
        {
            if (verboseDebug)
            {
                Debug.Log($"AbilityTeleportController: {handLabel} trigger pressed, but no valid teleport destination was found.");
            }
            return;
        }

        if (teleportationProvider == null)
        {
            teleportationProvider = FindObjectOfType<TeleportationProvider>();
        }

        if (teleportationProvider == null)
        {
            Debug.LogWarning("AbilityTeleportController: No TeleportationProvider found in scene.");
            return;
        }

        TeleportRequest request = new TeleportRequest
        {
            destinationPosition = destinationPosition,
            destinationRotation = Quaternion.identity,
            requestTime = Time.time,
            matchOrientation = MatchOrientation.WorldSpaceUp,
        };

        bool queued = teleportationProvider.QueueTeleportRequest(request);
        if (verboseDebug)
        {
            Debug.Log($"AbilityTeleportController: Manual confirm teleport ({handLabel}) queued={queued} at {destinationPosition}.");
        }
    }

    private float ReadActionValue(InputActionProperty action)
    {
        if (action.action == null)
        {
            return 0f;
        }

        return action.action.ReadValue<float>();
    }

    private void TryEnableAction(InputActionProperty action)
    {
        if (action.action == null)
        {
            return;
        }

        if (!action.action.enabled)
        {
            action.action.Enable();
        }
    }

    private void TryDisableAction(InputActionProperty action)
    {
        if (action.action == null)
        {
            return;
        }

        if (action.action.enabled)
        {
            action.action.Disable();
        }
    }
}
