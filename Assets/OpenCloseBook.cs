using UnityEngine;
using UnityEngine.InputSystem;

public class OpenCloseBook : MonoBehaviour
{
    [SerializeField] private Animator mAnimator;
    [SerializeField] private InputActionProperty interactleft;
    [SerializeField] private InputActionProperty interactright;
    private bool bookOpen = false;
    private float cooldown = 1f;
    float trackcooldown;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (mAnimator != null)
        {
            mAnimator.SetTrigger("Close");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (mAnimator != null)
        {
            float value = interactleft.action.ReadValue<float>();
            float value2 = interactright.action.ReadValue<float>();
        
            if (value == 1 || value2 == 1)
            {
                if (bookOpen == false && trackcooldown <= 0) //check for closed book and cooldown 0
                {
                    mAnimator.SetTrigger("Open");
                    bookOpen = true;
                    trackcooldown = cooldown;
                } else if (bookOpen == true && trackcooldown <= 0) //check for opened book and cooldown 0
                {
                    mAnimator.SetTrigger("Close");
                    bookOpen = false;
                    trackcooldown = cooldown;
                }
            }
            trackcooldown -= Time.deltaTime;
        }
    }
}
