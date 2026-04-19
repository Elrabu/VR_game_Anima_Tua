using UnityEngine;
using UnityEngine.InputSystem;

public class AbilityRotateInput : MonoBehaviour
{
    [SerializeField] private InputActionProperty rightRotateButton;
    [SerializeField] private InputActionProperty leftRotateButton;
    [SerializeField] private AbilityInventory abilityInventory;
    [SerializeField] private AbilityBookEquipOnSelect bookEquipOnSelect;
    [SerializeField] private AbilityTeleportController teleportController;
    [SerializeField] private float pressedThreshold = 0.5f;
    [SerializeField] private bool logCurrentAbility = false;
    [SerializeField] private bool autoEnableActions = true;
    [SerializeField] private bool verboseDebug = false;

    private bool wasRightPressed;
    private bool wasLeftPressed;

    private void Reset()
    {
        if (abilityInventory == null)
        {
            abilityInventory = GetComponent<AbilityInventory>();
        }

        if (bookEquipOnSelect == null)
        {
            bookEquipOnSelect = GetComponent<AbilityBookEquipOnSelect>();
        }

        if (teleportController == null)
        {
            teleportController = GetComponent<AbilityTeleportController>();
        }
    }

    private void Update()
    {
        if (abilityInventory == null)
        {
            if (verboseDebug)
            {
                Debug.LogWarning("AbilityRotateInput: abilityInventory is not assigned.");
            }
            return;
        }

        bool rightPressed = IsPressed(rightRotateButton);
        bool leftPressed = IsPressed(leftRotateButton);

        if (verboseDebug)
        {
            float rightValue = ReadValue(rightRotateButton);
            float leftValue = ReadValue(leftRotateButton);
            if (rightValue > 0f || leftValue > 0f)
            {
                Debug.Log($"AbilityRotateInput values -> Right: {rightValue:0.00}, Left: {leftValue:0.00}");
            }
        }

        bool rightPressedNow = rightPressed && !wasRightPressed;
        bool leftPressedNow = leftPressed && !wasLeftPressed;

        if (rightPressedNow)
        {
            RotateWithHand(isRightHand: true);
        }

        if (leftPressedNow)
        {
            RotateWithHand(isRightHand: false);
        }

        wasRightPressed = rightPressed;
        wasLeftPressed = leftPressed;
    }

    private bool IsPressed(InputActionProperty actionProperty)
    {
        if (actionProperty.action == null)
        {
            return false;
        }

        float value = actionProperty.action.ReadValue<float>();
        return value > pressedThreshold;
    }

    private float ReadValue(InputActionProperty actionProperty)
    {
        if (actionProperty.action == null)
        {
            return 0f;
        }

        return actionProperty.action.ReadValue<float>();
    }

    private void OnEnable()
    {
        if (autoEnableActions)
        {
            TryEnableAction(rightRotateButton, "rightRotateButton");
            TryEnableAction(leftRotateButton, "leftRotateButton");
        }

        if (verboseDebug)
        {
            Debug.Log($"AbilityRotateInput ready. Right action: {GetActionName(rightRotateButton)} | Left action: {GetActionName(leftRotateButton)} | Threshold: {pressedThreshold:0.00}");
        }
    }

    private void OnDisable()
    {
        if (autoEnableActions)
        {
            TryDisableAction(rightRotateButton);
            TryDisableAction(leftRotateButton);
        }
    }

    private void TryEnableAction(InputActionProperty actionProperty, string fieldName)
    {
        if (actionProperty.action == null)
        {
            if (verboseDebug)
            {
                Debug.LogWarning($"AbilityRotateInput: {fieldName} has no action assigned.");
            }
            return;
        }

        if (!actionProperty.action.enabled)
        {
            actionProperty.action.Enable();
        }
    }

    private void TryDisableAction(InputActionProperty actionProperty)
    {
        if (actionProperty.action == null)
        {
            return;
        }

        if (actionProperty.action.enabled)
        {
            actionProperty.action.Disable();
        }
    }

    private string GetActionName(InputActionProperty actionProperty)
    {
        if (actionProperty.action == null)
        {
            return "<none>";
        }

        return actionProperty.action.name;
    }

    private void RotateWithHand(bool isRightHand)
    {
        if (bookEquipOnSelect != null)
        {
            bookEquipOnSelect.SetPreferredHand(isRightHand);
        }

        if (teleportController != null)
        {
            teleportController.SetPreferredHand(isRightHand);
        }

        bool rotated = abilityInventory.RotateNextAbility();

        if (logCurrentAbility && rotated)
        {
            Debug.Log($"Current ability: {abilityInventory.CurrentAbility}");
        }

        if (logCurrentAbility && !rotated)
        {
            Debug.Log("Ability rotation did not change. Check Start Unlocked / pickups and input bindings.");
        }
    }
}
