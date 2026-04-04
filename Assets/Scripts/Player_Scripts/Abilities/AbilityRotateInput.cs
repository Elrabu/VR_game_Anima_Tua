using UnityEngine;
using UnityEngine.InputSystem;

public class AbilityRotateInput : MonoBehaviour
{
    [SerializeField] private InputActionProperty rightRotateButton;
    [SerializeField] private InputActionProperty leftRotateButton;
    [SerializeField] private AbilityInventory abilityInventory;
    [SerializeField] private AbilityBookEquipOnSelect bookEquipOnSelect;
    [SerializeField] private float pressedThreshold = 0.5f;
    [SerializeField] private bool logCurrentAbility = false;

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
    }

    private void Update()
    {
        if (abilityInventory == null)
        {
            return;
        }

        bool rightPressed = IsPressed(rightRotateButton);
        bool leftPressed = IsPressed(leftRotateButton);

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

    private void RotateWithHand(bool isRightHand)
    {
        if (bookEquipOnSelect != null)
        {
            bookEquipOnSelect.SetPreferredHand(isRightHand);
        }

        bool rotated = abilityInventory.RotateNextAbility();

        if (logCurrentAbility && rotated)
        {
            Debug.Log($"Current ability: {abilityInventory.CurrentAbility}");
        }
    }
}
