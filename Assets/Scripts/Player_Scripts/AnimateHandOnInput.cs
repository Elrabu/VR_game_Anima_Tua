using UnityEngine;
using UnityEngine.InputSystem;

public class AnimateHandOnInput : MonoBehaviour
{
    [SerializeField] private InputActionProperty triggerValue;
    [SerializeField] private InputActionProperty gripValue;

    [SerializeField] private Animator handAnimator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (handAnimator == null)
        {
            return;
        }

        float trigger = triggerValue.action != null ? triggerValue.action.ReadValue<float>() : 0f;
        float grip = gripValue.action != null ? gripValue.action.ReadValue<float>() : 0f;

        handAnimator.SetFloat("Trigger", trigger);
        handAnimator.SetFloat("Grip", grip);
    }
}