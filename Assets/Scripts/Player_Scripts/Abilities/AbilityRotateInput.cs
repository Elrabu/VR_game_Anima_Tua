using UnityEngine;
using UnityEngine.InputSystem;

public class AbilityRotateInput : MonoBehaviour
{
    [SerializeField] private InputActionProperty rotateButton;
    [SerializeField] private AbilityInventory abilityInventory;
    [SerializeField] private float pressedThreshold = 0.5f;
    [SerializeField] private bool logCurrentAbility = false;

    private bool wasPressed;

    private void Reset()
    {
        if (abilityInventory == null)
        {
            abilityInventory = GetComponent<AbilityInventory>();
        }
    }

    private void Update()
    {
        if (abilityInventory == null || rotateButton.action == null)
        {
            return;
        }

        float value = rotateButton.action.ReadValue<float>();
        bool isPressed = value > pressedThreshold;

        if (isPressed && !wasPressed)
        {
            bool rotated = abilityInventory.RotateNextAbility();

            if (logCurrentAbility && rotated)
            {
                Debug.Log($"Current ability: {abilityInventory.CurrentAbility}");
            }
        }

        wasPressed = isPressed;
    }
}
